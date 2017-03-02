using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternEventController : MonoBehaviour {

    public string trackPath = "BasicPattern.json";
    public bool saveWhenSPressed = false;
    public bool logBeat = false;
    TrackP track;
    int nextChunkIndex;
    int currentChunkIndex;
    Note[] currentPattern;
    int noteIndex;

    double nextBeat;
    double chunkBeat = -1;
    bool lastEvent = false;

    public BeatEvent[] beatEvent;

    void Start()
    {
        if(trackPath != "")
            GameTrackReader.NewLoadTrackFromPath(trackPath, ref track);

        currentChunkIndex = 0;
        nextChunkIndex = 1;
        currentPattern = track.patterns[track.chunks[currentChunkIndex].pIndex].notes;
        noteIndex = 0;

        nextBeat = track.chunks[currentChunkIndex].beat + track.chunks[currentChunkIndex].numPos/track.chunks[currentChunkIndex].denomPos;
        if (nextChunkIndex < track.chunks.Length) chunkBeat = track.chunks[nextChunkIndex].beat + track.chunks[nextChunkIndex].numPos / track.chunks[nextChunkIndex].denomPos;
        else lastEvent = true;
    }

    void Update()
    {
        if(saveWhenSPressed && trackPath != "" && Input.GetKeyDown(KeyCode.S))
        {
            GameTrackReader.NewSerializeAndSave(track, trackPath);
        }

        if (nextBeat < 0) return;
        
        double beatTime = BeatController.BeatTime;

        if (beatTime >= nextBeat)
        {
            if (!currentPattern[noteIndex].silence)
                for(int i = 0; i < beatEvent.Length; ++i)
                    beatEvent[i].Beat(currentPattern[noteIndex].noteDuration);


            if (logBeat) Debug.Log("Doing beat stuff on " + nextBeat + " " + track.chunks[currentChunkIndex].chunkType);

            nextBeat += currentPattern[noteIndex].noteDuration;
            ++noteIndex;

            bool changePattern = chunkBeat >= 0 && nextBeat >= chunkBeat;

            if (track.chunks[currentChunkIndex].chunkType == ChunkType.Loop)
                noteIndex = noteIndex  % currentPattern.Length;
            else changePattern |= noteIndex >= currentPattern.Length;

            if (changePattern) ChangePattern();            
        }
    }

    void ChangePattern()
    {
        currentChunkIndex = nextChunkIndex;

        if (!lastEvent) currentPattern = track.patterns[track.chunks[currentChunkIndex].pIndex].notes;
        else currentPattern = null;
        noteIndex = 0;

        nextBeat = chunkBeat;
        ++nextChunkIndex;
        if (nextChunkIndex < track.chunks.Length) chunkBeat = track.chunks[nextChunkIndex].beat + track.chunks[nextChunkIndex].numPos / track.chunks[nextChunkIndex].denomPos;
        else
        {
            lastEvent = true;
            chunkBeat = -1;
        }
    }

}
