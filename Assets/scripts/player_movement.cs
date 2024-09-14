using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// --Spiller skal kunne gå fritt langs x og z aksen
// Spiller skal kunne bytte mellom forskjellige entiter som er på lag
// Spiller skal kunne ta flag
// Spiller skal dø når den blir tatt av en motstander på dens territorie
// Spiller skal kunne skade fiender på eget territorie

public class player_movement : MonoBehaviour
{
    // State for spillerinformasjon
    public int playerID;
    
    // State for bevegelse
    public float forceMultiplier = 100;
    float max_speed = 5f, acceleration = 1.1f;
    public float drag = 0.95f;
    float xVelocity = 0f, zVelocity = 0f;
    bool xholding_key, zholding_key = false;

    // Initialiserer unity klasser
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (playerID == 0)
        {
            gameObject.tag = "player1";
        }  
         if (playerID == 1)
        {
            gameObject.tag = "player2";
        }          
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (rb.transform.position.y != 0.99)
        {
            rb.transform.position = new Vector3(rb.transform.position[0],0.99f, rb.transform.position[2]);
        }
         // Sjekk input
        if (playerID == 0)
        {
            if ( Input.GetKey(KeyCode.W))
            {
                zVelocity += acceleration;
                zholding_key = true;
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                xVelocity -= acceleration;
                xholding_key = true;
            }
            // Bestem bevegelse

            if (Input.GetKey(KeyCode.S))
            {
                zVelocity -= acceleration;
                zholding_key = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                xVelocity += acceleration;
                xholding_key = true;
            }
        }
        else if (playerID == 1)
        {
            if ( Input.GetKey(KeyCode.UpArrow))
            {
                zVelocity += acceleration;
                zholding_key = true;
            }
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                xVelocity -= acceleration;
                xholding_key = true;
            }
            // Bestem bevegelse

            if (Input.GetKey(KeyCode.DownArrow))
            {
                zVelocity -= acceleration;
                zholding_key = true;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                xVelocity += acceleration;
                xholding_key = true;
            }
        }
        // Enforce max speed
        if (xVelocity < -max_speed)
        {
            xVelocity = -max_speed;
        }
        if (xVelocity > max_speed)
        {
            xVelocity = max_speed;
        }
        
        if (zVelocity < -max_speed)
        {
            zVelocity = -max_speed;
        }
        if (zVelocity > max_speed)
        {
            zVelocity = max_speed;
        }

        if (xholding_key == false)
        {
            xVelocity *= drag;
        }
        if (zholding_key == false)
        {
            zVelocity *= drag;
        }
        Vector3 forceVector = new Vector3(xVelocity * forceMultiplier, 0, zVelocity * forceMultiplier);
        rb.AddForce(forceVector);
        if (xVelocity != 0 || zVelocity != 0)
        {
            transform.rotation = Quaternion.LookRotation(forceVector);
        }

        xholding_key = false;
        zholding_key = false;

    }
}
