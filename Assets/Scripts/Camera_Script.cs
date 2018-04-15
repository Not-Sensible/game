using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour {
    public PlayerMomentumAgain PlayerCharacter;  //Accesses the player script
    public GameObject FollowThis;
    public GameObject Engame;
    public float camerapostion;
	// Use this for initialization
	void Start () {
        //PlayerCharacter = FindObjectOfType<PlayerMomentumAgain>();
	}

    public void FollowNewObject(GameObject obj)
    {
        FollowThis = obj;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(FollowThis.transform.position.x, FollowThis.transform.position.y,camerapostion );
        if(PlayerCharacter.move==false)
        {
            FollowThis = Engame;
            //camerapostion = -15;
        }
	}
}
