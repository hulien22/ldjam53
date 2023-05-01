using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource background;
    [SerializeField] private float backgroundVolume;
    private float backgroundVolumeTarget = 0;
    void Awake() {
        if (Instance != null) {
            Debug.LogError("Duplicate audio manager detected.");
        }
        Instance = this;
    }

    public static void PlaySound(AudioClip clip) {
        Instance.source.clip = clip;
        Instance.source.loop = true;
        Instance.source.Play();
    }

    public static void PlayVoice(AudioClip clip) {
        Instance.source.PlayOneShot(clip);
    }

    public static void LowerBackground() {
        Instance.backgroundVolumeTarget = 0;
    }

    public static void RaiseBackground() {
        Instance.backgroundVolumeTarget = Instance.backgroundVolume;
    }

    private void FixedUpdate() {
        if (Instance.background.volume < Instance.backgroundVolumeTarget) {
            Instance.background.volume += 0.01f;
            if (Instance.background.volume >= Instance.backgroundVolumeTarget) {
                Instance.background.volume = Instance.backgroundVolumeTarget;
            }
        } else if (Instance.background.volume > Instance.backgroundVolumeTarget){
            Instance.background.volume -= 0.05f;
            if (Instance.background.volume <= Instance.backgroundVolumeTarget) {
                Instance.background.volume = Instance.backgroundVolumeTarget;
            }
        }
    }
}
