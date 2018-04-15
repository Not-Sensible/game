using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackAnimation : MonoBehaviour {
    Animator m_Animator;
    //Value from the slider, and it converts to speed level
    float m_MySliderValue;
    private PlayerMomentumAgain player;
    float maxspeed;
    float speed;
    public float temp;
    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerMomentumAgain>();
        //Get the animator, attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
        maxspeed = player.maxspeed;

    }

    void OnGUI()
    {
        //Create a Label in Game view for the Slider
       // GUI.Label(new Rect(0, 25, 40, 60), "Speed");
        //Create a horizontal Slider to control the speed of the Animator. Drag the slider to 1 for normal speed.

      //  m_MySliderValue = GUI.HorizontalSlider(new Rect(45, 25, 200, 60), m_MySliderValue, 0.0F, 1.0F);
        //Make the speed of the Animator match the Slider value
        //m_Animator.speed = m_MySliderValue;
    }
    // Update is called once per frame
    void Update () {
	}
    void FixedUpdate()
    {
        if (player.X < 0.2693608 && player.DesiredDir == 'N' || player.X > -0.2693608 && player.DesiredDir == 'N')
            player.X = 0;
        speed = player.X;
        temp = speed / maxspeed;
        if (temp < 0)
            temp = temp * -1;
       // if(player.X<)
        m_Animator.speed = temp;
    }
}
