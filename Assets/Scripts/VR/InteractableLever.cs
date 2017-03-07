using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLever : InteractableItem {

    public float offset = .2f;
    bool leverInPosition = true;

    RobotLeversBeatEvent control;

    public void Init(RobotLeversBeatEvent cont)
    {
        control = cont;
    }

    public bool isGrabbed()
    {
        return attachedWand != null;
    }

    public void leverNotInPos()
    {
        leverInPosition = false;
    }

    private void Update()
    {
        if (attachedWand == null) return;

        if (!leverInPosition)
        {
            Vector3 pos = transform.localPosition;
            pos.z = Mathf.Clamp(transform.parent.InverseTransformPoint(attachedWand.transform.position).z, control.leverMinPos, control.leverMaxPos);

            if (pos.z >= Mathf.Abs(control.leverMaxPos)-offset)
            {
                control.SetLeverInPosition();
                leverInPosition = true;
                pos.z = control.leverMaxPos;
            }

            transform.localPosition = pos;

        }
    }

}
