using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsController : MonoBehaviour {

    public float singleMaxDistance = 1.25f;
    public float singleBorderMaxDist = 1.5f;

    public float singleMinDistance = 0.5f;
    public float singleBorderMinDist = 1.25f;

    float maxDistance;

    float radius;
    public Transform center;
    public Transform[] middles;
    public Transform border;

    void Start()
    {
        maxDistance = singleMaxDistance * (float)(middles.Length) + singleBorderMaxDist;
        radius = center.localPosition.x;
    }
	// Update is called once per frame
	void LateUpdate () {
        Vector3 newPos = border.localPosition;
        Vector3 maxDir = newPos - center.localPosition;
        Vector2 radDir;
        if (maxDir.magnitude > maxDistance)
        {
            newPos = center.localPosition + maxDir.normalized * maxDistance;
        }

        radDir = new Vector2(newPos.x, newPos.z);
        if (radDir.magnitude < radius) radDir = radDir.normalized * radius;
        newPos.x = radDir.x;
        newPos.z = radDir.y;
        border.localPosition = newPos;


        Transform otherTrans = border;
        float maxDist = maxDistance - singleBorderMaxDist;
        float singleMaxDist = singleBorderMaxDist;
        float singleMinDist = singleBorderMinDist;

        for (int i = 0; i < middles.Length; ++i)
        {
            newPos = middles[i].localPosition;

            maxDir = otherTrans.localPosition - newPos;
            if (maxDir.magnitude > singleMaxDist)
            {
                newPos = otherTrans.localPosition - maxDir.normalized * singleMaxDist;
            } else if (maxDir.magnitude < singleMinDist)
            {
                newPos = otherTrans.localPosition - maxDir.normalized * singleMinDist;
            }

            maxDir = newPos - center.localPosition;
            if (maxDir.magnitude >= maxDist)
            {
                newPos = center.localPosition + maxDir.normalized * maxDist;
            }

            radDir = new Vector2(newPos.x, newPos.z);
            if (radDir.magnitude < radius) radDir = radDir.normalized * radius;
            newPos.x = radDir.x;
            newPos.z = radDir.y;
            middles[i].localPosition = newPos;

            middles[i].LookAt(otherTrans);
            middles[i].Rotate(Vector3.right, 90, Space.Self);

            singleMaxDist = singleMaxDistance;
            singleMinDist = singleMinDistance;
            maxDist -= singleMaxDistance;
            otherTrans = middles[i];
        }
    }
}
