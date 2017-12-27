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
    public Transform TouchingTerrain;
    public float GroundCheckRadius;
    public LayerMask CollideList; //Temporary, dimension shifting will require lots of these, although I can imagine more blunt ways of doing it
    private bool onGround;
    private int switcher;
    private float timeLeft;
    private char Direction; //Used to define the direction of the player in human terms

    public Quaternion rotation = Quaternion.identity;
    //public Quaternion[] angles = new Quaternion[] { Quaternion.identity, Quaternion.identity, Quaternion.identity };
    public Quaternion[] angles;
    // Use this for initialization
    void Start () {
        rig2d = GetComponent<Rigidbody2D>();
        animy = GetComponent<Animator>();
        CreateLists();
    }
   
    public void CreateLists()
    {
        angles = new Quaternion[] { Quaternion.identity, Quaternion.identity, Quaternion.identity };
        angles[0].eulerAngles = new Vector3(0f, 0f, 10f);
        angles[1].eulerAngles = new Vector3(0f, 0f, 20f);
        angles[2].eulerAngles = new Vector3(0f, 0f, 30f);
    }
    public void MoveTo(Vector2 pos)  //Not actually used, could be useful
    {
        transform.position = pos;
    }

    void checks()
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
        rotation = angles[0];
        Quaternion.Inverse(rotation);
        if (transform.eulerAngles.z==0)
        {
            RealMaxSpeed= maxspeed;
          //  Debug.Log("Why not?");
        }
        if(transform.eulerAngles.z==angles[0].eulerAngles.z)
        {
           RealMaxSpeed = maxspeed * 1.1f;

        }
        else if(transform.eulerAngles.z==angles[1].eulerAngles.z)
        {
            RealMaxSpeed = maxspeed * 1.2f;
        }
        else if(transform.eulerAngles.z==angles[2].eulerAngles.z)
        {
            RealMaxSpeed = maxspeed * 1.3f;
        }
        if (transform.eulerAngles.z == Quaternion.Inverse(angles[0]).eulerAngles.z)
        {
            RealMaxSpeed = maxspeed * 1.1f;
        }

        else if (transform.eulerAngles.z == Quaternion.Inverse(angles[1]).eulerAngles.z)
        {
            RealMaxSpeed = maxspeed * 1.2f;
        }
        else if (transform.eulerAngles.z == Quaternion.Inverse(angles[2]).eulerAngles.z)
        {
            RealMaxSpeed = maxspeed * 1.3f;
        }


        //  Debug.Log(transform.eulerAngles.z);
        Debug.Log(RealMaxSpeed);
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
