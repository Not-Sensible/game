using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour {
    private Player_Script2 player;  //Accesses the player script
    public GameObject FollowThis;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player_Script2>();
	}

    public void FollowNewObject(GameObject obj)
    {
        FollowThis = obj;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(FollowThis.transform.position.x, FollowThis.transform.position.y, -15);
	}
}
