using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDropper : MonoBehaviour
{
    public GameObject objToDrop; //Object to Drop
    public GameObject curObj;
    public Transform objDropPoint; //Object Drop Point

    public void DropObject()
    {
        if(curObj != null)
        {
            Destroy(curObj);
        }
        curObj = Instantiate(objToDrop, objDropPoint);
    }
}
