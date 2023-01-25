using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FtBMovingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed;
    public bool move;

    private bool toStart;

    // Start is called before the first frame update
    void Start()
    {
        toStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (toStart)
        {
            if(Vector3.Distance(transform.position, startPoint.position) > .05f)
            {
                transform.position += transform.forward * -speed * Time.deltaTime;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, endPoint.position) > .05f)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }

        if(Vector3.Distance(transform.position, endPoint.position) <= .05f && !toStart)
        {
            toStart = true;
        }

        if(Vector3.Distance(transform.position, startPoint.position) <= .05f && toStart)
        {
            toStart = false;
        }
    }
}
