using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class TemporaryFilter : MonoBehaviour {

    public ShiftingController shifter;
    SpriteRenderer m_SpriteRenderer;
    public Sprite sprite1; //Blue
    public Sprite sprite2; //Purple
    public Sprite sprite3; //Orange
    public Sprite sprite4; //Green
    public Sprite sprite5; //Black
    char previous;
    System.TimeSpan ts;
   public int elapsedtime;
    bool count = true;
    Stopwatch stopwatch = new Stopwatch();
    // Use this for initialization
    void Start()
    {
        shifter = FindObjectOfType<ShiftingController>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    SpriteRenderer clockFunction(SpriteRenderer sprite)
    {
        stopwatch.Start();
        System.TimeSpan ts = stopwatch.Elapsed;
        int elapsedtime = ts.Seconds;
        // UnityEngine.Debug.Log(elapsedtime);
        if (elapsedtime == 2)
        {
            stopwatch.Reset();
            sprite.sprite = null;
            count = false;
            elapsedtime = 0;
        }
        return sprite;
    }
    private void FixedUpdate()
    {
        if (shifter.charDimensionNumber == 'p' && previous!=shifter.charDimensionNumber && m_SpriteRenderer.sprite != sprite2)
        {
            m_SpriteRenderer.sprite = sprite2;
            count = true;
            previous = shifter.charDimensionNumber;
        }
        else if (shifter.charDimensionNumber == 'b' && previous != shifter.charDimensionNumber && m_SpriteRenderer.sprite != sprite1)
        {
            m_SpriteRenderer.sprite = sprite1;
            count = true;
            previous = shifter.charDimensionNumber;
        }
        else if (shifter.charDimensionNumber == 'o' && previous != shifter.charDimensionNumber && m_SpriteRenderer.sprite != sprite3)
        {
            m_SpriteRenderer.sprite = sprite3;
            count = true;
            previous = shifter.charDimensionNumber;
        }
        else if (shifter.charDimensionNumber == 'g' && previous != shifter.charDimensionNumber && m_SpriteRenderer.sprite != sprite4)
        {
            m_SpriteRenderer.sprite = sprite4;
            count = true;
            previous = shifter.charDimensionNumber;
        }
        else if (shifter.charDimensionNumber == 'w' && previous != shifter.charDimensionNumber && m_SpriteRenderer.sprite != sprite5)
        {
            m_SpriteRenderer.sprite = sprite5;
            count = true;
            previous = shifter.charDimensionNumber;
        }
        if(count==true)
        {
            m_SpriteRenderer=clockFunction(m_SpriteRenderer);
        }

    }

}