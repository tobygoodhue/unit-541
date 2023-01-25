using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtDMovingPlatform : MonoBehaviour
{
    public float speed; // Speed at which platform moves
    public Transform startPoint; // Starting point
    public Transform endPoint; // Ending point
    public bool toStart;
    public bool togglable;
    public bool move;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(toStart && move)
        {
            if (Vector3.Distance(transform.position, startPoint.position) >= .1f)
            {
                transform.position -= transform.up * speed * Time.deltaTime;
            }
        }

        if(!toStart && move)
        {
            if(Vector3.Distance(transform.position, endPoint.position) >= .1f)
            {
                transform.position += transform.up * speed * Time.deltaTime;
            }
        }

        if(Vector3.Distance(transform.position, startPoint.position) <= .1f && move && !togglable)
        {
            toStart = false;
        }

        if (Vector3.Distance(transform.position, endPoint.position) <= .1f && move && !togglable)
        {
            toStart = true;
        }       
    }

    public void ToggleMove()
    {
        move = !move;
    }
}
