using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLeversBeatEvent : BeatEvent {
    public bool workAlone = false;
    public InteractableLever[] levers;

    public float leverMaxPos = .5f;
    public float leverMinPos = -.75f;

    float leverMoveTime;
    float leverMoveDuration;

    public float leverSpeed = .125f;

    bool leverInPosition = true;

    Vector3 startPosition = Vector3.zero;
    Vector3 endPosition = Vector3.zero;

    public Renderer[] equalizers;

    public float equMaxPos = 0.01f;
    public float equMinPos = 1f;

    public Texture equTex;
    public Texture equDoneTex;
    public Texture equBadTex;

    float equMoveTime;
    float equMoveDuration;

    int index = 1;

    bool moveLock = false;

    public BeatEvent[] otherEffects;

    private void Start()
    {
        for(int i = 0; i < levers.Length; ++i)
        {
            levers[i].Init(this);
        }
    }

    public bool getLegMove()
    {
        return leverInPosition && !moveLock;
    }

    void Update()
    {
        EqualizerStuff();
        LeverStuff();
    }

    public void SetLeverInPosition()
    {
        leverInPosition = true;
        leverMoveDuration = 0;

        if (!moveLock)
        {
            equalizers[index].material.mainTexture = equDoneTex;
        }

        equalizers[index].material.SetFloat("_Cutoff", equMaxPos);
        equMoveDuration = 0;
    }

    void EqualizerStuff()
    {
        if (equMoveDuration == 0) return;
        equMoveTime += Time.deltaTime;

        float time = equMoveTime / equMoveDuration;
        equalizers[index].material.SetFloat("_Cutoff", Mathf.Lerp(equMinPos, equMaxPos, time));

        if (equMoveTime >= equMoveDuration)
        {
            equMoveDuration = 0;
            moveLock = true;
            equalizers[index].material.mainTexture = equBadTex;
        }
    }

    void LeverStuff()
    {
        if (leverMoveDuration == 0) return;
        leverMoveTime += Time.deltaTime;

        float time = leverMoveTime / leverMoveDuration;
        levers[index].transform.localPosition = Vector3.Lerp(startPosition, endPosition, time);

        if (leverMoveTime >= leverMoveDuration)
        {
            leverMoveDuration = 0;
            leverInPosition = false;
            levers[index].leverNotInPos();
        }
    }

    public override void Beat(double noteDuration)
    {
        if (leverInPosition && !moveLock)
        {
            for (int i = 0; i < otherEffects.Length; ++i)
                otherEffects[i].Beat(noteDuration);

            if (leverMoveDuration != 0) levers[index].transform.localPosition = endPosition;
            index = (index + 1) % levers.Length;

            startPosition = levers[index].transform.localPosition;
            startPosition.z = leverMaxPos;
            endPosition = startPosition;
            endPosition.z = leverMinPos;

            leverMoveDuration = (((float)noteDuration * 60f) / (float)BeatController.beatsPerMinute) * leverSpeed;
            leverMoveTime = 0;
        }

        if (moveLock && leverInPosition)
        {
            equalizers[index].material.mainTexture = equDoneTex;
        }
        else
        {
            equMoveDuration = ((float)noteDuration * 60f) / (float)BeatController.beatsPerMinute;
            equMoveTime = 0;
            equalizers[index].material.mainTexture = equTex;
        }

        moveLock = false;

        if (workAlone || levers[index].isGrabbed() && leverInPosition) SetLeverInPosition();
    }
}
