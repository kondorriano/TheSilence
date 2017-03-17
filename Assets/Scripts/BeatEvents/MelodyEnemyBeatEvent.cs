using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyEnemyBeatEvent : BeatEvent {


    Vector3[] rotos = new Vector3[] {
        //0
        new Vector3(0,0,-40),
        new Vector3(-12.21f,5.399f,-24.078f),
        new Vector3(-20,0,0),
        new Vector3(-12.21f,-5.399f,24.078f),
        new Vector3(0,0, 40),

        //5
        new Vector3(-13,0,0),
        new Vector3(0,0,0),
        new Vector3(13,0,0),
        new Vector3(28,0,0),

        //9
        new Vector3(12.21f,-5.399f,-24.078f),
        new Vector3(20,0,0),
        new Vector3(12.21f,5.399f,24.078f),

        //12
        new Vector3(0,0,-23.75f),
        new Vector3(0,0, 0),
        new Vector3(0,0,23.75f),


    };

    int[][] patterns = new int[][]
    {
        //0
        new int[] {0,1,2,3,4},
        new int[] {5,6,7,8},
        new int[] {0,4},
        new int[] {3,1,2},
        new int[] {9,10,11},
        //5
        new int[] {4,11,10,9,0},
        new int[] {8,7,6,5},
        //7
        new int[] {0,14,13,12,4},
        new int[] {2,10,13}



    };

    int[] track = new int[]
    {
        0, 1, 2, 3, 4, 1,
        5, 6, 2, 4, 3,
        7, 8, 1


    };

    int trackIndex = 0;
    int patternIndex = 0;
    int rotoIndex = 0;


    public GameObject melodyEnemyNote;

    public Transform enemyPosition;
    public Transform notePosition;

    List<GameObject> enemies;
    List<GameObject> notes;


    void Start()
    {
        enemies = new List<GameObject>();
        notes = new List<GameObject>();

        patternIndex = track[trackIndex];
    }

    public override void Beat(double noteDuration)
    {
        GameObject g = Instantiate(melodyEnemyNote, notePosition.position, Quaternion.identity, notePosition);
        g.transform.localEulerAngles = rotos[patterns[patternIndex][rotoIndex]];
        Transform enemy = g.transform.GetChild(1);
        enemy.SetParent(enemyPosition);
        enemies.Add(enemy.gameObject);
        notes.Add(g);

        if(++rotoIndex >= patterns[patternIndex].Length)
        {
            trackIndex = (trackIndex+1) % track.Length;
            rotoIndex = 0;
            patternIndex = track[trackIndex];
        }
    }

    public void destroyEnemy()
    {
        Destroy(enemies[0]);
        enemies.RemoveAt(0);

        Destroy(notes[0]);
        notes.RemoveAt(0);
    }

    public Transform GetMelodyEnemy()
    {
        return enemies[0].transform;
    }
}
