using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShiftingController : MonoBehaviour
{

    public char charDimensionNumber = 'n';


    public Animator animTest;
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1)) //Normal Dimension
        {
            Debug.Log("Normal Dimension");
            charDimensionNumber = 'n';
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))  //Blue Dimension
        {
            charDimensionNumber = 'n';
            Debug.Log("Blue");
            charDimensionNumber = 'b';

            animTest.SetBool("CurrentlyBlue", true);
            animTest.SetBool("CurrentlyPurple", false);
            animTest.SetBool("CurrentlyGreen", false);
            animTest.SetBool("CurrentlyOrange", false);
            animTest.SetBool("CurrentlyWhite", false);

        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) // Purple Dimension
        {
            charDimensionNumber = 'n';
            Debug.Log("Purple");
            charDimensionNumber = 'p';

            animTest.SetBool("CurrentlyBlue", false);
            animTest.SetBool("CurrentlyPurple", true);
            animTest.SetBool("CurrentlyGreen", false);
            animTest.SetBool("CurrentlyOrange", false);
            animTest.SetBool("CurrentlyWhite", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) // Green Dimension
        {
            charDimensionNumber = 'n';
            Debug.Log("Green");
            charDimensionNumber = 'g';

            animTest.SetBool("CurrentlyBlue", false);
            animTest.SetBool("CurrentlyPurple", false);
            animTest.SetBool("CurrentlyGreen", true);
            animTest.SetBool("CurrentlyOrange", false);
            animTest.SetBool("CurrentlyWhite", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) // Orange Dimension
        {
            charDimensionNumber = 'n';
            Debug.Log("Orange");
            charDimensionNumber = 'o';

            animTest.SetBool("CurrentlyBlue", false);
            animTest.SetBool("CurrentlyPurple", false);
            animTest.SetBool("CurrentlyGreen", false);
            animTest.SetBool("CurrentlyOrange", true);
            animTest.SetBool("CurrentlyWhite", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6)) // White Dimension
        {
            charDimensionNumber = 'n';
            Debug.Log("White");
            charDimensionNumber = 'w';

            animTest.SetBool("CurrentlyBlue", false);
            animTest.SetBool("CurrentlyPurple", false);
            animTest.SetBool("CurrentlyGreen", false);
            animTest.SetBool("CurrentlyOrange", false);
            animTest.SetBool("CurrentlyWhite", true);
        }




    }
}
