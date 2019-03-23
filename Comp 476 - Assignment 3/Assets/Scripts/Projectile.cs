using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    // Max velocity to keep track of 
    float maxVelocity;

    // Use this for initialization
    void Start () {
        maxVelocity = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        // Updating the new Max Velocity
        if (this.GetComponent<Rigidbody>().velocity.magnitude > maxVelocity) {
            maxVelocity = this.GetComponent<Rigidbody>().velocity.magnitude;
        }

        this.GetComponent<Rigidbody>().velocity = maxVelocity * this.GetComponent<Rigidbody>().velocity.normalized;

    }

    // Function that deals with the Collision
    void OnCollisionEnter(Collision col) {

        // When it Collides with the Border Wall
        if(col.gameObject.tag == "Border" || col.gameObject.tag == "Tank" || (this.gameObject.tag != "InvincibleRound" && col.gameObject.tag == "InnerWall")) {
            Destroy(this.gameObject);
        }


    }
}
