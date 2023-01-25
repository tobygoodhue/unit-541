using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    public List<AudioClip> voiceClips; // List of all voice clips
    public List<GameObject> objs; // Objects to call on

    private float timer; //Timer
    private bool count; // Boolean to indicate whether or not the timer should be decremented
    private bool room1DialogueHasBeenTriggered; // Indicates if the dialogue for Room 1 has already been played
    private AudioSource source; // Audio source on the narrator

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>(); // Assign the audio source
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            source.clip = null;
        }
    }
    public void PlayDialogue(AudioClip dtp) // Dialogue to Play
    {
        source.clip = dtp;
        source.Play();
    }
}
