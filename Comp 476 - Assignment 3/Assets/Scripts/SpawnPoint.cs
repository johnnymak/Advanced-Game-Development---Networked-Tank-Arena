using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    // Boolean to check whether this point has spawned yet
    private bool isSpawned;

    // Change of Tank Spawn Orientation
    public float orientationAngle;


	// Use this for initialization
	void Start () {
        isSpawned = false;
	}
	

    // Toggles the status of the Spawn Point
	public void toggleSpawnPoint() {
        isSpawned = !isSpawned;
    }


    // Check if Spawn Point has been Activated
    public bool getSpawnStatus() {
        return isSpawned;
    }

    // Enable the Spawn Point on restart
    public void enableSpawn() {
        isSpawned = false;
    }
}
