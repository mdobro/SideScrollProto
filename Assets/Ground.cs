using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Player p = GameObject.Find ("Player").GetComponent<Player>();
		//vf^2 = vi^2 + 2*a*d
		//d = (vf^2 - vi^2)/2a
		Vector3 groundStart = transform.position;
		groundStart.x -= transform.localScale.x / 2;
		float tapHeight = -(p.tapJumpVelocity * p.tapJumpVelocity) / (2 * (Physics.gravity.y - p.gravIncrease));
		groundStart.y += tapHeight;
		Gizmos.DrawRay (groundStart, Vector3.right * transform.localScale.x);
		float holdHeight = -(p.tapJumpVelocity * p.tapJumpVelocity) / (2 * (Physics.gravity.y - p.gravIncrease + p.holdJumpAccel));
		groundStart.y += holdHeight - tapHeight;
		Gizmos.DrawRay (groundStart, Vector3.right * transform.localScale.x);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
