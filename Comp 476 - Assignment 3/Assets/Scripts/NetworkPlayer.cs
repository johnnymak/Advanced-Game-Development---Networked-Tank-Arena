using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {

    // Tank Prefab Object
    public GameObject tankPrefab;

    // List of Spawn Points
    public List<GameObject> spawnPoints;

    // Random Number Generator
    private System.Random rnd;


    // Use this for initialization
    void Start () {

        // Random Number Generator
        rnd = new System.Random();
        
        // Check if it is a Local Player trying to Spawn
        if (!isLocalPlayer) {
            return;
        }

        // Spawn Tank Onto Server
        CmdSpawnTank();
	}
	
	// Update is called once per frame
	void Update () {
        // CmdMovement();
	}

    [Command]
    void CmdSpawnTank() {

        // Creating Tank Prefab
        GameObject obj = Instantiate(tankPrefab);


        // Spawn Tank Onto Spawn Points
        int spawnIndex = rnd.Next(0, spawnPoints.Count);
        int count = 0;

        while(spawnPoints[spawnIndex].GetComponent<SpawnPoint>().getSpawnStatus() && count < 10) {
            spawnIndex = rnd.Next(0, spawnPoints.Count);
            count++;
        }
        
        // Set the new position and orientation
        obj.transform.position = spawnPoints[spawnIndex].transform.position;
        float angle = spawnPoints[spawnIndex].GetComponent<SpawnPoint>().orientationAngle;
        obj.transform.Rotate(Vector3.up, angle);
        spawnPoints[spawnIndex].GetComponent<SpawnPoint>().toggleSpawnPoint();

        // Spawn the Tank on the server
        NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
    }

    // Resets the Server
    [Command]
    void CmdRestartServer() {
        foreach(GameObject obj in spawnPoints) {
            obj.GetComponent<SpawnPoint>().enableSpawn();
        }
    }



}
