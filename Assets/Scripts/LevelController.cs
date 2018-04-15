using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {


    public GameObject CurrentCheckpoint;
    public PlayerMomentumAgain player;
    public AudioClip Music;
    private AudioSource source;
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(Music, 0.8f);
    }
	
	// Update is called once per frame
	void Update () {
	}
    public void PlayerSpawn()   //Acessible for every script, to spawn the player
    {
        player.X = 0;
        player.Y = 0;
        player.onDeath();
        player.transform.position = new Vector3 (CurrentCheckpoint.transform.position.x, CurrentCheckpoint.transform.position.y,player.transform.position.z);
        //CurrentCheckpoint.

    }

}
