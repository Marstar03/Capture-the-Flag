using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    public float height = 20f;
    public float xOffset = 0f;
    public int playerID;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        while (true)
        {
            if (playerID == 0) 
            {
                player = GameObject.FindWithTag("player1");
                break;
            }
            else if (playerID == 1) 
            {
                player = GameObject.FindWithTag("player2");
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            transform.position = new Vector3(player.transform.position[0], height, player.transform.position[2]-xOffset);
        }

    }
}
