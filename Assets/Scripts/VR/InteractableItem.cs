using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour {

    protected WandController attachedWand;


    public virtual void Attach(WandController wand)
    {
        GetComponent<Renderer>().material.color = Color.yellow;
        attachedWand = wand;
    }

    public virtual void Deattach(WandController wand)
    {
        GetComponent<Renderer>().material.color = Color.white;

        if (attachedWand == wand) attachedWand = null;
    }
}
