using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBeatEvent : BeatEvent
{

    public Transform bird;
    public Transform birdTrail;

    public Vector3[] dirs =
    {
        Vector3.right,
        Vector3.down,
        Vector3.left,
        Vector3.up
    };

    int index = -1;

    Vector3 startPosition = Vector2.zero;
    Vector3 endPosition = Vector2.zero;

    float moveTime = 0;
    float moveDuration = 0;

    void Start()
    {
        birdTrail.localPosition = dirs[++index];
        bird.localPosition = dirs[++index];

        startPosition = birdTrail.localPosition;
        endPosition = birdTrail.localPosition;
    }

    void Update()
    {
        if (moveDuration == 0) return;
        moveTime += Time.deltaTime;

        float time = moveTime / moveDuration;
        birdTrail.localPosition = Vector3.Lerp(startPosition, endPosition, time);
        time = Easing.Elastic.Out(time);
        bird.localPosition = Vector3.Lerp(startPosition, endPosition, time);

        if (moveTime >= moveDuration) moveDuration = 0;

    }

    public override void Beat(double noteDuration)
    {
        index = (index + 1) % dirs.Length;

        startPosition = (moveDuration != 0) ? endPosition : birdTrail.localPosition;
        endPosition = dirs[index];

        moveDuration = ((float)noteDuration * 60f) / (float)BeatController.beatsPerMinute;
        moveTime = 0;
    }
}
