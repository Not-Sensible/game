using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointanimation : MonoBehaviour {
    // Use this for initialization
    Animator m_Animator;
    void Start () {
        m_Animator = gameObject.GetComponent<Animator>();
    }
    public void AnimateME()
    {
        m_Animator.SetBool("Play", true);

    }
    // Update is called once per frame
    void Update () {
		
	}
}
