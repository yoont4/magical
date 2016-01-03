using UnityEngine;
using System.Collections;

public class SoundManager2 : MonoBehaviour {

    public static AudioSource audio;
    public AudioSource audioSource;
    public static AudioClip gameover;
    public AudioClip gameoverSound;

	// Use this for initialization
	void Start () {
        audio = audioSource;
        gameover = gameoverSound;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void pause()
    {
        if (audio.isPlaying) 
        {
            audio.Pause();
        }
        
    }

    public static void unpause()
    {
        if (!audio.isPlaying)
        {
            audio.UnPause();
        }
    }

    public static void terminate()
    {
        audio.clip = gameover;
        audio.loop = false;
        audio.Play();
    }
}
