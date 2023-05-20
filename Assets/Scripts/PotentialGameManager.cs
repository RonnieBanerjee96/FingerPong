using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Modes { MainMenu, GameMode, Options }
public class PotentialGameManager : MonoBehaviour
{
  
    
    Modes CurrentMode;
    // Start is called before the first frame update
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentMode = GameManager.mode;
        modeSwitcher(CurrentMode);

    }

    void modeSwitcher(Modes mode)
    {
        switch (mode)
        {
            case Modes.GameMode:
                Debug.Log("Currently In Game Mode");
                break;

            case Modes.MainMenu:
                Debug.Log("Currently in MainMenu");
                break;
        }
    }
}
