using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FtBMovingPlatform : MonoBehaviour
{
    public Transform startPoint; // Starting position of platform
    public Transform endPoint; // Ending poistion of platform
    public float speed; // Speed at which platform moves
    public bool move; // Whether or not the platform should move

    private bool toStart;

    // Start is called before the first frame update
    void Start()
    {
        toStart = false; // Set platform to move to endPoint
    }

    // Update is called once per frame
    void Update()
    {
        if (toStart) // Check if platform needs to move to startPoint
        {
            if(Vector3.Distance(transform.position, startPoint.position) > .05f) // Move platform to startPoint if it is greater than .05m away from startPoint
            {
                transform.position += transform.forward * -speed * Time.deltaTime;
            }
        }
        else // Check if platform needs to move to endPoint
        {
            if (Vector3.Distance(transform.position, endPoint.position) > .05f) // Move platform to endPoint if it is greater than .05m away from endPoint
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }

        if(Vector3.Distance(transform.position, endPoint.position) <= .05f && !toStart) // Check if platform is within .05m of endPoint
        {
            toStart = true; // Start moving platform to startPoint
        }

        if(Vector3.Distance(transform.position, startPoint.position) <= .05f && toStart) // Check if platform is within .05m of startPoint
        {
            toStart = false; // Start moving platform to endPoint
        }
    }
}
