using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHand : InteractableItem {



    private void Update()
    {
        if (attachedWand == null) return;

        Vector3 pos = transform.localPosition;
        pos = transform.parent.InverseTransformPoint(attachedWand.transform.position);
        transform.localPosition = pos;        
    }

}
