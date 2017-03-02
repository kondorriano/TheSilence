using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBeatEvent : BeatEvent {
    public RobotLeversBeatEvent rlbe;
    public BeatEvent[] beatEvent;

    public override void Beat(double noteDuration)
    {
        if(rlbe.getLegMove())
        {
            for (int i = 0; i < beatEvent.Length; ++i) beatEvent[i].Beat(noteDuration);
        }
    }
}
