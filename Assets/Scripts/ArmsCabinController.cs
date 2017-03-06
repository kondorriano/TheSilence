using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsCabinController : MonoBehaviour {

    public Transform rightController;
    public Transform leftController;
    public Transform rightHand;
    public Transform leftHand;

    public float scaling = 3;

	// Update is called once per frame
	void Update () {
        rightHand.localPosition = rightController.localPosition * scaling;
        leftHand.localPosition = leftController.localPosition * scaling;
    }
}
