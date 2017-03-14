using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour {

    protected WandController attachedWand;


    public virtual void Attach(WandController wand)
    {

    }

    public virtual void Deattach(WandController wand)
    {

    }

    public virtual void Touched(WandController wand)
    {

    }

    public virtual void Untouched(WandController wand)
    {

    }
}
