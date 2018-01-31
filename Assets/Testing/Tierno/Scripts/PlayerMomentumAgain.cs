using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMomentumAgain : MonoBehaviour {
    public float maxspeed; //The plain default maxspeed on normal terrain
    public float speed;
    public float RealMaxspeed; //The Maxspeed changed by terrain and angles and state
    public float GravityStrength;
    public float X, Y; //X and Y of the player
    private Rigidbody2D rig2d;
    private Animator animy;
    private bool playermoving;
    public LayerMask CollideList; //Temporary, dimension shifting will require lots of these, although I can imagine more blunt ways of doing it
    public float FallRate; //How fast the player will fall down slopes
    public float GroundCheckRadius;
    private char direction;
    private float timeLeft;
    private Quaternion[] angles;
    public bool onGround;
    public Transform TouchingTerrain;
    int bob = 0;
    // Use this for initialization
    void Start() {
        rig2d = GetComponent<Rigidbody2D>();  //Enables the RigidBody2d component
        animy = GetComponent<Animator>();   //Allows the animator to work
        CreateLists();
        RealMaxspeed = maxspeed;
    }
    public void CreateLists()  //They had to be here because I have no clue what this excuse of a language defines as scope
    {
        angles = new Quaternion[35];  //Creating a list with the angles, more for convinience than having a load of random variable names
        for (int i = 10; i <= 350; i += 10)   //Angles goes up in 10 degree intervals, therefore all comparisons must be made within 10 degrees, I guess we could go up in more intervels such as 5 but this works too.
        {
            angles[bob] = Quaternion.Euler(0, 0, i);
            bob += 1;
        };
        //Add more I guess 
    }

    void checks() //Clock is here, pretty useless really, should be reliant on something else.
    {
        onGround = Physics2D.OverlapCircle(TouchingTerrain.position, GroundCheckRadius, CollideList); //Code to work out if the player is on terrain or not
        if (timeLeft > 0f)
        {
            clock();

        }
        else if (timeLeft < 0f)       //adjust much later
            Y = 0;
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
        //Placeholder
        if (Input.GetKeyDown("a"))
        {
            X = -speed;
            playermoving = true;
        }
        else if (Input.GetKeyDown("d"))
        {
            X = speed;
            playermoving = true;
        }
        if (Input.GetKeyUp("a"))
        {
           // X = 0;
            playermoving = false;
        }
        else if (Input.GetKeyUp("d"))
        {
            //X = 0;
            playermoving = false;
        }
        Y = -GravityStrength;
        transform.Translate(X * Time.deltaTime, 0, 0);
        if (onGround != true)
        {
            transform.Translate(0, Y * Time.deltaTime, 0, 0);
        }

    }

    void Momentum()
    {
        //0-10 Degrees slowdown if character not moving or running too fast on terrain
        if (transform.rotation.z < angles[0].z && transform.rotation.z > -angles[0].z)
        {
            RealMaxspeed = maxspeed;
            if (playermoving == true)
            {
                if (X > RealMaxspeed)
                    X -= 0.1f;
                else if(X<-RealMaxspeed)
                    X += 0.1f;

            }
            else if (playermoving!=true)
            {
                    if (X > 0)
                    {
                        if (X - 0.1f > 0f)
                        {
                            X += -0.1f;
                        }
                        else
                            X = 0;
                    }
                    else
                    {
                        if (X + 0.1f < 0f)
                        {
                            X += 0.f;
                        }
                        else
                            X = 0;
                    }
                
            }


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
