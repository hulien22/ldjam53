using CleverCrow.Fluid.Dialogues;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour {
    private static VoiceManager instance;
    private void Awake() {
        if (instance != null) {
            Debug.LogError("Duplicate voice manager detected.");
        }
        instance = this;
    }

    public ActorDefinition[] actors;
    public AudioClip[] voices;


    public static AudioClip GetVoice(IActor actor) {
        int c = 0;
        foreach (IActor possible in instance.actors) {
            if (possible.DisplayName == actor.DisplayName) {
                Debug.Log(actor + " " + possible + " " + c);
                int v = Random.Range(c, c+3);
                Debug.Log(v);
                Debug.Log(instance.voices[v]);
                return instance.voices[v];
            }
            c+=3;
        }
        return instance.voices[0];
    }
}
