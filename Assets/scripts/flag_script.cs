using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class flag_script : MonoBehaviour
{
    // Start is called before the first frame update

    // Må ha kontroll over begge spillere
    // Må ha kontroll over hvilket lag den er kontrollert av
    // Sjekk avstand mellom flag og spillere
    // Holde styr på hvor lenge den har blitt kapret i strekk
    // Bli kapret etter at en spiller er nærme nok lenge nok
    // Skifte farge når den blir tatt over

    public float captureTime = 5f;
    public float aggroRange = 30f;
    public string team = "Neutral";
    public float captureDistance = 5f;

    public score_manager scoreKeeper;
    private GameObject player1, player2;
    private float p1Distance, p2Distance;
    private float p1CaptureTime, p2CaptureTime;
    private List<GameObject> entityList;

    private Light light;
    private Light capturelight;
    private GameObject lightobject;
    private GameObject captureLightObject;

    public bool isInRange(GameObject player)
    {
        if (player == null)
        {
            return false;
        }
        return Vector3.Distance(gameObject.transform.position, player.transform.position) < aggroRange;
    }

    void Start()
    {
        player1 = GameObject.FindWithTag("player1");
        player2 = GameObject.FindWithTag("player2");
        GameObject gm = GameObject.Find("GameManager");
        //scoreKeeper = gm.GetComponent<score_manager>();
        p1CaptureTime = 0;
        p2CaptureTime = 0;
        transform.position = new Vector3(transform.position.x, 1.62f, transform.position.z);
        // Generate light
        GameObject lightobject = gameObject.transform.GetChild(0).gameObject;
        GameObject captureLightObject = gameObject.transform.GetChild(1).gameObject;
        light = lightobject.GetComponent<Light>();
        capturelight = captureLightObject.GetComponent<Light>();
        light.color = new Color(0.6f,0.3f,0.6f); //*0.8f + 0.2f * Color.blue; 
        capturelight.color = 0.5f*Color.green + 0.5f * light.color;
        lightobject.transform.position = gameObject.transform.position; 
        captureLightObject.transform.position = gameObject.transform.position;
        InvokeRepeating("timer", 5f, 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        p1Distance = Vector3.Distance(player1.transform.position, gameObject.transform.position);
        p2Distance = Vector3.Distance(player2.transform.position, gameObject.transform.position);
        if (team != "player 1")
        {
            if (p1Distance > captureDistance)
            {
                p1CaptureTime -= 0.01f;
                if (p1CaptureTime < 0)
                {
                    p1CaptureTime = 0;
                }
            }
            else if (p1Distance < captureDistance)
            {
                p1CaptureTime += 0.02f;
            }
        }
        if (team != "player 2")
        {
            if (p2Distance > captureDistance)
            {
                p2CaptureTime -= 0.01f;
                if (p2CaptureTime < 0)
                {
                    p2CaptureTime = 0;
                }
            }
            else if (p2Distance < captureDistance)
            {
                p2CaptureTime += 0.02f;
            }
        }
        if (p1CaptureTime >= captureTime)
        {
            team = "player 1";
            p1CaptureTime = 0;
        }
        else if (p2CaptureTime >= captureTime)
        {
            team = "player 2";
            p2CaptureTime = 0;
        }
        if (team == "player 1")
        {
            light.color = Color.red;
            capturelight.color = 0.33f * Color.white +  0.67f * light.color;
            
        }
        else if (team == "player 2")
        {
            light.color = Color.blue;
            capturelight.color = 0.33f * Color.white +  0.67f * Color.blue;
        }
    }

    public void timer() {
        if (scoreKeeper == null)
        {
            return;
        }
        if (team == "player 1")
        {
            scoreKeeper.increaseScore(1);
        }
        else if (team == "player 2")
        {
            scoreKeeper.increaseScore(2);
        }
    }


}
