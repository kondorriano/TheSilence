using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLegsBeatEvent : BeatEvent {
    public bool workAlone = false;

    public SingleButtonBeatEvent[] levers;
    InteractableButton[] ibs;

    int index = 1;


    void Start()
    {
        ibs = new InteractableButton[levers.Length];
        for (int i = 0; i < levers.Length; ++i) ibs[i] = levers[i].GetComponent<InteractableButton>();
    }

    public override void Beat(double noteDuration)
    {
        if (ibs[index].isActivated())
        {
            index = (index + 1) % levers.Length;
        }

        levers[index].Beat(noteDuration);
    }

    public bool getLegMove()
    {
        return workAlone || ibs[index].isActivated();
    }
}
