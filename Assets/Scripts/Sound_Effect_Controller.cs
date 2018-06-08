using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Effect_Controller : MonoBehaviour {

    public AudioClip CaveInSound;
    public AudioClip[] sound = new AudioClip[7];
    AudioSource audio_effects;

    void Start()
    {
        audio_effects = GetComponent<AudioSource>();

        if(GameManager.DidPlayCaveInSound())
        {
            audio_effects.PlayOneShot(CaveInSound, 1f);
        }

        InvokeRepeating("PlayClipAndChange", 0.01f, 10.0f);
    }

    void PlayClipAndChange()
    {
        //audio_effects.clip = sound[Random.Range(0, 5)];
        audio_effects.PlayOneShot(sound[Random.Range(0, 5)], 1f);
    }
}
