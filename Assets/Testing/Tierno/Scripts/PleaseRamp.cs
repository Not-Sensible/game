using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleaseRamp : MonoBehaviour {
    public PlayerMomentumAgain player;
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerMomentumAgain>();
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            player.flying = true;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
