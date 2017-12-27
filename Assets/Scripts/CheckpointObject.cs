using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointObject : MonoBehaviour {
    public LevelController levelController;

    // Use this for initialization
    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    void OnTriggerEnter2D(Collider2D collision)   //Detects if the player is in contact with the checkpoint
    {
        if (collision.name == "Player")
        {
            levelController.CurrentCheckpoint = gameObject;  //Assigns the new checkpoint
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
