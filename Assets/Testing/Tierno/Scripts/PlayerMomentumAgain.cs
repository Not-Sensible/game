using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class PlayerMomentumAgain : MonoBehaviour {
    public float maxspeed; //The plain default maxspeed on normal terrain
    public float speed;
    public float RealMaxspeed; //The Maxspeed changed by terrain and angles and state
    public float GravityStrength;
    public bool move = true;
    public float X, Y; //X and Y of the player
    public TerrainObject Block;
    private float raycastdistance=8.0f;
    private Rigidbody2D rig2d;
    private Animator animy;
    private bool playermoving;
    public LayerMask CollideList; //Temporary, dimension shifting will require lots of these, although I can imagine more blunt ways of doing it
    public LayerMask wall;
    private float groundslide = 25;
    public float GroundCheckRadius;
    private char direction; //Actual direction of the player
    public char DesiredDir; //The desired direction
    public float slowdown;
    private float timeLeft;
    private float groundholder = 10.0f;
    public bool flying = false;
    public bool onGround;
    public AudioClip jumpsound;
    private AudioSource source;
    public AudioClip Death;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;
    public Transform TouchingTerrain;
    public Transform left;
    public Transform right;
    public Transform top;
    public Transform TouchingTerrain2;
    public Transform TerrainRight;
    public Transform TerrainLeft;
    public bool moveright = true;
    public bool moveleft = true;
    private Transform JumpTransform;
    private Vector2 PreviousPos;
    private Transform Lastground;
    public float jumpY;
    private bool stop=false;
    System.TimeSpan ts;
    int elapsedtime;
    Stopwatch stopwatch = new Stopwatch();
    System.TimeSpan tss;
    int elapsedjump;
    Stopwatch stopjump = new Stopwatch();
    bool jumping;
    public float JumpValue;
    // Use this for initialization


    void Start() {
        rig2d = GetComponent<Rigidbody2D>();  //Enables the RigidBody2d component
        animy = GetComponent<Animator>();   //Allows the animator to work
        Block = FindObjectOfType<TerrainObject>();
        RealMaxspeed = maxspeed;
        stopjump.Start();
        stopwatch.Start();
        source = GetComponent<AudioSource>();

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
    public void onDeath()
    {
        source.PlayOneShot(Death, 1.0f);
    }
    void Collisions()
    {
        RaycastHit2D ray = Physics2D.Raycast(left.position, Vector2.left, 0.01f);
        if (ray == true && ray.transform.gameObject.tag == "Wall")
        {
            if(X<0)
                X = 0;
            moveleft = false;
        }
        else
            moveleft = true;

        RaycastHit2D ray2 = Physics2D.Raycast(right.position, Vector2.right, 0.01f);
        if (ray2 == true && ray2.transform.gameObject.tag == "Wall")
        {
            if (X > 0)
                X = 0;
            moveright = false;
        }
        else
            moveright = true;
        RaycastHit2D ray3 = Physics2D.Raycast(top.position, Vector2.up, 0.01f);
        if (ray3 == true && ray3.transform.gameObject.tag == "Wall")
        {
            jumpY = 0;
            jumping = false;
        }
        /*bool temp=Physics2D.OverlapCircle(new Vector2(TerrainRight.position.x, TerrainRight.position.y), GroundCheckRadius, wall); //Code to work out if the player is on terrain or not
        if (temp == true)
        {
            UnityEngine.Debug.Log("WHY");
            X = 0;
            moveright = false;
        }
        else
            moveright = true;
        Physics2D.OverlapCircle(new Vector2(TerrainLeft.position.x, TerrainLeft.position.y), GroundCheckRadius, wall); //Code to work out if the player is on terrain or not
       
        if (temp == true)
        {
            X = 0;
            moveleft = false;
        }
        else
            moveleft = true;*/
    }
    void AngleCheck() //This is used to work out if the player is on a 60 degree angle, if they are, it checks with a raycast if the next block is 90 degrees or not, as the player usually falls if it is.
    {
        if (transform.rotation.eulerAngles.z >= 60 && transform.rotation.eulerAngles.z <= 80 || transform.rotation.eulerAngles.z <= 300 && transform.rotation.eulerAngles.z >= 280) //Checks angles
        {
            if (X < 0)  //Works out if the player is moving left
            {
                RaycastHit2D ray = Physics2D.Raycast(new Vector2(TerrainLeft.position.x, TerrainLeft.position.y), Vector2.right * 15,raycastdistance); //Raycasts from the left floating orb boi
                if (ray == true && ray.transform.gameObject.tag == "block" && ray.transform.gameObject.transform.rotation.eulerAngles.z == 90.0f)  //Checks if the object detected is infact a block and that the rotation is 90 degrees
                {
                    transform.rotation = ray.transform.gameObject.transform.rotation; //Sets the player's roation to that block.
                }
            }
            if (X > 0)  //Sees if the player is moving right
            {
                RaycastHit2D ray = Physics2D.Raycast(new Vector2(TerrainRight.position.x, TerrainRight.position.y), Vector2.left * 15, raycastdistance);  //Raycasts from the Right floating orb boi
                if (ray == true && ray.transform.gameObject.tag == "block" && ray.transform.gameObject.transform.rotation.eulerAngles.z == 270.0f) //Checks if the object detected is infact a block and that the rotation is -90 degrees
                {
                    transform.rotation = ray.transform.gameObject.transform.rotation;//Sets the player's roation to that block.
                }
            }

        }
        /*else if (transform.rotation.eulerAngles.z ==90.0f|| transform.rotation.eulerAngles.z==270.0f)
        {
            if (X < 0 && transform.position.y < PreviousPos.y)
            {
                RaycastHit2D ray = Physics2D.Raycast(new Vector2(TerrainLeft.position.x, TerrainLeft.position.y), Vector2.right * 15); //Raycasts from the left floating orb boi
                if (ray == true && ray.transform.gameObject.tag == "block" && ray.transform.gameObject.transform.rotation.eulerAngles.z == 60.0f)  //Checks if the object detected is infact a block and that the rotation is 60 degrees
                {
                    transform.rotation = ray.transform.gameObject.transform.rotation; //Sets the player's roation to that block.
                }
            }
            if (X > 0 && transform.position.y < PreviousPos.y)
            {
                RaycastHit2D ray = Physics2D.Raycast(new Vector2(TerrainRight.position.x, TerrainRight.position.y), Vector2.left * 15); //Raycasts from the Right floating orb boi
                if (ray == true && ray.transform.gameObject.tag == "block" && ray.transform.gameObject.transform.rotation.eulerAngles.z == 300.0f)  //Checks if the object detected is infact a block and that the rotation is 60 degrees
                {
                    transform.rotation = ray.transform.gameObject.transform.rotation; //Sets the player's roation to that block.
                }
            }*/
        //}

    }
    void OnDrawGizmosSelected() //Just used to draw the path of the ray for debugging reasons, could be used for other stuff if you want. IF SOMEONE ELSE ACTUALLY LOOKED AT THIS THAT IS >:( anger
    {
        Gizmos.color = Color.red;
        Vector3 direction = TerrainRight.TransformDirection(Vector2.down) * 15;
        Gizmos.DrawRay(TerrainRight.position, direction);
    }
    private void RaycastingTerrain()  //This script is being used to test the terrain beneath the player and translate the player to the angle beneath them, preventing issues with terrain.
    {
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(TouchingTerrain.position.x,TouchingTerrain.position.y), Vector2.down*15, raycastdistance/2);  //Defining the ray and its path, Trying to offset the ray in testing as it gets stuck in the player object
        if (ray == true && ray.transform.gameObject.tag == "block" && ray.transform.gameObject.transform.eulerAngles.z!=90.0f || ray == true && ray.transform.gameObject.tag == "block" && ray.transform.gameObject.transform.eulerAngles.z != 270.0f )  //If true do this
            transform.rotation = ray.transform.gameObject.transform.rotation; //Sets the player's angle to the terrain
        else
            transform.rotation= Quaternion.Euler(0,0,0);
        
    }
    private GameObject raycastreturn()
    {
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(TouchingTerrain.position.x, TouchingTerrain.position.y), Vector2.down * 15, raycastdistance);
        if (ray == true && ray.transform.gameObject.transform.tag == "block")
        {
            return ray.transform.gameObject;
        }
        return ray.transform.gameObject;
    }
    bool RayCastCheck()   //This is used to check if the player is on the same angle as the terrain directly beneath them
    {   //The Rest of the function is the same as the raycast check above
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(TouchingTerrain.position.x, TouchingTerrain.position.y), Vector2.down * 15, 4);
        if (ray == true && onGround==true)
        {
            float d = (float)System.Math.Sqrt(System.Math.Pow(ray.transform.gameObject.transform.position.x - TouchingTerrain.position.x, 2) + System.Math.Pow(ray.transform.gameObject.transform.position.y - TouchingTerrain.position.y, 2));
            if (d < 0.3f && ray.transform.gameObject.transform.rotation.eulerAngles.z <= 30.0f || d < 0.3f && ray.transform.gameObject.transform.rotation.eulerAngles.z >= 330.0f)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.4230019f);
            }
            else if (d > 0.7f && ray.transform.gameObject.transform.rotation.eulerAngles.z <= 30.0f || d > 0.7f && ray.transform.gameObject.transform.rotation.eulerAngles.z >= 330.0f)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
            }
            
        }
        // if (ray.transform.gameObject.transform.rotation.eulerAngles.z == 0)
           // transform.position = new Vector2(transform.position.x, transform.position.y + GroundCheckRadius);
        if (ray == true && ray.transform.gameObject.transform.tag=="block")
        {
            transform.rotation = ray.transform.gameObject.transform.rotation;
            return (true);
        }
        else if (ray == true && ray.transform.gameObject.transform.tag == "block"   && ray.transform.gameObject.transform.eulerAngles.z!=90.0f || ray == true && ray.transform.gameObject.transform.tag == "block"  && ray.transform.gameObject.transform.eulerAngles.z != 270.0f)
        {
            transform.rotation = ray.transform.gameObject.transform.rotation;
            return (true);
        }
        //else if(ray ==true && ray.transform.gameObject.transform.tag=="block" && flying==true && ray.transform.gameObject.transform.rotation.eulerAngles.z < 60.0f || ray == true && ray.transform.gameObject.transform.tag == "block" && flying == true && ray.transform.gameObject.transform.rotation.eulerAngles.z > 300.0f )
        //{
        //  transform.rotation = ray.transform.gameObject.transform.rotation;
        //return (true);
        //}
        //   else if(ray == true && ray.transform.gameObject.transform.tag == "block" && jumping != true && ray.transform.gameObject.transform.rotation.eulerAngles.z <= 45 && ray == true && ray.transform.gameObject.transform.tag == "block" && jumping != true && ray.transform.gameObject.transform.rotation.eulerAngles.z >= 315) 
        //{
        //   transform.rotation = ray.transform.gameObject.transform.rotation;
        // }
        return false;
        
    }

    /*✋✋✋✋✋hol' up hol' up ✋✋ looks 👀 like we got a master 🎓 memer 🐸🐸🐸 over here 👈👈👈👩👩 hold on to your 👙panties👙ladies!💋💁fuccbois better back the hell ⬆️up⬆️ this absolute 🙀🙀🙀 maaaaaadman!!1! 👹 all you other aspiring 🌽🌽 memers👽👻💀 mmmight as wwwell give up! 👎👎👎👎cuse 👉this guy👈is as good 👌👌👌as it gets! 👏👏👏😹😹

OMG 😱😱😱 BRO👬 CALM 😴😴 DOWN BRO ⬇️⬇️ SIMMER ☕️☕️ DOWN⬇️⬇️ U WANNA KNOW Y⁉️ BC 💁💁 IT WAS JUST A PRANK 😂😂😂 😛😜 HAHAHA GOT U 👌👌 U FUKIN RETARD 😂😁😁THERE'S A CAMERA 📹📷 RIGHT OVER 👈👇👆☝️ THERE 📍U FAGOT 👨‍❤️‍💋‍👨👨‍❤️‍💋‍👨👐WE 👨‍👨‍👦 GOT U BRO👬. I BET U DIDNT 🙅🙅NOE 💆HOW 2⃣ REACT WHEN MY 🙋 BRO DESMOND 😎😎 CAME UP ⬆️ TO U AND 💦💦😫😫 JIZZED ALL OVER UR 👖👖 SWEET JEANS 😂😂 IT WAS SO FUNNY 😂😛😀😀😅 NOW U HAVE 🙋👅👅 SUM BABY👶👶 GRAVY 💦🍲 ALL OVER THEM SHITS😵😵

Merry ⛄️🌟 Christmas Babe 🔥🍑👅 I hope 🙏🏼👏🏼 Santa comes 👄💦😩 to visit you 👣👟and give 👍🏼 you a package 🙈📦💌💦. Hope you were a 😇🙂 good girl 😛🍆 this year instead of the😽 usual 😼 naughty 🙄 girl 💦🍑👅😛😫🔥🔥. Santa is definitely ✊🏻 coming 💧tonight 🎅🏿🎅🏻😳😏 and he's gonna 😍😘 stuff your stocking 😝👌🏽👈🏽 with goodies 💋💄👙👗 tonight on this 🎄Christmas 🎄night ❄️⛄️☃🌨💫. Santa 🎅🏻 is gonna 💪🏿💪🏼✊🏻squeeze 🖖🏻down your 👧🏽 😰 narrow 😛😍chimney 🏡🏠 and show you 👀 that you've been a very👸🏽👸🏽 naughty 😏😫😝 girl. Then his 💁🏼 helper 😬😏 Boy 🍆🙃🙂 is gonna 🎄sleigh you baby 😛😏😲👐🏼🙌🏻 and inspect 🕵🔎🔍 that 🍑 sweet 💦 ass🍑 because that's what 👉🏽you👈🏽 want for Christmas 🍑💦😛🔥😏😍🍆👅👀 Santa 🎅🏻 is cumin😻👽 to town 🏢🏦🏬🏚🏡🏠🏣🏤 the clock 🕐 is ticking 🙄 be ready 😏😛🍆 Santa is cumin down↘️⬇️↙️ your👌🏽😍 chimney🖖🏻👅 tonight 😮and he's gonna 😨drown in that chimney 🤐😰💦💧☔️🏊🏼🏄🏼🚣🏼 of yours 🛀🏼🍆🍑 SLEIGH 🎄🎄 🎅🏻SANTA🎅🏻 🎄🎄 SLEIGH 🍆😩💦👩‍❤️‍💋‍👩
    */

    public void clock()   //Clock system for the character, can be used for anything. //But doesn't actually work
    {
        System.TimeSpan ts = stopwatch.Elapsed;
        int elapsedtime = ts.Seconds;
    }
    void checks() //Clock is here, pretty useless really, should be reliant on something else.
    {
        onGround = Physics2D.OverlapCircle(new Vector2(TouchingTerrain.position.x,TouchingTerrain.position.y), GroundCheckRadius, CollideList); //Code to work out if the player is on terrain or not
        //onGround = Physics2D.OverlapCircle(TouchingTerrain2.position, GroundCheckRadius, CollideList); //Code to work out if the player is on terrain or not
        if(onGround==true)
        {
            Lastground = transform;
        }
        if (onGround != true)
        {
            stopwatch.Start();
            System.TimeSpan ts = stopwatch.Elapsed;
            int elapsedtime = ts.Seconds;
            // UnityEngine.Debug.Log(elapsedtime);
            if (elapsedtime == 2)
            {
                RaycastingTerrain();
                stopwatch.Reset();
                elapsedtime = 0;
            }

        }
        else
        {

            Y = 0;
            stopwatch.Reset();
        }
        if(jumping==true)
        {
           // Jump();
        }
        RayCastCheck();
        AngleCheck();
        PreviousPos = new Vector2(transform.position.x, transform.position.y);
        if (onGround == true && jumping == false)
        {
            //GravityStrength = 1.0f;
            //flying = false;
        }
        //TouchingTerrain.rotation = transform.rotation;
        

            

    }

    
    void InputScript()
    {
        //Placeholder
        if (Input.GetKeyDown("a") && moveleft==true && move==true)
        {
            DesiredDir = 'L';
            playermoving = true;
        }
        else if (Input.GetKeyDown("d") && moveright == true && move == true)
        {
            DesiredDir = 'R';
            playermoving = true;
        }
        if (Input.GetKeyUp("a") || move==false)
        {
            // X = 0;
            DesiredDir = 'N';
            playermoving = false;
        }
        else if (Input.GetKeyUp("d")|| move==false)
        {
            //X = 0;
            DesiredDir = 'N';
            playermoving = false;
        }
        if (Input.GetKeyDown("space") && onGround==true && move==true)
        {
            flying = true;
            jumping = true;
            //Y = JumpValue;
            source.PlayOneShot(jumpsound, 0.4f);
            jumpY = JumpValue;
            JumpTransform = transform;
            transform.Translate(0, 60*Time.deltaTime,0,JumpTransform);
        }
        Movement(DesiredDir, onGround);
        //Y =-GravityStrength;
        if(onGround==true)
            transform.Translate(X * Time.deltaTime, 0, 0,Lastground);
        else if(onGround==false && jumping==true)
        {
          //  transform.Translate(X * Time.deltaTime, 0, 0, JumpTransform);
        }
        else
        {
            transform.Translate(X * Time.deltaTime, 0, 0, Space.World);
        }
        if (onGround != true && flying==false)
        {
            transform.Translate(0, -groundholder * Time.deltaTime, 0);
        }
        else if(onGround!=true && flying==true)
        {
            if (Y > -50 && jumpY <= 0)
            {
                Y += (-GravityStrength *3.0f * Time.deltaTime);
                transform.Translate(0, Y * Time.deltaTime, 0, Space.World);
            }
            else if (JumpTransform != null)
            {
                jumping = false;
                transform.Translate(0, jumpY * Time.deltaTime, 0, JumpTransform);
                jumpY += (-GravityStrength * 3 * Time.deltaTime);
            }
        }

    }
    void Movement(char dir,bool IsOnGround)
    {

        if (onGround == true && dir == 'L' && X > -RealMaxspeed && moveleft!=false)
            X += -speed * Time.deltaTime;
        else if (onGround == true && dir == 'R' && X < RealMaxspeed && moveright != false)
            X += speed * Time.deltaTime;
        if (onGround != true && dir == 'L' && X > -RealMaxspeed && moveleft != false)
            X += (-speed * 0.55f) * Time.deltaTime;
        else if (onGround != true && dir == 'R' && X < RealMaxspeed && moveright != false)
            X += (speed*0.55f) * Time.deltaTime;
    }

    void TerrainSlowDown(float slowdownlocal)
    {

        if (transform.rotation.z > 0) //If the player's Direction is Right or Left
            X += -slowdownlocal * Time.deltaTime;  //If the Player is climbing right, they should start falling left
        else
            X += slowdownlocal * Time.deltaTime; //If the Player is climbing Left, they should start falling right



    }

    void Momentum()
    {
        if (onGround == true)
        {
            if (DesiredDir == 'R' && X < 0 || DesiredDir == 'L' && X > 0)
            {
                playermoving = false;
            }
            if (transform.rotation.z < Quaternion.Euler(0, 0, 10).z && transform.rotation.z > Quaternion.Euler(0, 0, -10).z)
            {
                RealMaxspeed = maxspeed;
                if (playermoving == true)
                {
                    if (X > RealMaxspeed)   //If the player is moving at a speed faster than the maxspeed designated by the terrain, slow them down.
                        X -= groundslide * Time.deltaTime;
                    else if (X < -RealMaxspeed)
                        X += groundslide * Time.deltaTime;

                }
                else if (playermoving != true)
                {
                    if (X > 0)
                    {
                        if (X - 0.1f > 0f)
                        {
                            X += -groundslide * Time.deltaTime;
                        }
                       else
                            X = 0;
                    }
                    else
                    {
                        if (X + 0.1f < 0f)
                        {
                            X += groundslide * Time.deltaTime;
                        }
                        else
                            X = 0;
                    }

                }




            }
        }
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(TouchingTerrain.position.x, TouchingTerrain.position.y), Vector2.down * 15, raycastdistance);

        if (ray == true && ray.transform.gameObject.transform.tag == "block")
        {
            float temp = 340;
            for (float i = 10; i < 100; i += 10)
            {
                if (ray.transform.gameObject.transform.rotation.eulerAngles.z >= i && ray.transform.gameObject.transform.rotation.eulerAngles.z < i + 10)

                {
                    TerrainSlowDown(slowdown * (1 + (i / 100)));
                }
                else if (ray.transform.gameObject.transform.rotation.eulerAngles.z < 360 && ray.transform.gameObject.transform.rotation.eulerAngles.z >= 270)
                {
                    if (ray.transform.gameObject.transform.rotation.eulerAngles.z <= i + temp && ray.transform.gameObject.transform.rotation.eulerAngles.z >= i + temp)
                    {
                        TerrainSlowDown(slowdown * (1 + (i / 100)));
                    }

                    temp -= 20;
                }
            }
        }
    }






    // Update is called once per frame
    void Update () {
        Collisions();
        InputScript();
        Momentum();
        checks();
    }
    void FixedUpdate()
    {
       
    }
}
