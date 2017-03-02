using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour {

   

    AudioSource[] music;

    private static double currentBeatSong = 0.0;
    private static double currentBeatReal = 0.0;

    private static double dspStartTime = -1.0;
    private static double dspStartTimeReal = -1.0;

    //Song stuff that needs to be moved
    public static double beatsPerMinute = 90.0;
    public static double compassNumerator = 4.0;
    public static double compassDenominator = 4.0;
    public double lastBeat = 188;
    public double loopBeat = 16;

    public static double BeatTime
    {
        get
        {
            return currentBeatSong;
        }
    }

    public static double BeatTimeFromBegin
    {
        get
        {
            return currentBeatReal;
        }
    }
    // Use this for initialization
    void Start () {
        music = transform.GetComponentsInChildren<AudioSource>();
        double dspTime = AudioSettings.dspTime;
        dspStartTime = dspTime;
        dspStartTimeReal = dspStartTime;
        for(int i = 0; i < music.Length; ++i)
            music[i].PlayScheduled(dspTime);
    }

    private void CalculateLoop(ref float musicTime, out bool looped, out double dspBeat)
    {
        looped = false;
        double dspTime = AudioSettings.dspTime;
        do
        {
            dspBeat = CalculateBeat(dspTime, dspStartTime);
            if (dspBeat > lastBeat)
            {
                double beatsToGoBack = lastBeat - loopBeat;
                double timeToGoBack = (60.0 * beatsToGoBack) / beatsPerMinute;
                musicTime -= (float)timeToGoBack;
                dspStartTime += timeToGoBack;
                looped = true;
            }
            else
                break;
        }
        while (true);
    }

    private double CalculateBeat(double currentTime, double startTime)
    {
        return ((currentTime - startTime) * beatsPerMinute) / 60.0;
    }

    // Update is called once per frame
    void Update () {

        bool looped;
        double dspCurrentBeat;
        float musicTime = music[0].time;
        double dspTime = AudioSettings.dspTime;
        CalculateLoop(ref musicTime, out looped, out dspCurrentBeat);

        if (looped)
            for (int i = 0; i < music.Length; ++i)
                music[i].time = musicTime;

        currentBeatSong = dspCurrentBeat;
        currentBeatReal = CalculateBeat(dspTime, dspStartTimeReal);
        Beat(currentBeatSong);
    }

    double oldBeat = 0;
    void Beat(double currentBeat)
    {
        float currentBeatFloor = Mathf.Floor((float)currentBeat);

        if (Mathf.Floor((float)oldBeat) != currentBeatFloor)
        {
            //Debug.Log(currentBeatFloor);
        }

        oldBeat = currentBeat;
    }
}
