using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    float max_speed = 5f, acceleration = 0.1f;
    float xVelocity = 0f, zVelocity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        max_speed = 2;        
    }

    // Update is called once per frame
    void Update()
    {
        max_speed = 2;
    }

    private void FixedUpdate()
    {
        // Sjekk input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            max_speed += 2;
        }
        // Bestem bevegelse
    }
}
