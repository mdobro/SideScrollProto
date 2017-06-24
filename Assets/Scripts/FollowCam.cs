using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;
    public float maxPlayerYBeforeFollowing;
    public float yPadding;

	// Use this for initialization
	void Start () {
        if (offset.z == 0)
            offset.z = transform.position.z; //use the natural camera Z position unless explicitly set
        transform.position = player.transform.position + offset;
	}
	
	// Update is called once per frame
	void Update () {
        //keep Y position const
        if (player != null) {
            Vector3 camPos = transform.position;
            camPos.x = player.transform.position.x + offset.x;
            //if camPos is outside of the current position + padding, change to that new position otherwise remain stationary
            transform.position = camPos;
        }
	}
}
