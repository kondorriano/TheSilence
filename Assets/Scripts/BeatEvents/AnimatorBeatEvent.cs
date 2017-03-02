using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBeatEvent : BeatEvent {

    public string[] animNames =
    {
        "RobotRightLeg",
        "RobotLeftLeg"
    };

    Animator anim;
    int index = -1;



    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void Beat(double noteDuration)
    {
        anim.speed = (float)BeatController.beatsPerMinute / (60f * (float)noteDuration);
        index = (index + 1) % animNames.Length;
        anim.Play(animNames[index]);
    }
}
