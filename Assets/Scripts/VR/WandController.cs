using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour {

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    HashSet<InteractableItem> objectsHoveringOver = new HashSet<InteractableItem>();
    HashSet<InteractableItem> objectsTriggering = new HashSet<InteractableItem>();


    private void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        if (controller.GetPressDown(triggerButton))
        {
            foreach (InteractableItem item in objectsHoveringOver)
            {
                item.Attach(this);
            }
        }

        if (controller.GetPressUp(triggerButton))
        {
            foreach (InteractableItem item in objectsHoveringOver)
            {
                item.Deattach(this);
            }

            foreach (InteractableItem item in objectsTriggering)
            {
                objectsHoveringOver.Remove(item);
            }
            objectsTriggering.Clear();
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            collidedItem.Touched(this);
            if (controller.GetPress(triggerButton))
            {
                objectsTriggering.Remove(collidedItem);
            }
            else objectsHoveringOver.Add(collidedItem);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            collidedItem.Untouched(this);
            if (controller.GetPress(triggerButton))
            {
                objectsTriggering.Add(collidedItem);
            } else objectsHoveringOver.Remove(collidedItem);
        }
    }
}
