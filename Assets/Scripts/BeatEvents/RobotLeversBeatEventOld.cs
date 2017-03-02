using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLeversBeatEventOld : BeatEvent {

    public float inmunity = .125f;
    public Transform[] levers;

    int index = -1;
    int toMoveIndex = 1;

    Vector3 startPosition = Vector3.zero;
    Vector3 endPosition = Vector3.zero;

    float moveTime = 0;
    float moveDuration = 0;

    bool toMove = true;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && toMove)
        {
            toMove = false;
            levers[toMoveIndex].localPosition += 2f * transform.InverseTransformDirection(transform.forward);
            moveDuration = 0;
        }
        if (moveDuration == 0) return;
        moveTime += Time.deltaTime;

        float time = moveTime / moveDuration;
        levers[index].localPosition = Vector3.Lerp(startPosition, endPosition, time);
        if(!toMove && time >= inmunity)
        {
            toMove = true;
            toMoveIndex = index;
        }
        if (moveTime >= moveDuration) moveDuration = 0;

    }

    public override void Beat(double noteDuration)
    {
        if (toMove) return;
        if (moveDuration != 0) levers[index].localPosition = endPosition;
        index = (index + 1) % levers.Length;

        startPosition = levers[index].localPosition;
        endPosition = startPosition - 2f*transform.InverseTransformDirection(transform.forward);

        moveDuration = ((float)noteDuration * 60f) / (float)BeatController.beatsPerMinute;
        moveTime = 0;
    }
}
