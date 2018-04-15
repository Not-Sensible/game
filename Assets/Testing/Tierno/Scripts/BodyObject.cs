using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyObject : MonoBehaviour {
    private PlayerMomentumAgain player;
    char dir = 'R'; //1 equals right
    // Use this for initialization
    void Start () {
       player = FindObjectOfType<PlayerMomentumAgain>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        if (player.DesiredDir == 'R' && dir!='R' && player.move==true)
        {
            transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.eulerAngles.y+180, transform.rotation.z);
            dir = 'R';
        }
        else if (player.DesiredDir == 'L' && dir != 'L' && player.move == true)
        {
            transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.eulerAngles.y-180, transform.rotation.z);
            dir = 'L';
        }
    }
}
