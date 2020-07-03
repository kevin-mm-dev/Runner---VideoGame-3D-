using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{set;get;}
    private PlayerMotor motor;
    bool isGameStarted=false;
    ////Puntuacion
    float score;
    float coins;
    float finalScore;
    ///////TEXT
    public Text scoreText;
    public Text coinsText;


    public bool IsGameStarted{
        get{
            return isGameStarted;
        }
    }

    private void Awake() {
        Instance=this;
        motor=FindObjectOfType<PlayerMotor>();

        // motor =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            score+=(Time.deltaTime*2);
            scoreText.text=score.ToString("0");

            // Debug.Log("Socore: "+score.ToString("0"));
            
        }
    }

    public void StartGame(){
        isGameStarted=true;
        motor.StartRun();
    }

    public void GetCollectable(int collectableAnnt){
        coins++;
        score+=collectableAnnt;
        UpdateScore();
    }

    public void GameOver(){
        finalScore=score+coins;
        
    }
    public void PlayAgain(){
        //Iniciar el nivel de nuevo
        Invoke("LoadScene",1f);
    }

    void LoadScene(){
        SceneManager.LoadScene(0);
    }

    public void UpdateScore(){
        scoreText.text=score.ToString();
        coinsText.text=coins.ToString();


    }
}
