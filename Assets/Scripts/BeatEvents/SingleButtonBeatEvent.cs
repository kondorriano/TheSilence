using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleButtonBeatEvent : BeatEvent
 {
    public Color buttonColor;
    public float maxZScale = 1;
    public float minZScale = .1f;

    public float maxYOffset = 0;
    public float minYOffset = .5f;


    public float shrinkSpeed = .125f;

    Renderer rend;

    float shrinkDuration = 0;
    float waitDuration = 0;
    float animTime = 0;

    InteractableButton ib;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = buttonColor;

        Vector2 offset = rend.material.GetTextureOffset("_MainTex");
        offset.y = minYOffset;
        rend.material.SetTextureOffset("_MainTex", offset);

        Vector3 scale = transform.localScale;
        scale.z = minZScale;
        transform.localScale = scale;

        ib = GetComponent<InteractableButton>();
    }

    void Update()
    {
        if (shrinkDuration == 0) return;
        
        animTime += Time.deltaTime;

        Shrink();

        if(animTime >= waitDuration+ shrinkDuration)
        {
            waitDuration = 0;
            shrinkDuration = 0;
            ib.setActivation(false);
        }
        
    }

    /*
     * void Grow()
    {        
        float time = animTime / growDuration;

        //offset
        Vector2 offset = rend.material.GetTextureOffset("_MainTex");
        offset.y = Mathf.Lerp(minYOffset, maxYOffset, time);
        rend.material.SetTextureOffset("_MainTex", offset);

        //scale
        time = Easing.Bounce.Out(time);
        Vector3 scale = transform.localScale;
        scale.z = Mathf.Lerp(minZScale, maxZScale, time);
        transform.localScale = scale;        
    }
    */

    void Shrink()
    {
        float time = animTime / waitDuration;

        //offset
        Vector2 offset = rend.material.GetTextureOffset("_MainTex");
        offset.y = Mathf.Lerp(maxYOffset, -minYOffset, time);
        rend.material.SetTextureOffset("_MainTex", offset);


        time = Mathf.Clamp((animTime - waitDuration) / shrinkDuration, 0f, 1f);

        //scale
        time = Easing.Bounce.Out(time);
        Vector3 scale = transform.localScale;
        scale.z = Mathf.Lerp(maxZScale, minZScale, time);
        transform.localScale = scale;
    }

    public override void Beat(double noteDuration)
    {
        Vector2 offset = rend.material.GetTextureOffset("_MainTex");
        offset.y = maxYOffset;
        rend.material.SetTextureOffset("_MainTex", offset);

        Vector3 scale = transform.localScale;
        scale.z = maxZScale;
        transform.localScale = scale;

        float growS =  (noteDuration < shrinkDuration) ? (float)noteDuration : shrinkSpeed;
        shrinkDuration = (((float)noteDuration * 60f) / (float)BeatController.beatsPerMinute) * growS;
        waitDuration = (((float)noteDuration * 60f) / (float)BeatController.beatsPerMinute);
        animTime = 0;

        ib.setActivation(true);
    }
}
