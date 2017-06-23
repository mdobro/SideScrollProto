using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float maxSpeed;
    public float jumpVel;
    public float wallJumpVertVel;
    public float wallJumpHorzVel;
    public float rayLengthOutsideOfPlayer;

    public bool _______________________;

    Rigidbody rigid;    
    SphereCollider coll;
    ConstantForce moveForward;
    bool grounded;
    Vector3 counterGravity = -Physics.gravity * 2; 

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<SphereCollider>();
        moveForward = GetComponent<ConstantForce>();
    }
	
	// Update is called once per frame
	void Update () {
	    	
	}

    void FixedUpdate () {
        grounded = Physics.Raycast(transform.position, new Vector3(0, -1, 0), coll.radius + rayLengthOutsideOfPlayer);
        
        if (!grounded) {
            //stop applying force if not grounded
            moveForward.enabled = false;
        } else {
            //move forward
            moveForward.enabled = true;
        }
        if (rigid.velocity.x > maxSpeed) {
            Vector3 maxVel = new Vector3(maxSpeed, rigid.velocity.y, 0);
            rigid.velocity = maxVel;
        }
        print(rigid.velocity);
    }

    //called by an event trigger attached to "Event Grabber"
    public void PointerDown() {
        //if on ground, jump
        grounded = Physics.Raycast(transform.position, new Vector3(0, -1, 0), coll.radius + rayLengthOutsideOfPlayer);
        bool wallRight = Physics.Raycast(transform.position, new Vector3(1, 0, 0), coll.radius + rayLengthOutsideOfPlayer);
        bool wallLeft = Physics.Raycast(transform.position, new Vector3(-1, 0, 0), coll.radius + rayLengthOutsideOfPlayer);
        if (grounded) {
            print("Jump");
            rigid.velocity = new Vector3(rigid.velocity.x, jumpVel, 0);
        } else if (wallRight) {
            print("Wall Jump from right wall");
            rigid.velocity = new Vector3(-wallJumpHorzVel, wallJumpVertVel, 0);
        } else if (wallLeft) {
            print("Wall Jump from left wall");
            rigid.velocity = new Vector3(wallJumpHorzVel, wallJumpVertVel, 0);
        }
        
    }
}
