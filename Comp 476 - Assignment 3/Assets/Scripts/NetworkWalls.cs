using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class NetworkWalls : NetworkBehaviour {

    // Inner Wall Prefab
    public GameObject innerWalls;

	// Use this for initialization
	void Start() {
        // CmdSpawnWalls();
    }
	
    [Command]
    void CmdSpawnWalls() {
        GameObject obj = Instantiate(innerWalls);
        NetworkServer.Spawn(obj);
    }
}
