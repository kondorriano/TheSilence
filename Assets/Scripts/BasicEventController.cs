using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEventController : MonoBehaviour {

    public string trackPath = "Basic.json";
    public bool saveWhenSPressed = false;
    public bool logBeat = false;
    Track track;
    int nextChunkIndex = 0;
    int currentChunkIndex = 0;

    double nextBeat;
    double chunkBeat = -1;
    bool lastEvent = false;

    public BeatEvent[] beatEvent;

    void Start()
    {
        if(trackPath != "")
            GameTrackReader.LoadTrackFromPath(trackPath, ref track);

        nextBeat = track.beatEvents[currentChunkIndex].beat + track.beatEvents[currentChunkIndex].numPos/track.beatEvents[currentChunkIndex].denomPos;
        ++nextChunkIndex;
        if (nextChunkIndex < track.beatEvents.Length) chunkBeat = track.beatEvents[nextChunkIndex].beat + track.beatEvents[nextChunkIndex].numPos / track.beatEvents[nextChunkIndex].denomPos;
        else lastEvent = true;
    }

    void Update()
    {
        if(saveWhenSPressed && trackPath != "" && Input.GetKeyDown(KeyCode.S))
        {
            GameTrackReader.SerializeAndSave(track, trackPath);
        }

        if (nextBeat < 0) return;
        
        double beatTime = BeatController.BeatTime;

        if (beatTime >= nextBeat)
        {
            if (!track.beatEvents[currentChunkIndex].silence)
                for(int i = 0; i < beatEvent.Length; ++i)
                    beatEvent[i].Beat(track.beatEvents[currentChunkIndex].noteDuration);

            if(logBeat) Debug.Log("Doing beat stuff on " + nextBeat + " " + track.beatEvents[currentChunkIndex].chunkType);

            bool looping = false;
            if(track.beatEvents[currentChunkIndex].chunkType == ChunkType.Loop)
            {
                double loopBeat = nextBeat + track.beatEvents[currentChunkIndex].loopDuration;
                if(lastEvent || loopBeat < chunkBeat)
                {
                    nextBeat = loopBeat;
                    looping = true;
                }
            }

            if (!looping)
            {
                currentChunkIndex = nextChunkIndex;
                nextBeat = chunkBeat;

                ++nextChunkIndex;

                if (nextChunkIndex < track.beatEvents.Length) chunkBeat = track.beatEvents[nextChunkIndex].beat + track.beatEvents[nextChunkIndex].numPos / track.beatEvents[nextChunkIndex].denomPos;
                else
                {
                    lastEvent = true;
                    chunkBeat = -1;
                }
            }
        }
    }

}
