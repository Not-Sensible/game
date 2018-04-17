using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : MonoBehaviour {
    public PlayerMomentumAgain player;
    LevelController level;
	// Use this for initialization
	void Start () {
        level = FindObjectOfType<LevelController>();
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            level.finished = true;
            level.source.Pause();
            player.move = false;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
