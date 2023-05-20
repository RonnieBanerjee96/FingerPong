using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class ScoreDisplayAndManager : MonoBehaviour
{

    Canvas GameOverCanvas;

    TextMeshProUGUI GameOverScoreText;
    TextMeshProUGUI GameOverHighScoreText;
    Text HighScoreText;
    Text Scoretext;
    Text hitsText;

    string _highScore = "HighScore";
    
    int hitsRemaining;
    float TimeElapsedTillNow;
    float Score;
    float HighScore;
    public static float GameStartTime =0.00f;


    void Awake()
    {
        GameOverCanvas = GameObject.Find("GameoverStats/GameOverCanvas").GetComponent<Canvas>();
        GameOverCanvas.enabled = false;

        GameOverScoreText = GameObject.Find("GameoverStats/GameOverCanvas/GameoverPanel/Score").GetComponent<TextMeshProUGUI>();
        GameOverHighScoreText = GameObject.Find("GameoverStats/GameOverCanvas/GameoverPanel/HighScore").GetComponent<TextMeshProUGUI>();
        HighScore = PlayerPrefs.GetFloat(_highScore);
        TimeElapsedTillNow = GameManager.TimeElapsed;
        HighScoreText = GameObject.Find("Canvas/HighScoreText").GetComponent<Text>();
        Scoretext = GameObject.Find("Canvas/score text").GetComponent<Text>();
        hitsText = GameObject.Find("Canvas/hit count").GetComponent<Text>();
        Wall.gameOver += GameOverHandler;
        


    }



    // Update is called once per frame
    void Update()
    {



        ManageScoreDisplay();
  

            
    }
    void GameOverHandler()
    {
        Wall.hitCounter = 1;
        Time.timeScale = 0;
        GameOverCanvas.enabled = true;
        GameOverScoreText.text = "Score:" + Score.ToString("F2");
        GameOverHighScoreText.text = "High Score:" + PlayerPrefs.GetFloat(_highScore).ToString("F2");
        //Debug.Log("Gameoverhandler Being called");
        Wall.gameOver -= GameOverHandler;



    }

    void ManageScoreDisplay()
    {
        hitsRemaining = Wall.hitCounter;
        GameStartTime = Time.time - TimeElapsedTillNow;
        Score = GameStartTime;
        Scoretext.text = "Score:" +Score.ToString("F2");
        HighScoreText.text = "High Score:" +PlayerPrefs.GetFloat(_highScore).ToString("F2");
        hitsText.text = "Hits Remaining:" + (4-(hitsRemaining)).ToString();

        if (Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetFloat(_highScore, Score);
        }

    }

    

}
