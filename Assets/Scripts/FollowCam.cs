using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public GameObject player;

    public Vector3 offset;

	// Use this for initialization
	void Start () {
        float y = transform.position.y;
        offset = transform.position - player.transform.position;
        offset.y = y;
	}
	
	// Update is called once per frame
	void Update () {
        //keep Y position const
        if (player != null) {
            Vector3 newPos = player.transform.position + offset;
            newPos.y = offset.y;
            transform.position = newPos;
        }
	}
}
