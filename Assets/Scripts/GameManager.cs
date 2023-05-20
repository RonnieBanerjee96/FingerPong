using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    
    Transform optionsClickedPosition;
    public static Modes mode;

    Scene scene;

    public static float TimeElapsed;
    public static int hitCounterReset;
    



    
    void Awake()
    {

        //Application.targetFrameRate = 90;
        hitCounterReset = 1;
 
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(scene.name);

    }

    public void LoadGameScene()
    {
        Time.timeScale = 1;
        Wall.gameOver += DoSomewthingOnGameOver;
        SceneManager.LoadScene("GameScene");
        mode = Modes.GameMode;
        
    }

   
    public void LoadMenu()
    {
        
        Time.timeScale = 1;
        Wall.gameOver -= DoSomewthingOnGameOver;

        SceneManager.LoadScene("Main Menu");
        mode = Modes.MainMenu;

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StoreMenuTime()
    {
        TimeElapsed = Time.time;
    }

    void DoSomewthingOnGameOver()
    {
        //LoadMenu();
        

        
    }

   

}
