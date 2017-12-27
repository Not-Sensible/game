using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {
    public LevelController levelController;
    public HeathBar Healthybar;
    private bool hit = false;
    private float health;

    float timeLeft = 3f;
    // Use this for initialization
    void Start () {
        levelController = FindObjectOfType<LevelController>();
        Healthybar = FindObjectOfType<HeathBar>();
        health = Healthybar.getCurrentHealth();
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log(hit);
        if (hit == true)
            hit=Timer();

    }

    bool Timer()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft<0)
        {
            timeLeft = 3;
            return false;
        }
        else
        {
            return true;
        }
    }
        //Collision with spikes
    void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.name=="Player" && hit==false)  //remove the hit
        {

            Debug.Log("I am hit");
            float hp = health - 0.15f;
            Healthybar.setCurrentHealth(hp);
            hit = true;
        }
    }
}
