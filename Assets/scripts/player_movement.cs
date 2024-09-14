using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Spiller skal kunne gå fritt langs x og z aksen
// Spiller skal kunne bytte mellom forskjellige entiter som er på lag
// Spiller skal kunne ta flag
// Spiller skal dø når den blir tatt av en motstander på dens territorie
// Spiller skal kunne skade fiender på eget territorie

public class player_movement : MonoBehaviour
{
    // State for spillerinformasjon
    public int playerID;
    
    //[,] keybinds = { { 1, 2, 3 }, { 4, 5, 6 } };

    // State for bevegelse
    public static float forceMultiplier = 200;
    float max_speed = 5f, acceleration = 1.1f;
    public float drag = 0.95f;
    float xVelocity = 0f, zVelocity = 0f;
    bool holding_key = false;

    // Initialiserer unity klasser
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        max_speed = 2;        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (rb.transform.position.y != 0.99)
        {
            //GetComponent<Rigidbody>().position = 
        }
         // Sjekk input
        if (playerID == 0)
        {
            if ( Input.GetKey(KeyCode.W))
            {
                zVelocity += acceleration;
                holding_key = true;
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                xVelocity -= acceleration;
                holding_key = true;
            }
            // Bestem bevegelse

            if (Input.GetKey(KeyCode.S))
            {
                zVelocity -= acceleration;
                holding_key = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                xVelocity += acceleration;
                holding_key = true;
            }
        }
        if (playerID == 1)
        {
            if ( Input.GetKey(KeyCode.UpArrow))
            {
                zVelocity += acceleration;
                holding_key = true;
            }
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                xVelocity -= acceleration;
                holding_key = true;
            }
            // Bestem bevegelse

            if (Input.GetKey(KeyCode.DownArrow))
            {
                zVelocity -= acceleration;
                holding_key = true;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                xVelocity += acceleration;
                holding_key = true;
            }
        }
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

        if (holding_key == false)
        {
            //acceleration = 0;
            xVelocity *= drag;
            zVelocity *= drag;
        }
        Vector3 forceVector = new Vector3(xVelocity * forceMultiplier, 0, zVelocity * forceMultiplier);
        rb.AddForce(forceVector);
        if (xVelocity != 0 || zVelocity != 0)
        {
            transform.rotation = Quaternion.LookRotation(forceVector);
        }

        holding_key = false;

    }
}
