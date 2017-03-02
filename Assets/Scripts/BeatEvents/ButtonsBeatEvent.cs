using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsBeatEvent : BeatEvent
 {

    class ButtonRobot
    {
        public Transform button;
        public Renderer rend;
        public Animator anim;

        public float scaleTime = 0;
        public float scaleDuration = 0;

        public float offsetTime = 0;
        public float offsetDuration = 0;
    }

    public float maxZScale = 1;
    public float minZScale = .1f;

    public float maxYOffset = 0;
    public float minYOffset = .5f;

    public float growSpeed = .125f;

    ButtonRobot[] buttons;


    int index = -1;


    void Start()
    {
        buttons = new ButtonRobot[transform.childCount];

        int index = 0;
        foreach (Transform child in transform)
        {
            buttons[index] = new ButtonRobot();
            buttons[index].button = child;
            buttons[index].rend = child.GetComponent<Renderer>();
            buttons[index].anim = child.GetComponent<Animator>();
            index++;
        }
    }

    void Update()
    {
        ButtonRobot br;

        for (int i = 0; i < buttons.Length; ++i)
        {
            br = buttons[i];
            if (br.scaleDuration != 0)
            {
                br.scaleTime += Time.deltaTime;
                float time = br.scaleTime / br.scaleDuration;
                time = Easing.Bounce.Out(time);
                Vector3 scale = br.button.localScale;
                scale.z = Mathf.Lerp(minZScale, maxZScale, time);
                br.button.localScale = scale;
                if (br.scaleTime >= br.scaleDuration) br.scaleDuration = 0;
            }

            if (br.offsetDuration != 0)
            {
                br.offsetTime += Time.deltaTime;
                float time = br.offsetTime / br.offsetDuration;
                Vector2 offset = br.rend.material.GetTextureOffset("_MainTex");
                offset.y = Mathf.Lerp(minYOffset, maxYOffset, time);
                br.rend.material.SetTextureOffset("_MainTex", offset);
                if (br.offsetTime >= br.offsetDuration) br.offsetDuration = 0;
            }
        }
    }

    public override void Beat(double noteDuration)
    {
        
        index = (index + 1) % buttons.Length;
        buttons[index].scaleDuration = growSpeed;
        buttons[index].scaleTime = 0;

        buttons[index].offsetDuration = growSpeed;
        buttons[index].offsetTime = 0;



    }
}
