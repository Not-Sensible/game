using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 public class HeathBar : MonoBehaviour {
    public LevelController levelController;
    public HeathBar heath;
    public float fillAmount;
    public  float currentHealth;

    public Image content;
	// Use this for initialization
	void Start () {
        levelController = FindObjectOfType<LevelController>();
        heath = FindObjectOfType<HeathBar>();
        currentHealth = 1;
    }
    void FixedUpdate()
    {
        HandleBar();
    }
    // Update is called once per frame
    void Update () {
        //HandleBar();
	}

    // Get the players current health
    public  float getCurrentHealth()
    {
        return currentHealth;
    }

    // Set players current health
    public  void setCurrentHealth (float hp)
    {
      //  Debug.Log(hp);
        currentHealth = hp;
    }


    // Changing the health bar and respawning
    void HandleBar()
    {

        //Debug.Log(Map(currentHealth, 0, 100, 0, 1));
        content.fillAmount = currentHealth;
        //Debug.Log(currentHealth);
        if (currentHealth == 0)
        {
            levelController.PlayerSpawn();
            currentHealth = 1;
        }
    }

    /*private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }*/
}
