using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent ftc; // Function to Call;

    public void PushButton()
    {
        ftc.Invoke(); // Call assigned function
    }
}
