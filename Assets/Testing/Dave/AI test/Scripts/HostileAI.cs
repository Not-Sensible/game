using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileAI : MonoBehaviour {
    int direction;
    float Dist = 100f;
    float movingSpeed = 1f;
    float minDist;
    float maxDist;
    public Sprite hostile1;
    public Sprite hostile2;
    public Transform Wall1;
    public Transform Wall2;
    float wall1;
    float wall2;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        direction = -1;

        spriteRenderer = GetComponent<SpriteRenderer>();
        minDist =  transform.position.x + Dist;
        maxDist =  transform.position.x - Dist;
        

         wall1 = Wall1.transform.position.x;
         wall2 = Wall2.transform.position.x;
    }

    public int getDirection()
    {
        return direction;
    }

    public void setDirection(int dir)
    {
        direction = dir;
    }
 
    // Update is called once per frame
    void Update ()
    {
                

        switch (direction)
        {
            case -1:
                if(transform.position.x < wall1)
                {

                    transform.Translate(movingSpeed*Time.deltaTime, 0.0f, 0.0f, Space.World);
                } else
                {
                    direction = 1;
                    spriteRenderer.sprite = hostile1;  
                }
                break;
            case 1:

                if(transform.position.x > wall2)
                {

                    transform.Translate(-movingSpeed*Time.deltaTime, 0.0f, 0.0f, Space.World);
                }
                else
                {
  
                    direction = -1;
                    spriteRenderer.sprite = hostile2;
                }

                break;
        }
		
	}
    
}
