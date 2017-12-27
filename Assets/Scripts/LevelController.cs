using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {


    public GameObject CurrentCheckpoint;
    private Player_Script2 player;
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player_Script2>();
    }
	
	// Update is called once per frame
	void Update () {
	}
    public void PlayerSpawn()   //Acessible for every script, to spawn the player
    {
        player.transform.position = CurrentCheckpoint.transform.position;

    }

}
