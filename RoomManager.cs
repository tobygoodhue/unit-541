using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> rooms; // List of all rooms
    public List<AudioClip> roomDialogues; // List of all basic room dialogues
    public int curRoom;
    public Narrator narrator;

    // Start is called before the first frame update
    void Start()
    {
        print(roomDialogues[curRoom].ToString());
        narrator.PlayDialogue(roomDialogues[curRoom]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementRoom()
    {
        if (curRoom <= rooms.Count - 2)
        {
            curRoom++;
            narrator.PlayDialogue(roomDialogues[curRoom]);
        }
    }
}
