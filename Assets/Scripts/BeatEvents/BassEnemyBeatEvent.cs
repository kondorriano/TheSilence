using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassEnemyBeatEvent : BeatEvent {


    public GameObject enemy;
    public Transform enemiesParent;

    public override void Beat(double noteDuration)
    {
        GameObject instantiatedEnemy = Instantiate(enemy, transform.position, Quaternion.identity, enemiesParent);
        float destroyOn = (8f*60f) / (float)BeatController.beatsPerMinute;
        Destroy(instantiatedEnemy, destroyOn);
    }
}
