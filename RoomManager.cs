using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> rooms; // List of all rooms
    public List<AudioClip> roomDialogues; // List of all basic room dialogues
    public int curRoom; // Current room number
    public Narrator narrator; // Narrator object

    // Start is called before the first frame update
    void Start()
    {
        print(roomDialogues[curRoom].ToString()); // Print current room diaglogue name
        narrator.PlayDialogue(roomDialogues[curRoom]); // Play starting room dialogue
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementRoom()
    {
        if (curRoom <= rooms.Count - 2) // Check to make sure curRoom doesn't go out of bounds of rooms
        {
            curRoom++; // Increment room
            narrator.PlayDialogue(roomDialogues[curRoom]); // Play current room diaglogue
        }
    }
}
