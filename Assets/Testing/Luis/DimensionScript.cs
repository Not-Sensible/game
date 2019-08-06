using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionScript : MonoBehaviour
{

    private    Rigidbody2D obst;
    private    BoxCollider2D collide;
    public LevelController levelController;
    public ShiftingController shiftcontrol;
    public char dimension='b';
	// Use this for initialization
	void Start ()
    {
        collide = GetComponent<BoxCollider2D>();
        obst = GetComponent<Rigidbody2D>();
        shiftcontrol = FindObjectOfType<ShiftingController>();
    }

    void resetcollisions()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {


        if(shiftcontrol.charDimensionNumber==dimension) // Finished Dimension Shifting code
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
            GetComponent<BoxCollider2D>().enabled = false;


        

    }

}
