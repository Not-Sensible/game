using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
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
    private float groundslide = 12;
    public float GroundCheckRadius;
    private char direction; //Actual direction of the player
    private char DesiredDir; //The desired direction
    public float slowdown;
    private float timeLeft;
    private Quaternion[] angles;
    public bool onGround;
    public Transform TouchingTerrain;
    int bob = 0;
    System.TimeSpan ts;
    int elapsedtime;
    Stopwatch stopwatch = new Stopwatch();
    // Use this for initialization
    void Start() {
        rig2d = GetComponent<Rigidbody2D>();  //Enables the RigidBody2d component
        animy = GetComponent<Animator>();   //Allows the animator to work
        CreateLists();
        RealMaxspeed = maxspeed;
        stopwatch.Start();
        
    }
    void OnDrawGizmosSelected() //Just used to draw the path of the ray for debugging reasons, could be used for other stuff if you want. IF SOMEONE ELSE ACTUALLY LOOKED AT THIS THAT IS >:( anger
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector2.down) * 15;
        Gizmos.DrawRay(transform.position, direction);
    }
    private void Raycasting()  //This script is being used to test the terrain beneath the player and translate the player to the angle beneath them, preventing issues with terrain.
    {
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(TouchingTerrain.position.x,TouchingTerrain.position.y), Vector2.down*15);  //Defining the ray and its path, Trying to offset the ray in testing as it gets stuck in the player object
        UnityEngine.Debug.Log(ray.transform.gameObject.transform.rotation.z);
        if (ray)  //If true do this
            transform.rotation = ray.transform.gameObject.transform.rotation; //Sets the player's angle to the terrain
        
    }

    /*✋✋✋✋✋hol' up hol' up ✋✋ looks 👀 like we got a master 🎓 memer 🐸🐸🐸 over here 👈👈👈👩👩 hold on to your 👙panties👙ladies!💋💁fuccbois better back the hell ⬆️up⬆️ this absolute 🙀🙀🙀 maaaaaadman!!1! 👹 all you other aspiring 🌽🌽 memers👽👻💀 mmmight as wwwell give up! 👎👎👎👎cuse 👉this guy👈is as good 👌👌👌as it gets! 👏👏👏😹😹

OMG 😱😱😱 BRO👬 CALM 😴😴 DOWN BRO ⬇️⬇️ SIMMER ☕️☕️ DOWN⬇️⬇️ U WANNA KNOW Y⁉️ BC 💁💁 IT WAS JUST A PRANK 😂😂😂 😛😜 HAHAHA GOT U 👌👌 U FUKIN RETARD 😂😁😁THERE'S A CAMERA 📹📷 RIGHT OVER 👈👇👆☝️ THERE 📍U FAGOT 👨‍❤️‍💋‍👨👨‍❤️‍💋‍👨👐WE 👨‍👨‍👦 GOT U BRO👬. I BET U DIDNT 🙅🙅NOE 💆HOW 2⃣ REACT WHEN MY 🙋 BRO DESMOND 😎😎 CAME UP ⬆️ TO U AND 💦💦😫😫 JIZZED ALL OVER UR 👖👖 SWEET JEANS 😂😂 IT WAS SO FUNNY 😂😛😀😀😅 NOW U HAVE 🙋👅👅 SUM BABY👶👶 GRAVY 💦🍲 ALL OVER THEM SHITS😵😵

Merry ⛄️🌟 Christmas Babe 🔥🍑👅 I hope 🙏🏼👏🏼 Santa comes 👄💦😩 to visit you 👣👟and give 👍🏼 you a package 🙈📦💌💦. Hope you were a 😇🙂 good girl 😛🍆 this year instead of the😽 usual 😼 naughty 🙄 girl 💦🍑👅😛😫🔥🔥. Santa is definitely ✊🏻 coming 💧tonight 🎅🏿🎅🏻😳😏 and he's gonna 😍😘 stuff your stocking 😝👌🏽👈🏽 with goodies 💋💄👙👗 tonight on this 🎄Christmas 🎄night ❄️⛄️☃🌨💫. Santa 🎅🏻 is gonna 💪🏿💪🏼✊🏻squeeze 🖖🏻down your 👧🏽 😰 narrow 😛😍chimney 🏡🏠 and show you 👀 that you've been a very👸🏽👸🏽 naughty 😏😫😝 girl. Then his 💁🏼 helper 😬😏 Boy 🍆🙃🙂 is gonna 🎄sleigh you baby 😛😏😲👐🏼🙌🏻 and inspect 🕵🔎🔍 that 🍑 sweet 💦 ass🍑 because that's what 👉🏽you👈🏽 want for Christmas 🍑💦😛🔥😏😍🍆👅👀 Santa 🎅🏻 is cumin😻👽 to town 🏢🏦🏬🏚🏡🏠🏣🏤 the clock 🕐 is ticking 🙄 be ready 😏😛🍆 Santa is cumin down↘️⬇️↙️ your👌🏽😍 chimney🖖🏻👅 tonight 😮and he's gonna 😨drown in that chimney 🤐😰💦💧☔️🏊🏼🏄🏼🚣🏼 of yours 🛀🏼🍆🍑 SLEIGH 🎄🎄 🎅🏻SANTA🎅🏻 🎄🎄 SLEIGH 🍆😩💦👩‍❤️‍💋‍👩
    */
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

    public void clock()   //Clock system for the character, can be used for anything. //But doesn't actually work
    {
        System.TimeSpan ts = stopwatch.Elapsed;
        int elapsedtime = ts.Seconds;
    }
    void checks() //Clock is here, pretty useless really, should be reliant on something else.
    {
        onGround = Physics2D.OverlapCircle(TouchingTerrain.position, GroundCheckRadius, CollideList); //Code to work out if the player is on terrain or not
        if (onGround != true)
        {
            stopwatch.Start();
            System.TimeSpan ts = stopwatch.Elapsed;
            int elapsedtime = ts.Seconds;
           // UnityEngine.Debug.Log(elapsedtime);
            if (elapsedtime == 5)
            {
                Raycasting();
                stopwatch.Reset();
                elapsedtime = 0;
            }

        }
        else
            stopwatch.Reset();
              

            

    }


    void InputScript()
    {
        //Placeholder
        if (Input.GetKeyDown("a"))
        {
            DesiredDir = 'L';
            playermoving = true;
        }
        if (Input.GetKeyDown("d"))
        {
            DesiredDir = 'R';
            playermoving = true;
        }
        if (Input.GetKeyUp("a"))
        {
            // X = 0;
            DesiredDir = 'N';
            playermoving = false;
        }
        if (Input.GetKeyUp("d"))
        {
            //X = 0;
            DesiredDir = 'N';
            playermoving = false;
        }
        Movement(DesiredDir, onGround);
        Y = -GravityStrength;
        transform.Translate(X * Time.deltaTime, 0, 0);
        if (onGround != true)
        {
            transform.Translate(0, Y * Time.deltaTime, 0, 0);
        }

    }
    void Movement(char dir,bool IsOnGround)
    {
        if (onGround == true && dir == 'L' && X > -RealMaxspeed)
            X += -speed * Time.deltaTime;
        else if (onGround == true && dir == 'R' && X < RealMaxspeed)
            X += speed * Time.deltaTime;

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
        //If the player is between 10 degrees and -10 degrees
        if (onGround == true)
        {
            if (DesiredDir == 'R' && X < 0 || DesiredDir == 'L' && X > 0)
            {
                playermoving = false;
            }
            if (transform.rotation.z < angles[0].z && transform.rotation.z > -angles[0].z)
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

            //Behaviours for all the different angles go here.
            if (transform.rotation.z >= angles[0].z && transform.rotation.z <= angles[1].z || transform.rotation.z <= -angles[0].z && transform.rotation.z >= -angles[1].z) //10 degrees
            {
                TerrainSlowDown(slowdown * 1.1f);

            }
            else if (transform.rotation.z >= angles[1].z && transform.rotation.z <= angles[2].z || transform.rotation.z <= -angles[0].z && transform.rotation.z >= -angles[1].z) //20
            {
                TerrainSlowDown(slowdown * 1.2f);

            }
            else if (transform.rotation.z >= angles[2].z && transform.rotation.z <= angles[3].z || transform.rotation.z <= -angles[1].z && transform.rotation.z >= -angles[2].z) //30
            {
                TerrainSlowDown(slowdown * 1.3f);

            }
            else if (transform.rotation.z >= angles[3].z && transform.rotation.z <= angles[4].z || transform.rotation.z <= -angles[2].z && transform.rotation.z >= -angles[3].z) //40
            {
                TerrainSlowDown(slowdown * 1.4f);

            }
            else if (transform.rotation.z >= angles[4].z && transform.rotation.z <= angles[5].z || transform.rotation.z <= -angles[4].z && transform.rotation.z >= -angles[5].z) //50
            {
                TerrainSlowDown(slowdown * 1.5f);

            }
            else if (transform.rotation.z >= angles[5].z && transform.rotation.z <= angles[6].z || transform.rotation.z <= -angles[5].z && transform.rotation.z >= -angles[6].z) //60
            {
                TerrainSlowDown(slowdown * 1.6f);

            }
            else if (transform.rotation.z >= angles[6].z && transform.rotation.z <= angles[7].z || transform.rotation.z <= -angles[6].z && transform.rotation.z >= -angles[7].z) //70
            {
                TerrainSlowDown(slowdown * 1.7f);

            }
            else if (transform.rotation.z >= angles[7].z && transform.rotation.z <= angles[8].z || transform.rotation.z <= -angles[7].z && transform.rotation.z >= -angles[8].z) //80
            {
                TerrainSlowDown(slowdown * 1.8f);

            }
            else if (transform.rotation.z >= angles[8].z && transform.rotation.z <= angles[9].z || transform.rotation.z <= -angles[8].z && transform.rotation.z >= -angles[9].z) //90
            {
                TerrainSlowDown(slowdown * 1.9f);

            }
        }
    }



    // Update is called once per frame
    void Update () {
        InputScript();
        Momentum();
	}
    void FixedUpdate()
    {
        //UnityEngine.Debug.Log(elapsedtime);
            
        checks();
    }
}
