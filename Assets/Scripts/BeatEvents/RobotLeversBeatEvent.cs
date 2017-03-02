using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLeversBeatEvent : BeatEvent {

    public Transform[] levers;

    public float leverMaxPos = .5f;
    public float leverMinPos = -.75f;

    float leverMoveTime;
    float leverMoveDuration;

    public float leverSpeed = .125f;

    bool leverMoved = true;

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
    int leverToMove = 0;

    bool moveLock = false;

    public BeatEvent[] otherEffects;

    public bool getLegMove()
    {
        return leverMoved && !moveLock;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !leverMoved)
        {
            leverMoved = true;
            Vector3 pos = levers[leverToMove].localPosition;
            pos.z = leverMaxPos;
            levers[leverToMove].localPosition = pos;
            leverMoveDuration = 0;

            if (!moveLock)
            {
                equalizers[index].material.mainTexture = equDoneTex;
            }
            equalizers[index].material.SetFloat("_Cutoff", equMaxPos);
            equMoveDuration = 0;
        }

        EqualizerStuff();
        LeverStuff();
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
        levers[index].localPosition = Vector3.Lerp(startPosition, endPosition, time);

        if (leverMoveTime >= leverMoveDuration)
        {
            leverMoveDuration = 0;
            leverMoved = false;
            leverToMove = index;
        }
    }

    public override void Beat(double noteDuration)
    {
        if (leverMoved && !moveLock)
        {
            for (int i = 0; i < otherEffects.Length; ++i)
                otherEffects[i].Beat(noteDuration);

            if (leverMoveDuration != 0) levers[index].localPosition = endPosition;
            index = (index + 1) % levers.Length;

            startPosition = levers[index].localPosition;
            startPosition.z = leverMaxPos;
            endPosition = startPosition;
            endPosition.z = leverMinPos;

            leverMoveDuration = (((float)noteDuration * 60f) / (float)BeatController.beatsPerMinute) * leverSpeed;
            leverMoveTime = 0;
        }

        if (moveLock && leverMoved)
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

    }
}
