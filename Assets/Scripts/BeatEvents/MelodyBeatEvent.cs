﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyBeatEvent : BeatEvent {

    public Transform[] eyes;
    public Transform melodyEnemy;

    LineRenderer[] lines;

    float beamDuration = 0;
    float beamCounter = 0;

    private void Start()
    {
        lines = new LineRenderer[eyes.Length];
        for(int i = 0; i < eyes.Length; ++i)
        {
            lines[i] = eyes[i].GetComponent<LineRenderer>();
            lines[i].enabled = false;
        }
    }

    private void Update()
    {
        if (beamDuration == 0) return;
        beamCounter += Time.deltaTime;
        if (beamCounter >= beamDuration)
        {
            beamDuration = 0;
            for (int i = 0; i < eyes.Length; ++i)
            {
                lines[i].enabled = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (beamDuration == 0) return;

        for (int i = 0; i < eyes.Length; ++i)
        {
            lines[i].SetPosition(0, eyes[i].position);
            lines[i].SetPosition(1, melodyEnemy.position);
        }


    }

    public override void Beat(double noteDuration)
    {
        beamDuration = ((float)noteDuration * 60f) / (float)BeatController.beatsPerMinute;
        beamCounter = 0;
        for (int i = 0; i < eyes.Length; ++i)
        {
            lines[i].enabled = true;
            lines[i].SetPosition(1, eyes[i].parent.InverseTransformPoint(melodyEnemy.position));
        }

    }
}