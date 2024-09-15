//Game manager 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_manager : MonoBehaviour
{

    public static game_manager GameManager {get; private set; }
    public score_manager scoreManager;
    public bool isGameOver;
    public int time = 30;

    private void Awake() {
        if (GameManager == null) {
             GameManager = this;
        }
    }

    public void startGame() {
        isGameOver = false;
        InvokeRepeating("timer", 5f, 5f);
    }

    public void endGame() {
        //Show who wins on screen and end game
    }

    // Start is called before the first frame update
    void Start()
    {
        startGame();
    }

    public void updateScores() {
        //logic for updating scores
        //scoreManager.increaseScore();
        return;
    }

    // Update is called once per frame | right now test case
    void Update()
    {
    }

    public void timer() {
        updateScores();
        time-=5;
        if(time <= 0) {
            endGame();
        }
    }
}

