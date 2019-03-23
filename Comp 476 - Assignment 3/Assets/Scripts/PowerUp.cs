using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, 1);
    }


    // Function that deals with the Collision
    void OnCollisionEnter(Collision col) {

        // When it Collides with the Border Wall
        if (col.gameObject.tag == "Tank") {
            Destroy(this.gameObject);
        }


    }
}
