using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    // List of Spawn Points
    public List<GameObject> spawnPoints;

    // Random Number Generator
    private System.Random rnd;


    // Use this for initialization
    void Start () {

        // Reset the Spawn Status on Restart
        foreach(GameObject obj in spawnPoints) {
            obj.GetComponent<SpawnPoint>().enableSpawn();
        }

        // Random Number Generator
        rnd = new System.Random();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Return location of the spawn point
    public GameObject getSpawnInfo() {

        // Spawn Tank Onto Spawn Points
        int spawnIndex = rnd.Next(0, spawnPoints.Count);

        while (spawnPoints[spawnIndex].GetComponent<SpawnPoint>().getSpawnStatus()) {
            spawnIndex = rnd.Next(0, spawnPoints.Count);
        }

        // Toggle Status of Spawn Point
        spawnPoints[spawnIndex].GetComponent<SpawnPoint>().toggleSpawnPoint();

        return spawnPoints[spawnIndex];


    }
}
