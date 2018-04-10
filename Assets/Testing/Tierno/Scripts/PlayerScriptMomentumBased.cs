using System.Collections;
using System.Collections.Generic;
using System.Linq; //For the count function
using UnityEngine;

public class PlayerScriptMomentumBased : MonoBehaviour {
    public float maxspeed; //A maxspeed for the player, allow it to be changed by factors such as terrain type or angle
    public float speed;
    public float RealMaxSpeed;
    public float gravitystrength; //What strength the gravity should be on angled terrain or just in general
    public Vector2 offsets; //contains the player's X and y movement
    private Rigidbody2D rig2d;
    private Animator animy;
    public float FallRate;  //Used to control how fast the player falls down slopes
    public float SlowDown; //Used to define how the player slows down on flat terrain and on less angled slopes e.t.c
    //These lot are used to define what the player is touching
   // public Transform TouchingTerrain;
    public float GroundCheckRadius;
    public LayerMask CollideList; //Temporary, dimension shifting will require lots of these, although I can imagine more blunt ways of doing it
    public Transform TouchingTerrain;
    private bool onGround;
    private bool AffectedByMomentum; //Used to tell if the momentum script is messing with the player
    private float timeLeft;
    private char Direction; //Used to define the direction of the player in human terms
    //public Quaternion[] angles = new Quaternion[] { Quaternion.identity, Quaternion.identity, Quaternion.identity };
    private Quaternion[] angles;
    // Use this for initialization
    void Start () {
        int bob = 0;  //Placeholder variable used for loops and such
        rig2d = GetComponent<Rigidbody2D>();  //Enables the RigidBody2d component
        animy = GetComponent<Animator>();   //Allows the animator to work
        CreateLists(bob);
        RealMaxSpeed = maxspeed;

    }
   
    public void CreateLists(int bob)  //They had to be here because I have no clue what this excuse of a language defines as scope
    {
        angles = new Quaternion[35];  //Creating a list with the angles, more for convinience than having a load of random variable names
        for(int i=10;i<=350;i+=10)   //Angles goes up in 10 degree intervals, therefore all comparisons must be made within 10 degrees, I guess we could go up in more intervels such as 5 but this works too.
        {
            angles[bob]=Quaternion.Euler(0, 0, i);
            bob += 1;
        };
                                                           //Add more I guess 
    }
    public void MoveTo(Vector2 pos)  //Not actually used, could be useful
    {
        transform.position = pos;
    }

    void checks() //Clock is here, pretty useless really, should be reliant on something else.
    {
        onGround = Physics2D.OverlapCircle(TouchingTerrain.position, GroundCheckRadius, CollideList); //Code to work out if the player is on terrain or not
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
        if(Input.GetKeyDown("a"))   //temporary movement system here, for testing terrain
        {
            Direction = 'L';
        }
        else if (Input.GetKeyDown("d"))
        {
            Direction = 'R';
        }
        if (Input.GetKeyDown("space"))
        {
            rig2d.MoveRotation(angles[2].eulerAngles.z*-1);
        }
        if(Input.GetKeyUp("a"))
        {
            Direction = 'n';
        }
        else if(Input.GetKeyUp("d"))
        {
            Direction = 'n';     //These are so temporary and just copies, it won't work with what I'm planning
        }
        //Controls the player's momentum probably should be moved really. Code is very inefficent and memory intensive, but right now I want it to work and don't paticularily care
        if (Direction == 'L'&& offsets[0]+-speed > -RealMaxSpeed)
            offsets[0] += -speed ;
        else if (Direction == 'R' && offsets[0] + speed < RealMaxSpeed)
            offsets[0] += speed ;
        if (offsets[0] > RealMaxSpeed && Direction == 'N')
            if (offsets[0] - speed < 0)
                offsets[0] = 0;
            //else if

            else
                offsets[0] -= speed;
        else if (offsets[0] < RealMaxSpeed&&Direction=='N')
            offsets[0] += speed;



        transform.Translate(offsets[0] * Time.deltaTime, 0, 0);  //Entirely Temporary
        offsets[1] = -gravitystrength; //PUlls the player down
        if( onGround != true)
         transform.Translate(0, offsets[1] * Time.deltaTime, 0);
    }

    void Momentum() //Not worth looking at except for reference
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
    void Momentum2()
    {
        // if (transform.eulerAngles.z == 0)   //If on a flat plane, resort to the default main speed
        //{
        // RealMaxSpeed = maxspeed;
        //Debug.Log("Why wq22?");
        //}
        //if (transform.eulerAngles.z>=angles[0].z && transform.eulerAngles.z<angles[1].z)
        //{
        //   RealMaxSpeed = maxspeed * 1.1f;
        //    Debug.Log("Why not?");
        //}
        //Very Temporary. Basically just been struggling with Quaternion angles.




        //Max Speed Detection Section
        //If the player is on a plane of less than 10 degrees and more than -10 degrees
        if (transform.rotation.z<angles[0].z && transform.rotation.z>-angles[0].z)
        {
            RealMaxSpeed = maxspeed;
            //Temporary Values, but this stops the player from moving on flat terrain
            if (offsets[0] > 0)   
                if (offsets[0] - SlowDown > 0f)
                    offsets[0] += -SlowDown;
                else
                    offsets[0] = 0;
            else
                if (offsets[0] + SlowDown < 0f)
                    offsets[0] += SlowDown;
                else
                    offsets[0] = 0;
        }


        //To break this down nicely, If the player's Z rotation is more than or equal to 10 or more degrees but inbetween that and 20 degrees do as said. 
        //The second section is an OR statement that does the same thing if it's -10 degrees instead. Simples
        if(transform.rotation.z>=angles[0].z && transform.rotation.z<angles[1].z || transform.rotation.z<=-angles[0].z && transform.rotation.z>-angles[1].z) //10 degrees or more go a bit faster
        {
            RealMaxSpeed = maxspeed * 0.9f;
            AffectedByMomentum = true;
            if (0f+transform.rotation.z>0)  //Differentiating from the different possible directions.
                if(offsets[0]+-FallRate*Time.deltaTime<RealMaxSpeed)
                    offsets[0] += -FallRate*Time.deltaTime; //Positive angles
            else                                    //Negative Angles
                if (offsets[0] + FallRate * Time.deltaTime > RealMaxSpeed)
                    offsets[0] += FallRate * Time.deltaTime;
        }
        else if (transform.rotation.z >= angles[1].z && transform.rotation.z < angles[2].z || transform.rotation.z <= -angles[1].z && transform.rotation.z > -angles[2].z) //20 degrees or more go a bit faster
        {
            RealMaxSpeed = maxspeed * 0.8f;
            AffectedByMomentum = true;
            if (0f + transform.rotation.z > 0)  //Differentiating from the different possible directions.
                if (offsets[0] + -FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += -FallRate*1.2f * Time.deltaTime; //Positive angles
            else                                    //Negative Angles
                if (offsets[0] + FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += FallRate * 1.2f * Time.deltaTime;
        }
       else if (transform.rotation.z >= angles[2].z && transform.rotation.z < angles[3].z || transform.rotation.z <= -angles[2].z && transform.rotation.z > -angles[3].z) //30 degrees or more go a bit faster
        {
            RealMaxSpeed = maxspeed * 0.7f;
            AffectedByMomentum = true;
            if (0f + transform.rotation.z > 0)  //Differentiating from the different possible directions.
                if (offsets[0] + -FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += -FallRate * 1.4f* Time.deltaTime; //Positive angles
            else                                    //Negative Angles
                if (offsets[0] + FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += FallRate * 1.4f * Time.deltaTime;
        }
       else if (transform.rotation.z >= angles[3].z && transform.rotation.z < angles[4].z || transform.rotation.z <= -angles[3].z && transform.rotation.z > -angles[4].z) //40 degrees or more go a bit faster
        {
            RealMaxSpeed = maxspeed * 0.6f;
            AffectedByMomentum = true;
            if (0f + transform.rotation.z > 0)  //Differentiating from the different possible directions.
                if (offsets[0] + -FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += -FallRate * 1.6f * Time.deltaTime; //Positive angles
            else                                    //Negative Angles
                if (offsets[0] + FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += FallRate * 1.6f * Time.deltaTime;
        }
       else if (transform.rotation.z >= angles[4].z && transform.rotation.z < angles[5].z || transform.rotation.z <= -angles[4].z && transform.rotation.z > -angles[5].z) //50 degrees or more go a bit faster
        {
            RealMaxSpeed = maxspeed * 0.5f;
            AffectedByMomentum = true;
            if (0f + transform.rotation.z > 0)  //Differentiating from the different possible directions.
                if (offsets[0] + -FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += -FallRate * 1.8f * Time.deltaTime; //Positive angles
            else                                    //Negative Angles
                if (offsets[0] + FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += FallRate * 1.8f * Time.deltaTime;
        }
        else if (transform.rotation.z >= angles[5].z && transform.rotation.z < angles[6].z || transform.rotation.z <= -angles[5].z && transform.rotation.z > -angles[6].z) //60 degrees or more go a bit faster
        {
            RealMaxSpeed = maxspeed * 0.4f;
            AffectedByMomentum = true;
            if (0f + transform.rotation.z > 0)  //Differentiating from the different possible directions.
                if (offsets[0] + -FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += -FallRate * 2f * Time.deltaTime; //Positive angles
            else                                    //Negative Angles
                if (offsets[0] + FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += FallRate * 2f * Time.deltaTime;
        }
       else if (transform.rotation.z >= angles[6].z && transform.rotation.z < angles[7].z || transform.rotation.z <= -angles[6].z && transform.rotation.z > -angles[7].z) //70 degrees or more go a bit faster
        {
            RealMaxSpeed = maxspeed * 0.3f;
            AffectedByMomentum = true;
            if (0f + transform.rotation.z > 0)  //Differentiating from the different possible directions.
                if (offsets[0] + -FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += -FallRate * 2.2f * Time.deltaTime; //Positive angles
            else                                    //Negative Angles
                if (offsets[0] + FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += FallRate * 2.2f * Time.deltaTime;
        }
      else  if (transform.rotation.z >= angles[7].z && transform.rotation.z < angles[8].z || transform.rotation.z <= -angles[7].z && transform.rotation.z > -angles[8].z) //80 degrees or more go a bit faster
        {
            RealMaxSpeed = maxspeed * 0.2f;
            AffectedByMomentum = true;
            if (0f + transform.rotation.z > 0)  //Differentiating from the different possible directions.
                if (offsets[0] + -FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += -FallRate * 2.4f * Time.deltaTime; //Positive angles
            else                                    //Negative Angles
                if (offsets[0] + FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += FallRate * 2.4f * Time.deltaTime;
        }
       else if (transform.rotation.z >= angles[8].z && transform.rotation.z < angles[9].z || transform.rotation.z <= -angles[8].z && transform.rotation.z > -angles[9].z) //90 degrees or more go a bit faster
        {
            RealMaxSpeed = maxspeed * 0.1f;
            AffectedByMomentum = true;
            if (0f + transform.rotation.z > 0)  //Differentiating from the different possible directions.
                if (offsets[0] + -FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += -FallRate * 2.6f * Time.deltaTime; //Positive angles
            else                                    //Negative Angles
                if (offsets[0] + FallRate * Time.deltaTime < RealMaxSpeed)
                    offsets[0] += FallRate * 2.6f * Time.deltaTime;
        }


       





    }
    // Update is called once per frame
    void Update () {
        Movement();
        Momentum2();
        //Momentum();
	}


    void FixedUpdate()
    {
        checks();  //Checks that only need to be called once a frame, just putting here to make it look nicer.
    }
}
