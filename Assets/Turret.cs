using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public GameObject bullet;
    public Transform range;
    public Transform barrel;
    public AudioClip boom;
    public AudioSource source;
    public float speed;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        bullet.transform.position =new Vector3 (bullet.transform.position.x,bullet.transform.position.y-speed * Time.deltaTime,bullet.transform.position.z);
        if(bullet.transform.position.y<range.transform.position.y)
        {
            bullet.transform.position = barrel.position;
            source.PlayOneShot(boom);
        }
	}
}
