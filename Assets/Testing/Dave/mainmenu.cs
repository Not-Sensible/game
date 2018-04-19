using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class mainmenu : MonoBehaviour {

    public Texture background;
    public GUIStyle startgame;
    public GUIStyle quitgame;

    public float levelSelectY;
    public float newGameY;
    public float creditsY;
    public float quitY;

    public float levelSelectX;
    public float newGameX;
    public float creditsX;
    public float quitX;

    

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);

        if(GUI.Button(new Rect(Screen.width * levelSelectX, Screen.height * levelSelectY, Screen.width*.15f,Screen.height*.15f), "Start Game", startgame))
        {
            SceneManager.LoadScene(1);
            
        }

        if (GUI.Button(new Rect(Screen.width * quitX, Screen.height * quitY, Screen.width * .15f, Screen.height * .15f), "Quit", quitgame))
        {
            Application.Quit();
        }
    }
}
