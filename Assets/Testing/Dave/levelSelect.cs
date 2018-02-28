using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelSelect : MonoBehaviour {

    public Texture background;
    public GUIStyle level1;
    public GUIStyle level2;
    public GUIStyle level3;
    public GUIStyle level4;
    public GUIStyle level5;
    public GUIStyle back;

    public float level1Y;
    public float level2Y;
    public float level3Y;
    public float level4Y;
    public float level5Y;
    public float backY;

    public float level1X;
    public float level2X;
    public float level3X;
    public float level4X;
    public float level5X;
    public float backX;

    void OnGUI() { 
    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);

        if(GUI.Button(new Rect(Screen.width* level1X, Screen.height* level1Y, Screen.width* .235f, Screen.height* .2337f), "Level1", level1))
        {
          
            SceneManager.LoadScene(2);
            Debug.Log("working");
        }
        if (GUI.Button(new Rect(Screen.width* level2X, Screen.height* level2Y, Screen.width * .235f, Screen.height * .2337f), "Level2", level2))
        {
            //SceneManager.LoadScene("Actual_level");
        }
        if (GUI.Button(new Rect(Screen.width* level3X, Screen.height* level3Y, Screen.width * .235f, Screen.height * .2337f), "Level3", level3))
        {
            //SceneManager.LoadScene("Actual_level");
        }
        if (GUI.Button(new Rect(Screen.width* level4X, Screen.height* level4Y, Screen.width * .235f, Screen.height * .2337f), "Level4", level4))
        {
            //SceneManager.LoadScene("Actual_level");
        }
        if (GUI.Button(new Rect(Screen.width * level5X, Screen.height * level5Y, Screen.width * .235f, Screen.height * .2337f), "Level5", level5))
        {
            //SceneManager.LoadScene("Actual_level");
        }
        if (GUI.Button(new Rect(Screen.width * backX, Screen.height * backY, Screen.width * .25f, Screen.height * .075f), "Back", back))
        {
            SceneManager.LoadScene(0);
        }
    }

}

