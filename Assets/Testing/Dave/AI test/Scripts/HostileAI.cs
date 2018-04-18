using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileAI : MonoBehaviour {
    int direction;
    float movingSpeed = 10f;
    public Sprite hostile1;
    public Sprite hostile2;
    public Transform stopRight;
    public Transform stopLeft;
    float wall1;
    float wall2;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        direction = -1;

        spriteRenderer = GetComponent<SpriteRenderer>();
        

         wall1 = stopRight.transform.position.x;
         wall2 = stopLeft.transform.position.x;
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
