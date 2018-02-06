using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainmenu : MonoBehaviour {

    public Texture background;
    public GUIStyle levelSelect;
    public GUIStyle newGame;
    public GUIStyle options;
    public GUIStyle credits;
    public GUIStyle quit;

    public float levelSelectY;
    public float newGameY;
    public float optionsY;
    public float creditsY;
    public float quitY;

    public float levelSelectX;
    public float newGameX;
    public float optionsX;
    public float creditsX;
    public float quitX;


    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);

        if(GUI.Button(new Rect(Screen.width * levelSelectX, Screen.height * levelSelectY, Screen.width * .25f, Screen.height * .075f), "Level Select", levelSelect))
        {
            SceneManager.LoadScene(1);
        }
        if (GUI.Button(new Rect(Screen.width * newGameX, Screen.height * newGameY, Screen.width * .25f, Screen.height * .075f), "New Game", newGame))
        {

        }
        if (GUI.Button(new Rect(Screen.width * optionsX, Screen.height * optionsY, Screen.width * .25f, Screen.height * .075f), "Options", options))
        {

        }
        if(GUI.Button(new Rect(Screen.width * creditsX, Screen.height * creditsY, Screen.width * .25f, Screen.height * .075f), "Credits", credits))
        {

        }

        if (GUI.Button(new Rect(Screen.width * quitX, Screen.height * quitY, Screen.width * .25f, Screen.height * .075f), "Quit", quit))
        {
            Application.Quit();
        }
    }
}
