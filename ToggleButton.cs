using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleButton : MonoBehaviour
{
    public UnityEvent ftc; // Function to Call
    private bool hbp; // Has Been Pressed

    public void PushButton()
    {
        if(!hbp) // Make sure ftc doesn't get called more than once
        {
            ftc.Invoke(); // Invoke assigned function
            hbp = true; // Toggle hpb
        }
    }
}
