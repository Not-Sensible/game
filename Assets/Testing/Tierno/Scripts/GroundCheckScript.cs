using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour {
    private PlayerMomentumAgain player;  //Accesses the player script
    public char type;
    void Start () {
        player = FindObjectOfType<PlayerMomentumAgain>();
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        // UnityEngine.Debug.Log("WHYYYYYYYYYYYYY");
        Debug.Log("please");
        if (coll.gameObject.tag == "Wall" && type=='R')
        {
            player.moveright = false;
            player.X = 0;
        }
        else if (coll.gameObject.tag == "Wall" && type == 'L')
        {
            player.moveleft = false;
            player.X = 0;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        
            player.moveright = true;
            player.moveleft = true;
    }
    // Update is called once per frame
    void Update () {
        transform.rotation = player.transform.rotation;
	}
}
