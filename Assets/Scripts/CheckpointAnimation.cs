using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointAnimation : MonoBehaviour {

    Animator m_Animator;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }
    public void AnimateME()
    {
        m_Animator.SetBool("Play", true);
    }
    public void StopAnimatingME()
    {
        m_Animator.SetBool("Play", false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
