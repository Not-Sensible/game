using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenu : MonoBehaviour {

    public Texture background;

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);

      if(GUI.Button(new Rect(Screen.width * .5f, Screen.height * .5f, Screen.width * .5f, Screen.height * .1f), "Play"))
        {

        }
    }
}
