using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class PlayerScriptRegret : MonoBehaviour {
    public float maxspeed; //The plain default maxspeed on normal terrain
    public float speed;
    public float RealMaxspeed; //The Maxspeed changed by terrain and angles and state
    public float GravityStrength;
    public float X, Y; //X and Y of the player
    public TerrainObject Block;
    private float raycastdistance = 8.0f;
    private Rigidbody2D rig2d;
    private Animator animy;
    private bool playermoving;
    public LayerMask CollideList; //Temporary, dimension shifting will require lots of these, although I can imagine more blunt ways of doing it
    private float groundslide = 20;
    public float GroundCheckRadius;
    private char direction; //Actual direction of the player
    public char DesiredDir; //The desired direction
    public float slowdown;
    private float timeLeft;
    private float Previous;
    private float groundholder = 1.5f;
    private bool flying = false;
    public bool onGround=false;
    public Transform TouchingTerrain;
    public Transform TouchingTerrain2;
    public Transform TerrainRight;
    public Transform TerrainLeft;
    private Transform JumpTransform;
    private Vector2 PreviousPos;
    private Transform Lastground;
    public float jumpY;
    private bool first;
    System.TimeSpan ts;
    int elapsedtime;
    Stopwatch stopwatch = new Stopwatch();
    System.TimeSpan tss;
    int elapsedjump;
    Stopwatch stopjump = new Stopwatch();
    bool jumping;
    public float JumpValue;
    // Use this for initialization


    void Start()
    {
        rig2d = GetComponent<Rigidbody2D>();  //Enables the RigidBody2d component
        animy = GetComponent<Animator>();   //Allows the animator to work
        Block = FindObjectOfType<TerrainObject>();
        RealMaxspeed = maxspeed;
        stopjump.Start();
        stopwatch.Start();

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        // UnityEngine.Debug.Log("WHYYYYYYYYYYYYY");

        if (coll.gameObject.tag == "block")
        {
            transform.rotation = coll.transform.gameObject.transform.rotation;
            //transform.position = new Vector2(transform.position.x, coll.transform.position.y + 1.4f);
            jumpY = 0;
            if(Previous!=coll.gameObject.transform.position.y)
            {
               transform.Translate(0, -groundholder * Time.deltaTime, 0); //Forces the player back onto terrain if they've fallen off of it.
            }
            Previous = coll.gameObject.transform.position.y;
            onGround = true;
        }
        if (coll.gameObject.tag == "wall" && X > 0)
        {
            if (X > 0)
                X = (X * -0.8f);

            //X = 0;
        }
        else if (coll.gameObject.tag == "wall" && X < 0)
        {
            if (X < 0)
                X = (-X * 0.8f);
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
     //   if (playermoving == true)
      //  {
     //       UnityEngine.Debug.Log("HYYYYYYY");
      //      transform.Translate(0, -groundholder * Time.deltaTime, 0); //Forces the player back onto terrain if they've fallen off of it.
      // }
       RaycastHit2D ray = Physics2D.Raycast(new Vector2(TouchingTerrain.position.x, TouchingTerrain.position.y), Vector2.down , 1f);  //Defining the ray and its path, Trying to offset the ray in testing as it gets stuck in the player object
        if (ray.transform.gameObject.tag != "block")
           onGround = false;
        // Previous = null;
    }
    
    void Awake() // jeff
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
#endif
#if UNITY_STANDALONE_WIN
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
#endif
    }
    void InputScript() //Takes input from the user, changing the variables for modification elsewhere
    {
        //Placeholder
        if (Input.GetKeyDown("a"))
        {
            DesiredDir = 'L';
            playermoving = true;
        }
        else if (Input.GetKeyDown("d"))
        {
            DesiredDir = 'R';
            playermoving = true;
        }
        if (Input.GetKeyUp("a"))
        {
            X = 0;
            Y = 0;
            DesiredDir = 'N';
            playermoving = false;
        }
        else if (Input.GetKeyUp("d"))
        {
            X = 0;
            Y = 0;
            DesiredDir = 'N';
            playermoving = false;
        }
        if (Input.GetKeyDown("space") && onGround == true)
        {
            flying = true;
            jumping = true;
            //Y = JumpValue;
            jumpY = JumpValue;
            JumpTransform = transform;
            transform.Translate(0, 60 * Time.deltaTime, 0, JumpTransform);
        }
        if (DesiredDir == 'R')
        {
            X = speed * Mathf.Cos((float)(transform.rotation.z * 0.01745));
            Y = speed * Mathf.Sin((float)(transform.rotation.z * 0.01745));
        }
        if (DesiredDir == 'L')
        {
            X = -speed * Mathf.Cos((float)(transform.rotation.z * 0.01745));
            Y = -speed * Mathf.Sin((float)(transform.rotation.z * 0.01745));
        }
        transform.Translate(X * Time.deltaTime, Y * Time.deltaTime, 0); //General Movement
        if(onGround==false)
        {
           jumpY += GravityStrength * Time.deltaTime;
           transform.Translate(0, jumpY * Time.deltaTime, 0, Space.World); //Gravity
        }
        //if(Previous!=null)
          //  transform.position = new Vector2(transform.position.x, Previous.transform.position.y + 1.4f);
        /*
         *  if (onGround == false)
         {
             if (first == true)
             {
                 transform.Translate(0, -groundholder * Time.deltaTime, 0); //Forces the player back onto terrain if they've fallen off of it.
                 first = false;
             }
             jumpY += GravityStrength * Time.deltaTime;
             transform.Translate(0, jumpY * Time.deltaTime, 0, Space.World); //Gravity
         }*/
    }

    void Update()
    {
        InputScript();

    }





}