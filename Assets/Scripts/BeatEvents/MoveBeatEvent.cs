using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBeatEvent : BeatEvent {

    public Vector3[] dirs =
    {
        Vector3.forward
    };

    int index = -1;

    Vector3 startPosition = Vector2.zero;
    Vector3 endPosition = Vector2.zero;

    float moveTime = 0;
    float moveDuration = 0;

    void Start()
    {
        startPosition = transform.localPosition;
        endPosition = transform.localPosition;
    }

    void Update()
    {
        if (moveDuration == 0) return;
        moveTime += Time.deltaTime;

        float time = moveTime / moveDuration;
        transform.localPosition = Vector3.Lerp(startPosition, endPosition, time);
        if (moveTime >= moveDuration) moveDuration = 0;

    }

    public override void Beat(double noteDuration)
    {
        index = (index + 1) % dirs.Length;
        Vector3 position = dirs[index];

        startPosition = (moveDuration != 0) ? endPosition : transform.localPosition;
        endPosition = startPosition + position;

        moveDuration = ((float)noteDuration * 60f) / (float)BeatController.beatsPerMinute;
        moveTime = 0;
    }
}
