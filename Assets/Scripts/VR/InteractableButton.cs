using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : InteractableItem {

    bool isEnabled = false;
    bool isPressed = true;

    float fusaTime;
    float counter;

    Material m;

    private void Start()
    {
        m = GetComponent<Renderer>().material;
        fusaTime = 0.125f*(60f / (float)BeatController.beatsPerMinute);
    }

    private void Update()
    {
        if (!isPressed) return;

        if (
            isEnabled)
        {
            m.SetColor("_EmissionColor", m.color);
        }       

        counter -= Time.deltaTime;
        if(counter <= 0) isPressed = false;
    }

    public override void Touched(WandController wand)
    {
        isPressed = true;
        counter = fusaTime;
    }

    public void setActivation(bool enabled)
    {
        isEnabled = enabled;
        if(!isEnabled) m.SetColor("_EmissionColor", Color.black);
    }

}
