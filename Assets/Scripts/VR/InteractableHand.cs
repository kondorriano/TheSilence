using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHand : InteractableItem {

    Vector3 origLocalPos;
    Color origCol;
    void Start()
    {
        origLocalPos = transform.localPosition;
    }

    public override void Attach(WandController wand)
    {
        GetComponent<Renderer>().material.color = Color.yellow;
        attachedWand = wand;
    }

    public override void Deattach(WandController wand)
    {
        GetComponent<Renderer>().material.color = origCol;

        if (attachedWand == wand) attachedWand = null;
    }

    private void Update()
    {
        if (attachedWand == null)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, origLocalPos, .2f);
        }
        else
        {

            Vector3 pos = transform.localPosition;
            pos = transform.parent.InverseTransformPoint(attachedWand.transform.position);
            transform.localPosition = pos;
            transform.rotation = attachedWand.transform.rotation;
        }    
    }

}
