using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtDMovingPlatform : MonoBehaviour
{
    public float speed; // Speed at which platform moves
    public Transform startPoint; // Starting point
    public Transform endPoint; // Ending point
    public bool toStart; // Whether or not platform should move to start position
    public bool togglable; // Whether or not the platform's movement can be toggled
    public bool move; // Whether or not the platform should move

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(toStart && move) // Check if platform should move to startPoint, and should be moving
        {
            if (Vector3.Distance(transform.position, startPoint.position) >= .1f) // Check if platform is greater than or equal to .1m from startPoint
            {
                transform.position -= transform.up * speed * Time.deltaTime; // Move platform towards startPoint
            }
        }

        if(!toStart && move) // Check if platform should move to endPoint, and should be moving
        {
            if(Vector3.Distance(transform.position, endPoint.position) >= .1f) // Check if platform is greater than or equal to .1m from endPoint
            {
                transform.position += transform.up * speed * Time.deltaTime; // Move platform towards endPoint
            }
        }

        if(Vector3.Distance(transform.position, startPoint.position) <= .1f && move && !togglable) // Check if platform is within .1m of of startPoint, should move, and isn't togglable
        {
            toStart = false; // Move towards endPoint
        }

        if (Vector3.Distance(transform.position, endPoint.position) <= .1f && move && !togglable) // Check if platform is within 1.m of endPoint, should move, and isn't togglable
        {
            toStart = true; // Move towards startPoint
        }       
    }

    public void ToggleMove()
    {
        move = !move; // Toggle move bool
    }
}
