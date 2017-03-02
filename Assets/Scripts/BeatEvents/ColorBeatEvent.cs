using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBeatEvent : BeatEvent {

    public Color[] cols =
    {
        Color.white,
        Color.red,
        Color.green,
        Color.yellow,
        Color.white ,
        new Color (0.3f, 0.0f, 1.0f),
        new Color (1.0f, 0.3f, 0.0f),
        new Color (0.0f, 1.0f, 0.3f)
    };
    int index = -1;
    Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public override void Beat(double noteDuration)
    {
        index = (index + 1) % cols.Length;
        rend.material.color = cols[index];
    }
}
