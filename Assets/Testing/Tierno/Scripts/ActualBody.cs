using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualBody : MonoBehaviour {
    public PlayerMomentumAgain player;
    char dir = 'R';
    SpriteRenderer m_SpriteRenderer;
    public Sprite sprite1;
    public Sprite sprite2;
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerMomentumAgain>();
        //Fetch the SpriteRenderer of the Sprite
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = sprite1;
        //Output the current Texture of the Sprite (this returns the source Sprite if the Texture isn't packed)
    }


    void Update () {
		
	}
     void FixedUpdate()
    {
        if (player.DesiredDir == 'R' && dir != 'R' && player.move == true)
        {
            m_SpriteRenderer.sprite = sprite1;
            dir = 'R';
        }
        else if (player.DesiredDir == 'L' && dir != 'L' && player.move == true)
        {
            m_SpriteRenderer.sprite = sprite2;
            dir = 'L';
        }
    }
}
