using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class TankMovement : NetworkBehaviour {

    // Prefab for the projectile
    public GameObject projectilePrefab;
    public GameObject invincibleRoundPrefeb;

    // ============ Private Variables ============ 

    // Tank Health 
    private int health;

    // Variables for basic projectile
    private GameObject projectile;
    private float tankSpeed = 3.0f;

    // Variable for Invincible Tank Rounds
    private int invincibleRoundCount;
    private int maxInvincibleRound;
    private bool isInvincibleTankRoundActivated;


    // Variables for Basic Shooting Mechanic 
    private float shootingTimer;
    private float shootingInterval;

    // Variables for Unlimited Shots Power Up
    private bool isUnlimitedShotActivated;
    private float unlimitedShotTimer;
    private float unlimitedShotCount;


    // Use this for initialization
    void Start () {

        // Tank Health
        health = 3;

        // Shooting Interval Timer 
        shootingTimer    = 0.0f;
        shootingInterval = 1.0f;

        // Invincible Tank Round
        invincibleRoundCount = 0;
        maxInvincibleRound   = 1;
        isInvincibleTankRoundActivated = false;

        // Unlimited Shots Timer
        unlimitedShotTimer = 3.0f;
        unlimitedShotCount = 0.0f;
        isUnlimitedShotActivated = false;
}
	
	// Update is called once per frame
	void Update () {

        // Check if the Player has Authority over this specific object
        if (!hasAuthority) {
            return;
        }

        // Check for Controls
        controls();
	}


    // Function that takes WASD input and moves the tank
    void controls() {

        // Move Forward
        if(Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.right * tankSpeed * Time.deltaTime);
        }

        // Move Backward
        if (Input.GetKey(KeyCode.S)) {
            transform.Translate(-Vector3.right * tankSpeed * Time.deltaTime);
        }

        // Rotate Left 
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.up, -1);
        }

        // Rotate Right
        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.up, 1);
        }

        // Shooting Mechanic
        // - Shoot a projectile towards where the tank is pointing
        if (Input.GetKeyDown(KeyCode.Space)) {


            // Unlimited Shot PowerUp
            if(isUnlimitedShotActivated) {
                CmdShooting();
            }

            // Spawn a Projectile on the Server
            else if(shootingTimer > shootingInterval) {
                CmdShooting();
            }

        }

        // Add the Timer for Unlimited Shooting PowerUp
        if(unlimitedShotCount < unlimitedShotTimer) {
            unlimitedShotCount += Time.deltaTime;
        }

        else {
            isUnlimitedShotActivated = false;
            shootingInterval = 1.0f;
            unlimitedShotCount = 0.0f;
        }

        // Add the Timer to the shooting mechanic
        shootingTimer += Time.deltaTime;
    }


    // Shooting Function
    // - Spawns a projectile in front of the tank, launches it forwards
    [Command]
    void CmdShooting() {

        float velocity = 0.0f;

        // Shoot Invincible Round
        if(isInvincibleTankRoundActivated && invincibleRoundCount < maxInvincibleRound) {
            projectile = Instantiate(invincibleRoundPrefeb, this.transform.position + this.transform.right, Quaternion.identity);
            velocity = 4.0f; // Setting Velocity to the projectile
            invincibleRoundCount++; // Increasing Invincible Round Shot Count

        }

        // Shoot Regular Round
        else {
            // Creating the Basic Projectile Object from ProjectilePrefeb
            isInvincibleTankRoundActivated = false;
            invincibleRoundCount = 0;
            projectile = Instantiate(projectilePrefab, this.transform.position + this.transform.right, Quaternion.identity);
            velocity = 8.0f; // Setting Velocity to the projectile
        }

        // Apply the velocity & Spawn Object on Network
        projectile.GetComponent<Rigidbody>().velocity = this.transform.right * velocity;
        NetworkServer.Spawn(projectile);

        // Reset Shooting Cooldown
        shootingTimer = 0.0f;
        
    }


    // Function that deals with the Collision
    void OnCollisionEnter(Collision col) {

        // When it Collides with the Border Wall
        if (col.gameObject.tag == "InfiniteAmmo") {
            shootingInterval = 0.0f;
            isInvincibleTankRoundActivated = false;
            isUnlimitedShotActivated = true;
        }

        // When it Collides with the Border Wall
        if (col.gameObject.tag == "InvincibleRound") {
            isUnlimitedShotActivated = false;
            isInvincibleTankRoundActivated = true;
        }

        // When it Collides with the Border Wall
        if (col.gameObject.tag == "Projectile") {
            health--;

            if(health == 0) {
                Destroy(this.gameObject);
            }
        }

    }

}
