using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }

    private AudioSource source;
    void Awake() {
        if (Instance != null) {
            Debug.LogError("Duplicate audio manager detected.");
        }
        Instance = this;
        Instance.source = GetComponent<AudioSource>();
    }

    public AudioClip thrust;

    public static void PlaySound(AudioClip clip) {
        Instance.source.clip = clip;
        Instance.source.loop = true;
        Instance.source.Play();
    }

    public static void StartThrust() {
        Instance.source.clip = Instance.thrust;
        Instance.source.loop = true;
        Instance.source.Play();
    }

    public static void StopThrust() {
        //Instance.source.clip = clip;
        Instance.source.loop = false;
        //Instance.source.Play();
    }

    public static void PlayVoice(AudioClip clip) {
        Instance.source.PlayOneShot(clip);
    }
}
