using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptMomentumBased : MonoBehaviour {
    public float speed; //Base movement speed for the player
    public float maxspeed; //A maxspeed for the player, allow it to be changed by factors such as terrain type or angle
    public float gravitystrength; //What strength the gravity should be on angled terrain or just in general
    public Vector2 offsets; //contains the player's X and y movement
    private Rigidbody2D rig2d;
    private Animator animy;

    //These lot are used to define what the player is touching
    public Transform TouchingTerrain;
    public float GroundCheckRadius;
    public LayerMask CollideList; //Temporary, dimension shifting will require lots of these, although I can imagine more blunt ways of doing it
    private bool onGround;

    private float timeLeft;
    private char Direction; //Used to define the direction of the player in human terms



	// Use this for initialization
	void Start () {
        rig2d = GetComponent<Rigidbody2D>();
        animy = GetComponent<Animator>();
	}

   public void MoveTo(Vector2 pos)  //Not actually used, could be useful
    {
        transform.position = pos;
    }

    bool clock()   //Clock system for the character, can be used for anything.
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            return false;
        }
        else
        {
            return true;
        }


    }

    // Update is called once per frame
    void Update () {
		
	}
}
