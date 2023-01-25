using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiscFuncTriggerSingleUse : MonoBehaviour
{
    public UnityEvent ftc; // Function to Trigger

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ftc.Invoke();
            Destroy(gameObject);
        }
    }
}
