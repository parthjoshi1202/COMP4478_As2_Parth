//Navigating to the main Game Scene once the Game is over, using switch case
//Connected to "Play Again" button to restart the game

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void triggerMenuBehavior(int i)
    {
        switch(i)
        {
            default:
            case(0):
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
                break;
        }
    }
}
