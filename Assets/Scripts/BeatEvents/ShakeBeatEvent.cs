using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBeatEvent : BeatEvent {

    public float shakeCompassDuration = .25f;
    public float shakeAmount = .2f;
    public float decreaseFactor = 1f;

    Vector3 originalPos;

    float shakeTime = 0;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (shakeTime <= 0) return;
        transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

        shakeTime -= Time.deltaTime * decreaseFactor;
        if(shakeTime <= 0)
            transform.localPosition = originalPos;
    }

    public override void Beat(double noteDuration)
    {
        shakeTime = (shakeCompassDuration * (float)noteDuration * 60f) / (float)BeatController.beatsPerMinute;
    }
}
