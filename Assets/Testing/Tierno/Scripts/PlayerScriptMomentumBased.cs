using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptMomentumBased : MonoBehaviour {
    public float speed; //Base movement speed for the player
    public float maxspeed; //A maxspeed for the player, allow it to be changed by factors such as terrain type or angle
    public float RealMaxSpeed;
    public float gravitystrength; //What strength the gravity should be on angled terrain or just in general
    public Vector2 offsets; //contains the player's X and y movement
    private Rigidbody2D rig2d;
    private Animator animy;
    //These lot are used to define what the player is touching
   // public Transform TouchingTerrain;
    public float GroundCheckRadius;
    public LayerMask CollideList; //Temporary, dimension shifting will require lots of these, although I can imagine more blunt ways of doing it
    private bool onGround;
    private float timeLeft;
    private char Direction; //Used to define the direction of the player in human terms

    public Quaternion rotation = Quaternion.identity;
    //public Quaternion[] angles = new Quaternion[] { Quaternion.identity, Quaternion.identity, Quaternion.identity };
    public Quaternion[] angles;
    // Use this for initialization
    void Start () {
        rig2d = GetComponent<Rigidbody2D>();  //Enables the RigidBody2d component
        animy = GetComponent<Animator>();   //Allows the animator to work
        CreateLists();
    }
   
    public void CreateLists()  //They had to be here because I have no clue what this excuse of a language defines as scope
    {
        angles = new Quaternion[] { Quaternion.identity, Quaternion.identity, Quaternion.identity };  //Creating a list with the angles, more for convinience than having a load of random variable names
        angles[0].eulerAngles = new Vector3(0f, 0f, 10f);  //10 degrees
        angles[1].eulerAngles = new Vector3(0f, 0f, 20f);  //20 degrees
        angles[2].eulerAngles = new Vector3(0f, 0f, 30f);  //30 degrees    
                                                           //Add more I guess 
    }
    public void MoveTo(Vector2 pos)  //Not actually used, could be useful
    {
        transform.position = pos;
    }

    void checks() //Clock is here, pretty useless really, should be reliant on something else.
    {
        //onGround = Physics2D.OverlapCircle(TouchingTerrain.position, GroundCheckRadius, CollideList); //Code to work out if the player is on terrain or not
        if (timeLeft > 0f)
        {
            clock();

        }
        else if (timeLeft < 0f)       //adjust much later
            offsets[1] = 0;
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


    void Movement()
    {
        if(Input.GetKeyDown("a"))
        {
            Direction = 'L';
        }
        else if (Input.GetKeyDown("d"))
        {
            Direction = 'R';
        }
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("woooosh");  //Make it do stuff
            rig2d.MoveRotation(angles[2].eulerAngles.z*-1);
        }
        if(Input.GetKeyUp("a")&& offsets[0]!=speed)
        {
            offsets[0] = 0;
            Direction = 'n';
        }
        else if(Input.GetKeyUp("d")&& offsets[0]!=-speed)
        {
            offsets[0] = 0;
            Direction = 'n';     //These are so temporary and just copies, it won't work with what I'm planning
        }


    }

    void Momentum()
    {
        if (transform.eulerAngles.z==0)   //If on a flat plane, resort to the default main speed
        {
            RealMaxSpeed= maxspeed;
          //  Debug.Log("Why not?");
        }
        if(transform.eulerAngles.z==angles[0].eulerAngles.z)   //If on a 10 degree slope, go a bit faster, these numbers are not permenant
        {
           RealMaxSpeed = maxspeed * 1.1f;

        }
        else if(transform.eulerAngles.z==angles[1].eulerAngles.z)  //If on a 20 degree slope go faster
        {
            RealMaxSpeed = maxspeed * 1.2f;
        }
        else if(transform.eulerAngles.z==angles[2].eulerAngles.z) //Yada yada yada
        {
            RealMaxSpeed = maxspeed * 1.3f;
        }
        if (transform.eulerAngles.z == Quaternion.Inverse(angles[0]).eulerAngles.z)  //If on a negative 20 degrees slope, do the same thing 
        {
            RealMaxSpeed = maxspeed * 1.1f;
        }

        else if (transform.eulerAngles.z == Quaternion.Inverse(angles[1]).eulerAngles.z)  //yada yada yada
        {
            RealMaxSpeed = maxspeed * 1.2f;
        }
        else if (transform.eulerAngles.z == Quaternion.Inverse(angles[2]).eulerAngles.z)  //yada
        {
            RealMaxSpeed = maxspeed * 1.3f;
        }
    }

    // Update is called once per frame
    void Update () {
        Movement();
        Momentum();
	}


    void FixedUpdate()
    {
        checks();  //Checks that only need to be called once a frame, just putting here to make it look nicer.
    }
}
