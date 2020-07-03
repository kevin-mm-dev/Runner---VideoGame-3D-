using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public Vector3 offset = new Vector3 (0f, 3f, -1f);

    private void LateUpdate () {

        if (GameManager.Instance.IsGameStarted) {
            Vector3 newPosition = target.position + offset;
            newPosition.x = 0;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
        }
    }
}