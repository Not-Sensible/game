using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour {
    public float speed;
    public float maxspeed;
    public float jumpspeed;   // Customise in Unity, the speed with which the character jumps each frame
    public float jumptime;   // How long this jump lasts for, editable in Unity as well
    public Vector2 offsets;    //contains x and y movement speeds
    private Rigidbody2D rig2d;   
    private Animator animy;
    //Used to define what is ground and if the player is touching it
    public Transform TouchingTerrain;
    public float GroundCheckRadius;
    public LayerMask CollideList;  //could be expanded upon later with more than one layermask
    private bool onGround;
    private float timeLeft;
    private char Horizontal;   //Used to work out the momentum for the horizontal movements, L is left, R is right.
    // Use this for initialization
    void Start () {
        rig2d = GetComponent<Rigidbody2D>();   //Acesses it
        animy = GetComponent<Animator>();  //Acesses Animation component for basic left and right movement
    }

    void MoveTo(Vector2 pos)  //Not actually used, could be useful
    {
        transform.position = pos;
    }

    bool clock()   //Clock system for the character, can be used for anything.
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            return false;
        }
        else
        {
            return true;
        }


    }

    void checks()  //Called in Fixed update, to make it look nicer.
    {
        onGround = Physics2D.OverlapCircle(TouchingTerrain.position, GroundCheckRadius, CollideList); //Code to work out if the player is on terrain or not.
        if (onGround == false)   //Prevents awkard rotation in the air
            rig2d.freezeRotation = true;
        else if (onGround == true)  //Allows rotation on the ground
            rig2d.freezeRotation = false;
        if (timeLeft > 0f)
        {
            clock();

        }
        else if (timeLeft < 0f)       //adjust much later
            offsets[1] = 0;


    }
    void Movement()
    {
        Physics.gravity = new Vector3(0, 0, 0f);
        if (Input.GetKeyDown("a"))
        {
            Horizontal = 'L';
            //offsets[0] = -speed;
        }
        else if (Input.GetKeyDown("d"))
        {
            Horizontal = 'R';
           // offsets[0] = speed;
        }
        if (Input.GetKeyDown("space") && onGround!=false)  //Cannot jump whilst not on the ground
        {
            offsets[1] = jumpspeed;
            timeLeft = jumptime;
            clock();
        }
        if (Input.GetKeyUp("a") && offsets[0] != speed)    //Will Implement Momentum later
        {
            offsets[0] = 0;
            Horizontal = 'n';
        }
        else if (Input.GetKeyUp("d") && offsets[0] != -speed)
        {
            offsets[0] = 0;
            Horizontal = 'n';
        }
        //  transform.Translate(offsets[0] * Time.deltaTime, offsets[1] * Time.deltaTime,0f, Space.World);
        transform.Translate(offsets[0] * Time.deltaTime, 0f, 0f);    //By seperating the movement into two different lines, the horizontal movement can be based off of the player's rotation, and the vertical jumps modified to be based off of world space movement if need be (e.g. to stop spinning in the air.
        transform.Translate(0f, offsets[1]*Time.deltaTime, 0f);

    }

    void Momentum()
    {
        switch (Horizontal) //Momentum is being worked on, very much a work in progress
        {
            case 'L':
                if (offsets[0]>-maxspeed)
                    offsets[0] = offsets[0] + -speed;
                else if (offsets[0]<-maxspeed)
                        offsets[0] = offsets[0] + speed;
                break;
            case 'R':
                if (offsets[0] < maxspeed)
                    offsets[0] = offsets[0] + speed;
                else if (offsets[0] > maxspeed)
                    offsets[0] = offsets[0] + speed;
                break;
            default:
                Debug.Log("stop it");
                break;

        }

    }
    // Update is called once per frame
    void Update () {
        Movement();
        Momentum();
	}

    void FixedUpdate()
    {
        checks();
            
    }
}
