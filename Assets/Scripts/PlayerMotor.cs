using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {
    const float DISTANCE = 1.6f;
    const float TURN_SPEED = 0.5f;
    //Character movement
    float jump = 0.5f;
    private float gravity = 12f;
    float verticalVelocity;
    float speed = 5f;
    int lane = 1;
    bool isRunning=false;
    CharacterController controller;
    Animator anim;

    // Start is called before the first frame update
    void Start () {
        controller = GetComponent<CharacterController> ();
        anim = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update () {

        if (!isRunning)
        {
            return;
        }
        if (Input.GetKeyDown (KeyCode.LeftArrow)) {
            ChangePlane (false);

        }
        if (Input.GetKeyDown (KeyCode.RightArrow)) {
            ChangePlane (true);
        }

        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (lane == 0) {
            targetPosition += Vector3.left * DISTANCE;
        } else if (lane == 2) {
            targetPosition += Vector3.right * DISTANCE;
        }

        Vector3 moveTarget = Vector3.zero;
        moveTarget.x = ((targetPosition - transform.position).normalized.x * speed);
        bool isGrounded = IsGrounded ();
        anim.SetBool("isGrounded",isGrounded);
        if (isGrounded) {
            verticalVelocity = -0.1f;
            if (Input.GetKeyDown (KeyCode.Space)) {
                verticalVelocity = jump;
                anim.SetTrigger("Jump");
            }
        } else {
            verticalVelocity = (gravity + Time.deltaTime);
        }
        moveTarget.y = verticalVelocity;
        moveTarget.z = speed;

        controller.Move (moveTarget * Time.deltaTime);
        anim.SetFloat("speed",controller.velocity.z);
        Vector3 dir = controller.velocity;
        dir.y = 0;
        //Rotation del personaje cuando cambia de carril
        transform.forward = Vector3.Lerp (transform.forward, dir, TURN_SPEED);

    }

    public void ChangePlane (bool isRight) {
        lane += (isRight) ? 1 : -1;
        lane = Mathf.Clamp (lane, 0, 2);
    }
    public void StartRun(){
        anim.SetFloat("speed",5f);
        isRunning=true;
    }

    public bool IsGrounded () {
        Vector3 vector = new Vector3 (
            controller.bounds.center.x,
            (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f,
            controller.bounds.center.z);

        Ray groundRay = new Ray (vector, Vector3.down);

        Debug.DrawRay (vector, Vector3.down, Color.red, 5.0f);
        return Physics.Raycast (groundRay, 0.2f + 0.1f);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            //Morir personaje
            Crash();
        }
    }

    public void Crash(){
        anim.SetTrigger("Dead");
        anim.SetFloat("speed",0f);
        isRunning=false;
    }

}