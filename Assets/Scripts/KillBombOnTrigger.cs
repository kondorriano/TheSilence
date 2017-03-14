﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBombOnTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bomb") Destroy(other.gameObject);
    }
}
