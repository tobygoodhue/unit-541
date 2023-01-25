using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeSwitch : MonoBehaviour
{
    public UnityEvent funcToCall; // Function that gets called when the switch is activated
    public enum switchTypes
    {
        Yellow,
        Red,
        Green
    };

    public switchTypes switchType; // Switch's type

    private void OnCollisionEnter(Collision collision)
    {
        switch(switchType) // Check the color of the box
        {
            case switchTypes.Yellow:
                if (collision.collider.tag == "Cube" && collision.collider.GetComponent<Cube>().thisColor == Cube.colors.Yellow)
                {
                    funcToCall.Invoke(); // Call set function
                }
                break;

            case switchTypes.Red:
                if (collision.collider.tag == "Cube" && collision.collider.GetComponent<Cube>().thisColor == Cube.colors.Red)
                {
                    funcToCall.Invoke(); // Call set function
                }
                break;

            case switchTypes.Green:
                if (collision.collider.tag == "Cube" && collision.collider.GetComponent<Cube>().thisColor == Cube.colors.Green)
                {
                    funcToCall.Invoke(); // Call set function
                }
                break;

            default:
                break;
        }
    }
}
