using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

	public bool drawGizmos = true;

	// Use this for initialization
	void Start () {
		
	}

	void OnDrawGizmos() {
		if (drawGizmos) {
			Gizmos.color = Color.red;
			Player p = GameObject.Find ("Player").GetComponent<Player> (); //used to get jump variables

			//create rays to show jump heights
			//vf^2 = vi^2 + 2*a*d
			//d = (vf^2 - vi^2)/2a
			Vector3 groundStart = transform.position;
			groundStart.y += transform.localScale.y / 2; //account of thinkness of platform
			groundStart.x -= transform.localScale.x / 2; //start ray at left most side of platform
			float tapHeight = -(p.tapJumpVelocity * p.tapJumpVelocity) / (2 * (Physics.gravity.y - p.gravIncrease));
			groundStart.y += tapHeight; //raise ray to tap jump height
			Gizmos.DrawRay (groundStart, Vector3.right * transform.localScale.x);
			float holdHeight = -(p.tapJumpVelocity * p.tapJumpVelocity) / (2 * (Physics.gravity.y - p.gravIncrease + p.holdJumpAccel));
			groundStart.y += holdHeight - tapHeight; //raise ray to hold jump height
			Gizmos.DrawRay (groundStart, Vector3.right * transform.localScale.x);

			//create ray to show max jump distance level with current platform
			Vector3 topRightOfPlatform = transform.position;
			topRightOfPlatform.x += transform.localScale.x / 2 - p.transform.localScale.x * 2;
			topRightOfPlatform.y += transform.localScale.y / 2;
			//vf = vi + a*t
			//vf = 0
			//t = -vi / a
			float timeInAir = -(p.tapJumpVelocity) / (Physics.gravity.y - p.gravIncrease + p.holdJumpAccel);
			timeInAir *= 2;
			//d = ((vi + vf) / 2) * t
			//vi == vf
			float d = p.maxSpeed * timeInAir;
			Gizmos.DrawRay (topRightOfPlatform, Vector3.right * d);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
