using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// --Spiller skal kunne gå fritt langs x og z aksen
// Spiller skal kunne bytte mellom forskjellige entiter som er på lag
// Spiller skal kunne ta flag
// Spiller skal dø når den blir tatt av en motstander på dens territorie
// Spiller skal kunne skade fiender på eget territorie

// Hvilket flag er vi i?
// Blir vi tatt på av noen på det laget
// Gå tilbake til start


public class player_movement : MonoBehaviour
{
    // State for spillerinformasjon
    public int playerID;
    public string myTeam;
    public string enemyTeam;
    // State for bevegelse
    public float forceMultiplier = 100;
    float max_speed = 5f, acceleration = 1.1f;
    public float drag = 0.95f;
    float xVelocity = 0f, zVelocity = 0f;
    bool xholding_key, zholding_key = false;
    public bool vulnerable = false;

    public GameObject[] flags;
    public GameObject currentFlag;
    public GameObject enemy;
    public float catchDistance = 2f;
    public GameObject[] npcList;
    public bool touching;

    private bool trash = false;
    public Vector3 returnPoint = new Vector3(0,1,0);
    public List <GameObject> myFlags;

    
    // Initialiserer unity klasser
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (playerID == 0)
        {
            gameObject.tag = "player1";
            myTeam = "player 1";
            enemyTeam = "player 2";
            enemy = GameObject.FindWithTag("player2");
            returnPoint = new Vector3(-100,1,-100);
        }  
         if (playerID == 1)
        {
            gameObject.tag = "player2";
            myTeam = "player 2";
            enemyTeam = "player 1";
            enemy = GameObject.FindWithTag("player1");
            returnPoint = new Vector3(100,1,100);
        }        
        npcList = GameObject.FindGameObjectsWithTag("npc");
    }

    // Update is called once per frame
    void Update()
    {
        if (trash)
        {
            transform.position = returnPoint;
            trash = false;
        }
        touching = false;
        vulnerable = false;
        flags = GameObject.FindGameObjectsWithTag("flag");
        currentFlag = null;
        myFlags = new List<GameObject>();
        foreach (GameObject flag in flags) {
            flag_script currentFlagScript = flag.GetComponent<flag_script>();
            if (currentFlagScript.team == myTeam)
            {
                myFlags.Add(flag);
            }
            if (currentFlagScript.isInRange(gameObject))
            {
                currentFlag = flag;
            }

        }
        if (currentFlag != null)
        {
            if (!myFlags.Contains(currentFlag))
            {
                vulnerable = true;
            }
        }
        else
        {
            vulnerable = true;
        }
        float distance = 0f;
        foreach (GameObject npc in npcList)
        {
            distance = Vector3.Distance(gameObject.transform.position, npc.transform.position);
            if (distance < catchDistance)
            {
                touching = true;
            }
        }
        distance = Vector3.Distance(gameObject.transform.position, enemy.transform.position);
        if (distance < catchDistance)
        {
            touching = true;
        }
        if (touching && vulnerable)
        {
            trash = true;
        }
        // Sjekk om noen tar på deg
        // Sjekk om laget på currentflag og den som tar på deg er like
        // Om du er vulnerable, tilbake til start.




    }

    private void FixedUpdate()
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
