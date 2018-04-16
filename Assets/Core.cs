using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
    public ShiftingController shifter;
    UnityEngine.UI.Image m_SpriteRenderer;
    public Sprite sprite1; //Blue
    public Sprite sprite2; //Purple
    public Sprite sprite3; //Orange
    public Sprite sprite4; //Green
    public Sprite sprite5; //Black
    // Use this for initialization
    void Start () {
        shifter = FindObjectOfType<ShiftingController>();
        m_SpriteRenderer = GetComponent<UnityEngine.UI.Image>();
        m_SpriteRenderer.sprite = sprite1;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    private void FixedUpdate()
    {
        if(shifter.charDimensionNumber=='p' &&m_SpriteRenderer.sprite!=sprite2)
        {
            m_SpriteRenderer.sprite = sprite2;
        }
        else if (shifter.charDimensionNumber == 'b' && m_SpriteRenderer.sprite != sprite1)
        {
            m_SpriteRenderer.sprite = sprite1;
        }
        else if (shifter.charDimensionNumber == 'o' && m_SpriteRenderer.sprite != sprite3)
        {
            m_SpriteRenderer.sprite = sprite3;
        }
        else if (shifter.charDimensionNumber == 'g' && m_SpriteRenderer.sprite != sprite4)
        {
            m_SpriteRenderer.sprite = sprite4;
        }
        else if (shifter.charDimensionNumber == 'w' && m_SpriteRenderer.sprite != sprite5)
        {
            m_SpriteRenderer.sprite = sprite5;
        }
    }
}
