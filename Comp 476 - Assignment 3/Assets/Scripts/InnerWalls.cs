using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class InnerWalls : NetworkBehaviour {

    

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

       
    }

    // Function that deals with the Collision
    void OnCollisionEnter(Collision col) {

        // When it Collides with the Border Wall
        if (col.gameObject.tag == "Projectile" || col.gameObject.tag == "InvincibleRound") {

            // CmdDestroyWall();
            Destroy(this.gameObject);
            // this.gameObject.SetActive(false);
        }

    }

    [Command]
    void CmdDestroyWall() {
        NetworkServer.UnSpawn(this.gameObject);
    }
}
