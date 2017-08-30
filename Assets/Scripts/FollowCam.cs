using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;
	public float ySpeed;

	private float groundHeight; //below this height the camera will follow the player down

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
			Player p = player.GetComponent<Player> ();
			if (p.isGrounded ()) {
				if ((player.transform.position.y > groundHeight + 1) || player.transform.position.y < groundHeight - 1) {
					//keeps camera from "jiggling" when player jumps due to ray length
					groundHeight = player.transform.position.y;
				}
			}
			print (groundHeight);
			Vector3 camPos = transform.position;
			camPos.x = player.transform.position.x + offset.x;
			if ((player.transform.position.y > p.MAXJUMPHEIGHT+1 + groundHeight) || (player.transform.position.y < groundHeight)) {
				//follow in Y if player goes above the max jump height
				camPos.y = Mathf.Lerp (camPos.y, player.transform.position.y + offset.y, Time.fixedDeltaTime * ySpeed);
			} else if (player.transform.position.y < p.MAXJUMPHEIGHT + groundHeight) {
				camPos.y = Mathf.Lerp (camPos.y, groundHeight + offset.y, Time.fixedDeltaTime * ySpeed);
			}
            transform.position = camPos;
        }
	}
}
