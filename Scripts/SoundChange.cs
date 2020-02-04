using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChange : MonoBehaviour {

    public AudioSource audios;
    public AudioClip sounds;
    bool onceChange;

	// Use this for initialization
	void Start () {

        onceChange = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        if (!onceChange && Checkpoint.sendStart)
        {
            onceChange = true;
            audios.clip = sounds;
            audios.Play();
        }
	}
}
