using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenu : MonoBehaviour {

    public Texture background;
    public GUIStyle button1;

    public float buttonY;
    public float button2Y;
    public float buttonX;
    public float button2X;
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);

      if(GUI.Button(new Rect(Screen.width * buttonX, Screen.height * buttonY, Screen.width * .25f, Screen.height * .075f), "Level Select",button1))
        {

        }
        if (GUI.Button(new Rect(Screen.width * button2X, Screen.height * button2Y, Screen.width * .25f, Screen.height * .075f), "Credits", button1))
        {

        }
    }
}
