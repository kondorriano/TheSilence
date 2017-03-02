using System;
using UnityEngine;

public class GameTrackReader
{
    public static void LoadTrackFromPath(string path, ref Track track)
    {
        if (System.IO.File.Exists(path))
        {
            bool sucess = LoadFromFile(path, ref track);
            if (!sucess)
                Debug.LogError("Failed to load " + path);
        }
        else
        {
            Debug.Log("No path " + path + " found");
        }

    }

    static bool LoadFromFile(string path, ref Track track)
    {
        bool sucess = true;
        try
        {
            string fileContents = System.IO.File.ReadAllText(path);
            track = JsonUtility.FromJson<Track>(fileContents);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            sucess = false;
        }

        return sucess;
    }

    public static void SerializeAndSave(Track track, string path)
    {
        string serializedBank = JsonUtility.ToJson(track, true);
        System.IO.File.WriteAllText(path, serializedBank);
    }



    public static void NewLoadTrackFromPath(string path, ref TrackP track)
    {
        if (System.IO.File.Exists(path))
        {
            bool sucess = NewLoadFromFile(path, ref track);
            if (!sucess)
                Debug.LogError("Failed to load " + path);
        }
        else
        {
            Debug.Log("No path " + path + " found");
        }

    }

    static bool NewLoadFromFile(string path, ref TrackP track)
    {
        bool sucess = true;
        try
        {
            string fileContents = System.IO.File.ReadAllText(path);
            track = JsonUtility.FromJson<TrackP>(fileContents);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            sucess = false;
        }

        return sucess;
    }

    public static void NewSerializeAndSave(TrackP track, string path)
    {
        string serializedBank = JsonUtility.ToJson(track, true);
        System.IO.File.WriteAllText(path, serializedBank);
    }
}

[Serializable]
public class Track
{
    public BeatChunk[] beatEvents = null;
}

[Serializable]
public class BeatChunk
{

    public double beat; //Triggers at beat
    public double noteDuration = 1;
    public double loopDuration = 1;
    public double numPos = 0; //offset from the beat
    public double denomPos = 1; //offset note size
    public ChunkType chunkType = ChunkType.Single;
    public bool silence = false;


    public BeatChunk(double b)
    {
        beat = b;
    }

    public BeatChunk(double b, ChunkType type)
    {
        beat = b;
        chunkType = type;
    }

    public BeatChunk(double b, ChunkType type, bool s)
    {
        beat = b;
        chunkType = type;
        silence = s;
    }

    public BeatChunk(double b, ChunkType type, bool s, double np, double dp)
    {
        beat = b;
        chunkType = type;
        silence = s;
        numPos = np;
        denomPos = dp;
    }

    public BeatChunk(double b, ChunkType type, double np, double dp)
    {
        beat = b;
        chunkType = type;
        numPos = np;
        denomPos = dp;
    }
}

public enum ChunkType
{
    Single = 1,
    Loop = 2
}

[Serializable]
public class Note
{
    public double noteDuration = 1;
    public bool silence = false;
}

[Serializable]
public class Pattern
{
    public Note[] notes = null;
}

[Serializable]
public class Chunk
{
    public int pIndex; //Pattern index
    public double beat; //Triggers at beat
    public double numPos = 0.0; //offset from the beat
    public double denomPos = 1.0; //offset note size
    public ChunkType chunkType = ChunkType.Single;
    //public bool breakable = false; //If it is breakable, the last pattern can me stopped in the middle. If it is not, the last pattern won't even play
}

[Serializable]
public class TrackP
{
    public Pattern[] patterns = null;
    public Chunk[] chunks = null;
}