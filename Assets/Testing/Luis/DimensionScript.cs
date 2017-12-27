using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionScript : MonoBehaviour
{

    private    Rigidbody2D obst;
    private    BoxCollider2D collide;

    




	// Use this for initialization
	void Start ()
    {
        collide = GetComponent<BoxCollider2D>();
        obst = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Hello");
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else

            GetComponent<BoxCollider2D>().enabled = true;
    }

}
