using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music_Track_Controller : MonoBehaviour
{

    public AudioClip Track1;
   //public AudioClip Track2;

    private AudioSource audio_source;

    private bool playedTrack1 = false;
    private bool playedTrack2 = false;

	// Use this for initialization
	void Start ()
    {
        audio_source = GetComponent<AudioSource>();

        audio_source.clip = Track1;
        audio_source.Play();
        playedTrack1 = true;
        playedTrack2 = false;
	}


    void Update()
    {
        if (!audio_source.isPlaying)
        {
            if (!playedTrack1)
            {
                audio_source.clip = Track1;
                playedTrack1 = true;
                playedTrack2 = false;
                audio_source.volume = 0.05f;
                audio_source.PlayDelayed(2);
            }
            else if (!playedTrack2)
            {
                //audio_source.clip = Track2;
                playedTrack2 = true;
                playedTrack1 = false;
                audio_source.volume = 1.0f;
                audio_source.PlayDelayed(2);
            }
        }
    }
}
