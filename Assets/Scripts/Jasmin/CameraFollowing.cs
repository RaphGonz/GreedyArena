using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }
}