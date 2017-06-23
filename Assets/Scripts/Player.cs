using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float movementSpeed;
    public float jumpForce;
    public float wallJumpForce;
    public float rayLengthOutsideOfPlayer;

    public bool _______________________;

    Rigidbody rigid;
    SphereCollider coll;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<SphereCollider>();
    }
	
	// Update is called once per frame
	void Update () {
	    	
	}

    void FixedUpdate () {
        Vector3 movement = new Vector3(1, 0, 0);
        rigid.AddForce(movement * movementSpeed * Time.deltaTime);

    }

    public void PointerDown() {
        //if on ground, jump
        bool grounded = Physics.Raycast(transform.position, new Vector3(0, -1, 0), coll.radius + rayLengthOutsideOfPlayer);
        bool wallRight = Physics.Raycast(transform.position, new Vector3(1, 0, 0), coll.radius + rayLengthOutsideOfPlayer);
        bool wallLeft = Physics.Raycast(transform.position, new Vector3(-1, 0, 0), coll.radius + rayLengthOutsideOfPlayer);
        if (grounded) {
            print("Jump");
            Vector3 jump = new Vector3(0, 1, 0);
            rigid.AddForce(jump * jumpForce, ForceMode.Impulse);
        } else if (wallRight) {
            print("Wall Jump from right wall");
            Vector3 wallJump = new Vector3(-1, 1, 0);
            rigid.AddForce(wallJump * wallJumpForce, ForceMode.Impulse);
        } else if (wallLeft) {
            print("Wall Jump from left wall");
            Vector3 wallJump = new Vector3(1, 1, 0);
            rigid.AddForce(wallJump * wallJumpForce, ForceMode.Impulse);
        }
        
    }
}
