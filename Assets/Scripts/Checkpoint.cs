using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public Material green;
    bool triggered = false;

    void OnTriggerEnter(Collider coll) {
        if (coll.tag == "Player" && !triggered) {
            //raise quad
            triggered = true;
            print("Checkpoint!");
            Transform quad = transform.GetChild(1);
            quad.Translate(new Vector3(0, 2, 0));
            quad.GetComponent<MeshRenderer>().material = green;
        }
    }
}
