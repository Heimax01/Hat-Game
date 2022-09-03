using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [HideInInspector]
    public int score;

    private int highscore;

    private float currentTime;

    [SerializeField]
    private float startTime;

    [HideInInspector]
    public bool gameStarted;

    private UIController uiController;

    private SpawnController spawnController;

    [SerializeField]
    private Transform player;

    private Vector2 playerPosition;

    private void Awake() {

        DeleteHighscore();
    }

    // Start is called before the first frame update
    void Start()
    {

        gameStarted = false;
        uiController = FindObjectOfType<UIController>();
        spawnController = FindObjectOfType<SpawnController>();
        highscore = GetScore();
        uiController.txtTime.text = currentTime.ToString();
        playerPosition = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyAllBalls() {

        foreach (Transform child in spawnController.allBallsParent) {

            Destroy(child.gameObject);

        }

    }

    public void SaveScore() {

        if (score > highscore) {
           
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);

        } else {

            return;

        }

    }


    public int GetScore() {

        int highscore = PlayerPrefs.GetInt("highscore");

        return highscore;

    }

    public void DeleteHighscore() {

        PlayerPrefs.DeleteKey("highscore");

    }

    public void InvokeCountdownTime() {
        InvokeRepeating("CountdownTime", 1f, 1f);
    }

    public void StartGame() {

        score = 0;
        currentTime = startTime;
        gameStarted = true;
        InvokeCountdownTime();
        player.position = playerPosition; 
    }

    public void BackMainMenu() {

        score = 0;
        currentTime = 0f;
        gameStarted = false;
        CancelInvoke("CountdownTime");
        player.position = playerPosition;

    }

    public void CountdownTime() {

        uiController.txtTime.text = currentTime.ToString();

        if(currentTime > 0f && gameStarted) {

            currentTime -= 1f;

        } else {

            uiController.panelGameover.gameObject.SetActive(true);
            uiController.panelGame.gameObject.SetActive(false);
            gameStarted = false;
            SaveScore();
            currentTime = 0f;
            CancelInvoke("CountdownTime");
            return;

        }
        
    }
}
