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
    public string team = "Neutral";
    public float captureDistance = 5f;

    private GameObject player1, player2;
    private float p1Distance, p2Distance;
    private float p1CaptureTime, p2CaptureTime;

    private Light light;
    private Light capturelight;
    private GameObject lightobject;
    private GameObject captureLightObject;
    void Start()
    {
        player1 = GameObject.FindWithTag("player1");
        player2 = GameObject.FindWithTag("player2");
        p1CaptureTime = 0;
        p2CaptureTime = 0;

        // Generate light
        GameObject lightobject = gameObject.transform.GetChild(0).gameObject;
        GameObject captureLightObject = gameObject.transform.GetChild(1).gameObject;
        light = lightobject.GetComponent<Light>();
        capturelight = captureLightObject.GetComponent<Light>();
        light.color = Color.green;
        capturelight.color = 0.33f * Color.white +  0.67f * Color.green;
        lightobject.transform.position = gameObject.transform.position; 
        captureLightObject.transform.position = gameObject.transform.position;
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
            capturelight.color = 0.33f * Color.white +  0.67f * Color.red;
        }
        else if (team == "player 2")
        {
            light.color = Color.blue;
            capturelight.color = 0.33f * Color.white +  0.67f * Color.blue;
        }
    }
}
