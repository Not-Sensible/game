using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour {
    private PlayerMomentumAgain player;  //Accesses the player script

    void Start () {
        player = FindObjectOfType<PlayerMomentumAgain>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = player.transform.rotation;
	}
}
