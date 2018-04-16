using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corebody2 : MonoBehaviour {
    SpriteRenderer m_SpriteRenderer;
    public Sprite sprite1;
    public Sprite sprite2;
    // Use this for initialization
    void Start () {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = sprite1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
