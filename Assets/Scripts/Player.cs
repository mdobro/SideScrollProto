﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float maxSpeed;
    public float jumpVel;
    public float hangTime;
    public float wallJumpVertVel;
    public float wallJumpHorzVel;
    public float rayLengthOutsideOfPlayer;
    public float respawnDelay;
    public float deathHeight; //Y value at which the player dies and respawns at the last checkpoint
    public float gravIncrease;
    public float speedWhereGravIncreaseApplies;
    public float speedWhereHoldToJumpCancels;
    
    public bool _______________________;

    public Vector3 respawnLocation;

    Rigidbody rigid;    
    SphereCollider playerColl;
    ConstantForce moveForward;
    bool grounded;
    bool wallLeft;
    bool wallRight;
    bool jumping = false;

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
        wallRight = Physics.Raycast(transform.position, Vector3.right, playerColl.radius + rayLengthOutsideOfPlayer);
        wallLeft = Physics.Raycast(transform.position, Vector3.left, playerColl.radius + rayLengthOutsideOfPlayer);

        if (transform.position.y < deathHeight) {
            //player has fallen off map, respawn
            Respawn();
        }
        
        if (!grounded) {
            //IN AIR
            moveForward.enabled = false;
            if (jumping && rigid.velocity.y < speedWhereGravIncreaseApplies && rigid.velocity.y > speedWhereHoldToJumpCancels) {
                //hold to jump higher
                rigid.AddForce(Vector3.up * hangTime, ForceMode.Acceleration);
            }
            if (rigid.velocity.y < speedWhereGravIncreaseApplies) {
                //increase gravity
                float downForce = wallLeft || wallRight ? gravIncrease * 1 / 3 : gravIncrease;
                rigid.AddForce(Vector3.down * downForce, ForceMode.Acceleration);
            }
        } else {
            //move forward
            if (rigid.velocity.y < 0)
                rigid.velocity = new Vector3(rigid.velocity.x, 0, 0); //cancel downward force
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
            Respawn();
        }
        if (coll.tag == "Checkpoint") {
            respawnLocation = coll.transform.position;
        }
    }

    private void Respawn() {
        MainGameController.S.RespawnWithDelay(respawnDelay);
        jumping = false;
        rigid.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    //called by an event trigger attached to "Event Grabber"
    public void PointerDown() {
        //if on ground, jump
        grounded = Physics.Raycast(transform.position, Vector3.down, playerColl.radius + rayLengthOutsideOfPlayer);
        wallRight = Physics.Raycast(transform.position, Vector3.right, playerColl.radius + rayLengthOutsideOfPlayer);
        wallLeft = Physics.Raycast(transform.position, Vector3.left, playerColl.radius + rayLengthOutsideOfPlayer);
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
