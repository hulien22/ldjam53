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

    public static void PlaySound(AudioClip clip) {
        Instance.source.clip = clip;
        Instance.source.loop = true;
        Instance.source.Play();
    }
}
