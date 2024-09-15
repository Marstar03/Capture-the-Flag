//score manager
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score_manager : MonoBehaviour
{
    public Text ScoreText;
    public int p1Score;
    public int p2Score;
    bool stop = false;
    void Start(){
        p1Score = 0;
        p2Score = 0;
        updateScoreboard();
    }

    public void increaseScore(int player) {
        //This needs to be changed
        if (player == 1)
        {
            p1Score++;
        }
        else if (player == 2)
        {
            p2Score++;
        }
        int scoreCap = 50;
        if (p1Score >= scoreCap || p2Score >= scoreCap)
        {
            string wintext = "TIE :(";
            if (p1Score > p2Score)
            {
                wintext = "Player 1 WON!";
            }
            else if (p2Score > p1Score)
            {
                wintext = "Player 2 WON!";
            }
            ScoreText.text = wintext;
            stop = true;
        }

        updateScoreboard();
    }

    private void updateScoreboard() {
        {
            ScoreText.text = p1Score.ToString() + "   " + p2Score.ToString();
        }
    }

 void Update() {
    if (stop)
    {
        Time.timeScale = 0;
    }
 }
    
}
