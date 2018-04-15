using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {
    public LevelController levelController;

    // public HeathBar Healthybar;
    private float health;

    // Use this for initialization
    void Start () {
        levelController = FindObjectOfType<LevelController>();
        // Healthybar = FindObjectOfType<HeathBar>();
        // health = Healthybar.getCurrentHealth();
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log(hit);
        

    }

        //Collision with spikes
    void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag=="Player")  //remove the hit
        {
            levelController.PlayerSpawn();
           // Healthybar.setCurrentHealth(hp);
        }
    }
}
