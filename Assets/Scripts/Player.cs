using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float maxSpeed;
    public float tapJumpVelocity;
    public float holdJumpAccel;
    public float wallJumpVertVel;
    public float wallJumpHorzVel;
    public float rayLengthOutsideOfPlayer;
    public float respawnDelay;
    public float deathHeight; //Y value at which the player dies and respawns at the last checkpoint
    public float gravIncrease;
    public float speedWhereHoldToJumpCancels;
    public float wallUpwardForce;
    public float wallHugForce;
    
    public bool _______________________;

    public Vector3 respawnLocation;
	public float MAXJUMPHEIGHT;

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
		MAXJUMPHEIGHT = -(tapJumpVelocity * tapJumpVelocity) / (2 * (Physics.gravity.y - gravIncrease + holdJumpAccel));
    }

	void OnDrawGizmos() {
		
	}
	
	// Update is called once per frame
	void Update () {
	    	
	}

    void FixedUpdate () {
        grounded = Physics.Raycast(transform.position, new Vector3(0, -1, 0), playerColl.radius + rayLengthOutsideOfPlayer, LayerMask.GetMask("Default"));
        wallRight = Physics.Raycast(transform.position, Vector3.right, playerColl.radius + rayLengthOutsideOfPlayer, LayerMask.GetMask("Wall", "Default"));
		wallLeft = Physics.Raycast(transform.position, Vector3.left, playerColl.radius + rayLengthOutsideOfPlayer, LayerMask.GetMask("Wall", "Default"));

        if (transform.position.y < deathHeight) {
            //player has fallen off map, respawn
            PlayerDied();
        }
        
        if (!grounded) {
            //IN AIR
            moveForward.enabled = false;

            //increase gravity
            rigid.AddForce(Vector3.down * gravIncrease, ForceMode.Force);

            if (jumping && rigid.velocity.y > speedWhereHoldToJumpCancels && !(rigid.velocity.y <= 0 && wallLeft || wallRight)) {
                //hold to jump higher
                rigid.AddForce(Vector3.up * holdJumpAccel, ForceMode.Force);
            }
            if ((wallRight || wallLeft) && rigid.velocity.y < 0) {
                //apply wall friction force to allow "hugging" and slow sliding down walls
                rigid.AddForce((wallRight ? Vector3.right : Vector3.left) * wallHugForce, ForceMode.Acceleration);
                rigid.AddForce(Vector3.up * wallUpwardForce, ForceMode.Force);
            }
        } else {
            //move forward
            if (rigid.velocity.y < 0) {
                rigid.velocity = new Vector3(rigid.velocity.x, 0, 0); //cancel downward force
            }
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
            PlayerDied();
        }
        if (coll.tag == "Checkpoint") {
            respawnLocation = coll.transform.position;
        }
    }

    private void PlayerDied() {
        jumping = false;
        rigid.velocity = Vector3.zero;
        gameObject.SetActive(false);
		Invoke ("RespawnPlayer", respawnDelay);
    }

	private void RespawnPlayer() {
		transform.position = respawnLocation;
		gameObject.SetActive(true);
	}

    //called by an event trigger attached to "Event Grabber"
    public void PointerDown() {
        //if on ground and active in the scene, jump
		if (gameObject.activeSelf) {
			grounded = Physics.Raycast (transform.position, Vector3.down, playerColl.radius + rayLengthOutsideOfPlayer, LayerMask.GetMask ("Default"));
			wallRight = Physics.Raycast (transform.position, Vector3.right, playerColl.radius + rayLengthOutsideOfPlayer, LayerMask.GetMask ("Wall", "Default"));
			wallLeft = Physics.Raycast (transform.position, Vector3.left, playerColl.radius + rayLengthOutsideOfPlayer, LayerMask.GetMask ("Wall", "Default"));
			jumping = grounded ? grounded : wallRight ? wallRight : wallLeft ? wallLeft : false;
			if (grounded) {
				//print("Jump");
				rigid.velocity = new Vector3 (rigid.velocity.x, tapJumpVelocity, 0);
			} else if (wallRight) {
				//print("Wall Jump from right wall");
				rigid.velocity = new Vector3 (-wallJumpHorzVel, wallJumpVertVel, 0);
			} else if (wallLeft) {
				//print("Wall Jump from left wall");
				rigid.velocity = new Vector3 (wallJumpHorzVel, wallJumpVertVel, 0);
			}
		}
    }

    //called by an event trigger attached to "Event Grabber"
    public void PointerUp() {
        jumping = false;
    }

	public float getMaxJumpHeight() {
		return -(tapJumpVelocity * tapJumpVelocity) / (2 * (Physics.gravity.y - gravIncrease + holdJumpAccel));
	}

	public bool isGrounded() {
		return grounded;
	}
}
