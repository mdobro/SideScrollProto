using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float movementSpeed;
    public float jumpHeight;

    public bool _______________________;

    Rigidbody rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
		          
	}
	
	// Update is called once per frame
	void Update () {
	    	
	}

    void FixedUpdate () {
        Vector3 movement = new Vector3(1, 0, 0);
        rigid.AddForce(movement * movementSpeed * Time.deltaTime);

    }

    public void PointerDown() {
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), 0.55f)) {
            //jump
            print("Jump");
            Vector3 jump = new Vector3(0, 1, 0);
            rigid.AddForce(jump * jumpHeight, ForceMode.Impulse);
        }
    }
}
