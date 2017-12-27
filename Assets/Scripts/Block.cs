using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public float BlockRotation; //Holds the rotation value for the block, can be customised in unity
    private Player_Script2 player;  //Accesses the player script
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player_Script2>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision) //Checks if the block is colliding with the player
    {
        if(collision.name=="Player" && player.transform.eulerAngles.z!=transform.eulerAngles.z)
        {
            player.RotatePlayer(BlockRotation);
        }
       // if (collision.name == "Player" && player.transform.eulerAngles.z != 360f - BlockRotation)   //The player just falls through, I don't know why, the angle system is annoying
      //  {

           // player.RotatePlayer(BlockRotation);
      //  }


    }
}
