using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script2 : MonoBehaviour
{
    public float speed;
    public float jumpspeed;
    public short jumptimeDev;      //Variable used to limit how many frames of jumping are gained editable in unity to make life easier
    private short jumptime;
    public Vector2 offsets;    //contains x and y movement speeds
    private Rigidbody2D rig2d;
    private Animator animy;
    //Used to define what is ground and if the player is touching it
    public Transform TouchingTerrain;
    public float GroundCheckRadius;
    public LayerMask CollideList;  //could be expanded upon later with more than one layermask
    private bool onGround;

    // Use this for initialization
    void Start()
    {
        rig2d = GetComponent<Rigidbody2D>();   //Acesses it
        animy = GetComponent<Animator>();  //Acesses Animation component for basic left and right movement
    }

    void MoveTo(Vector2 pos)
    {
        transform.position = pos;
    }
//public void RotatePlayer(float zaxis)
//{
      //  private bool please = true;


          //  transform.Rotate(0f, 0f, zaxis);

    

    public void RotatePlayer(float zaxis)
    {
        // transform.Rotate(0f, 0f, zaxis);
        rig2d.freezeRotation = true;   //
        rig2d.MoveRotation(zaxis);
        rig2d.freezeRotation = false;
    }
    void Movement()    //Extremely basic and place holder    Eventual plan is to include no changing direction in air or at least making it harder
    {
        if (Input.GetKeyDown("a"))
        {
            offsets[0] = -speed;
            animy.SetInteger("MoveAnim", -1);
        }
        if (Input.GetKeyDown("d"))
        {
            offsets[0] = speed;
            animy.SetInteger("MoveAnim", 1);
        }

        if (Input.GetKeyDown("space")&& onGround!=false)  //Can't jump whilst on terrain
        {
            offsets[1] = jumpspeed;
            jumptime = jumptimeDev;
        }
            

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            offsets[0] = 0;
        //Could obviously do with tweaking too basic, makes jump look ridiculous

        transform.Translate(offsets[0]*Time.deltaTime, 0, 0);
        if (jumptime == 0 && onGround!=true) 
             offsets[1] = -50f*Time.deltaTime;
        else if(jumptime>0)    //Very basic system for reducing jump time
            jumptime -= 1;
    }

    void FixedUpdate()  //This occurs less than the Update Function per second, saving resources, for applications the player won't notice if it happens less
    {
        onGround=Physics2D.OverlapCircle(TouchingTerrain.position, GroundCheckRadius, CollideList); //Code to work out if the player is on terrain or not.
    }
    void Update()
    {
        Movement();
    }
}

