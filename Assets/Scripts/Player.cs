using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Vector3 respawnLocation;

    public float maxSpeed;
    public float jumpVel;
    public float hangTime;
    public float wallJumpVertVel;
    public float wallJumpHorzVel;
    public float rayLengthOutsideOfPlayer;
    public float respawnDelay;
    
    public bool _______________________;

    Rigidbody rigid;    
    SphereCollider playerColl;
    ConstantForce moveForward;
    bool grounded;
    bool jumping = false;
    Vector3 counterGravity = -Physics.gravity * 2;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        playerColl = GetComponent<SphereCollider>();
        moveForward = GetComponent<ConstantForce>();
        respawnLocation = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	    	
	}

    void FixedUpdate () {
        grounded = Physics.Raycast(transform.position, new Vector3(0, -1, 0), playerColl.radius + rayLengthOutsideOfPlayer);
        
        if (!grounded) {
            //stop applying force if not grounded
            moveForward.enabled = false;
            //if user is holding down jump, accelerate
            if (jumping) {
                rigid.AddForce(Vector3.up * hangTime, ForceMode.Acceleration);
            }
        } else {
            //move forward
            moveForward.enabled = true;
        }
        if (rigid.velocity.x > maxSpeed) {
            Vector3 maxVel = new Vector3(maxSpeed, rigid.velocity.y, 0);
            rigid.velocity = maxVel;
        }
        //print(rigid.velocity);
    }

    public void OnTriggerEnter(Collider coll) {
        if (coll.tag == "Hazard") {
            MainGameController.S.RespawnWithDelay(respawnDelay);
            Destroy(this.gameObject);
        }
    }

    //called by an event trigger attached to "Event Grabber"
    public void PointerDown() {
        //if on ground, jump
        grounded = Physics.Raycast(transform.position, Vector3.down, playerColl.radius + rayLengthOutsideOfPlayer);
        bool wallRight = Physics.Raycast(transform.position, Vector3.right, playerColl.radius + rayLengthOutsideOfPlayer);
        bool wallLeft = Physics.Raycast(transform.position, Vector3.left, playerColl.radius + rayLengthOutsideOfPlayer);
        jumping = grounded ? grounded : wallRight ? wallRight : wallLeft ? wallLeft : false;
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

    //called by an event trigger attached to "Event Grabber"
    public void PointerUp() {
        print("Stop Jumping");
        jumping = false;
    }
}
