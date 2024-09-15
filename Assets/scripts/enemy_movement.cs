using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    // Start is called before the first frame update
 	public float speed = 5f;
	public float minDist = 1f;
	public Transform target;
    public string team;
    private GameObject player1;
    private GameObject player2;
    private GameObject myFlag;
    private flag_script flagScript;
    private int prioritized_target;
    Rigidbody rb;
    public bool player1InRange = false, player2InRange = false;

	// Use this for initialization
	void Start() 
	{
        /* player = GameObject.Find("Player");
        if (player != null)
        {
            target = player.GetComponent<Transform>();
        } */
        //Random rand = new Random();
        rb = GetComponent<Rigidbody>();
        prioritized_target = Random.Range(0,2);
        player1 = GameObject.FindWithTag("player1");
        player2 = GameObject.FindWithTag("player2");
        
        GameObject[] flags = GameObject.FindGameObjectsWithTag("flag");
        
        myFlag = flags[0];
        foreach (GameObject flag in flags) {
            float currentDistance = Vector3.Distance(transform.position, myFlag.transform.position);
            float newDistance = Vector3.Distance(transform.position, flag.transform.position);
            if (newDistance < currentDistance) {
                myFlag = flag;
            }
        }
        flagScript = myFlag.GetComponent<flag_script>();
	}
	
	// Update is called once per frame
	void Update() 
	{
        if (transform.position.x > 100) {
            transform.position = new Vector3(100, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -100) {
            transform.position = new Vector3(-100, transform.position.y, transform.position.z);
        }
        if (transform.position.z > 100) {
            transform.position = new Vector3(transform.position.x, transform.position.y, 100);
        }
        if (transform.position.z < -100) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -100);
        }
        if (rb.transform.position.y != 1.5)
        {
            rb.transform.position = new Vector3(rb.transform.position[0],1.5f, rb.transform.position[2]);
        }
        team = flagScript.team; // Debug
        if (flagScript.isInRange(player1) && flagScript.team != "player 1")
        {
            player1InRange = true;
        }
        else
        {
            player1InRange = false;
        }
        if (flagScript.isInRange(player2) && flagScript.team != "player 2")
        {
            player2InRange = true;
        }
        else
        {
            player2InRange = false;
        }

        if (player1InRange && player2InRange)
        {
            if (prioritized_target == 0)
            {
                target = player1.transform;
            }
            else
            {
                target = player2.transform;
            }
        }
        else if (player1InRange)
        {
            target = player1.transform;
        }
        else if (player2InRange)
        {
            target = player2.transform;
        }
        else if (!flagScript.isInRange(gameObject))
        {
            target = myFlag.transform;
        }
        else
        {
            target = null;
            return;
        }

        // Check if the enemy is within the terrain box
        /* if (terrainCollider != null && !terrainCollider.bounds.Contains(transform.position))
        {
            return; // Do not move if outside bounds
        } */

        //float PlayerFlagDistance = Vector3.Distance(player.transform.position, myFlag.transform.position);
        transform.LookAt(target);
        float distance = Vector3.Distance(transform.position,target.position);

        if(distance > minDist)	
            transform.position += transform.forward * speed * Time.deltaTime;

	}

	// Set the target of the chaser
	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}

  
}