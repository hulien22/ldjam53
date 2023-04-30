using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour {
    public static WorldState Instance { get; private set; }
    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Duplicate instance of world state.");
        }
        Instance = this;
    }

    public float Fuel;
    public bool HasSunKey;
}
