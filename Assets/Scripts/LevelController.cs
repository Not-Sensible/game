using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {


    public GameObject CurrentCheckpoint;
    public PlayerMomentumAgain player;
    public AudioClip Music;
    public AudioSource source;
    public Text Timer;
    private GoalObject goal;
    public float startTime;
    public bool finished=false;
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        goal = FindObjectOfType<GoalObject>();
        source.PlayOneShot(Music, 0.8f);
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (finished == false)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString();
            Timer.text = ("Time" + ": " + minutes + ":" + seconds[0] + seconds[1]);
        }
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
