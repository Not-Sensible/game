using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckpointObject : MonoBehaviour {
    public LevelController levelController;
    public CheckpointAnimation checkpoint;
    public AudioClip telporterOn;
    public AudioClip telporterOff;
    private AudioSource source;
    Animator m_Animator;
    // Use this for initialization
    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        m_Animator = gameObject.GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)   //Detects if the player is in contact with the checkpoint
    {
        if (collision.name == "Player")
        {
            levelController.CurrentCheckpoint = gameObject;  //Assigns the new checkpoint
            source.PlayOneShot(telporterOn, 1.0f);
            checkpoint.AnimateME();
        }
    }
    void OnTriggerExit2D(Collider2D collision)   //Detects if the player is in contact with the checkpoint
    {
        if (collision.name == "Player")
        {
            levelController.CurrentCheckpoint = gameObject;  //Assigns the new checkpoint
            source.PlayOneShot(telporterOff, 1.0f);
            checkpoint.StopAnimatingME();
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
