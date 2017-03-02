using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBeatEvent : BeatEvent {

    public float scaleFrom = 1.5f;

    Vector3 startScale = Vector2.zero;
    Vector3 endScale = Vector2.zero;
    Vector3 originalScale;

    float moveTime = 0;
    float moveDuration = 0;

    void Start()
    {
        originalScale = transform.localScale;
        startScale = originalScale;
        endScale = originalScale;
    }

    void Update()
    {
        if (moveDuration == 0) return;
        moveTime += Time.deltaTime;

        float time = moveTime / moveDuration;
        time = Easing.Bounce.Out(time);
        transform.localScale = Vector3.Lerp(startScale, endScale, time);
        if (moveTime >= moveDuration) moveDuration = 0;
    }

    public override void Beat(double noteDuration)
    {
        startScale = originalScale * scaleFrom;
        endScale = originalScale;
        moveDuration = ((float)noteDuration * 60f) / (float)BeatController.beatsPerMinute;
        moveTime = 0;
    }
}
