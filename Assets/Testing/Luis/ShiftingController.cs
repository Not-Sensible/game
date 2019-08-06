using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShiftingController : MonoBehaviour
{
    public char charDimensionNumber= 'b';
    
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        

        if (Input.GetKeyDown(KeyCode.Alpha1))  //Blue Dimension
        {
            charDimensionNumber = 'n';
            charDimensionNumber = 'b';
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // Purple Dimension
        {
            charDimensionNumber = 'n';
            charDimensionNumber = 'p';
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) // Green Dimension
        {
            charDimensionNumber = 'n';
            charDimensionNumber = 'g';
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) // Orange Dimension
        {
            charDimensionNumber = 'n';
            charDimensionNumber = 'o';
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) //Normal Dimension
        {
            charDimensionNumber = 'w';
        }

    }
}
