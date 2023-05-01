using CleverCrow.Fluid.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class CargoState : MonoBehaviour {
    private static CargoState instance;
    private int packages = 0;
    private void Awake() {
        if (instance != null) {
            Debug.LogError("Duplicate cargo manager detected.");
        }
        instance = this;
    }

    public static void AddPackage(Location loc)
    {
        instance.packages++;
        GlobalState.AddKnownLocation(loc);
    }

    public static void RemovePackage() {
        instance.packages--;
    }

    public static int GetPackageCount() {
        return instance.packages;
    }
}
