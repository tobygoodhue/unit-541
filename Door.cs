using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float speed; // Speed at which door opens/closes
    public Transform doorTrans; // Door transform
    public Transform doorCloseTrans; // Point door is at when closed
    public Transform doorOpenTrans; // Point door is at when opened

    private bool openTick; // Boolean to make sure isOpen doesn't get triggered repeatedly when opening
    private bool closeTick; // Boolean to make sure !isOpen doesn't get triggered repeatedly when closing
    public bool isOpen; // Boolean to indicate if door is open or not
    private float doorOpenStartTime; // Time door starts to open
    private float doorCloseStartTime; // Time door starts to close
    private float doorTravelDist; // Distance door has to travel between close and open points
    private float dcot; // Door Close/Open Time
    private Transform doorTargetTrans; // Door Target Transform

    void Start()
    {
        doorTravelDist = Vector3.Distance(doorCloseTrans.position, doorOpenTrans.position); // Distance between door open and close postions
    }

    void Update()
    {
        if(isOpen) // Set door target position based on isOpen boolean
        {
            doorTargetTrans = doorOpenTrans;
        }
        else
        {
            doorTargetTrans = doorCloseTrans;
        }

        float dc = (Time.time - dcot) * speed; // Distance Covered
        float foj = dc / doorTravelDist; // Fraction of Journey

        if (isOpen)
        {
           doorTrans.position = Vector3.Lerp(doorCloseTrans.position, doorOpenTrans.position, foj); // Lerp door to open position
        }
        
        if(!isOpen)
        {
            doorTrans.position = Vector3.Lerp(doorOpenTrans.position, doorCloseTrans.position, foj); // Lerp door to closed position
        }
    }
    public void OpenDoor()
    {
        if(!openTick) // Make sure door opening only gets triggered once
        {
            isOpen = true;
            dcot = Time.time; // Set Door Close/Open Time to the time that OpenDoor is called
        }

        openTick = true; // Toggle openTick so OpenDoor doesn't get called repeatedly
    }

    public void CloseDoor()
    {
        if(!closeTick) // Make sure door closing only gets triggered once
        {
            isOpen = false;
            dcot = Time.time; // Set Door Close/Open Time to the time that CloseDoor is called
        }

        closeTick = true; // Toggle closeTick so CloseDoor doesn't get called repeatedly
    }
}
