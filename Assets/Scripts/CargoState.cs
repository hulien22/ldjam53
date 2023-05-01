using CleverCrow.Fluid.Databases;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

using static Assets.Scripts.StorySystem.JobsGenerator;

public class CargoState : MonoBehaviour {
    private static CargoState instance;
    // private int packages = 0;

    public class CargoItem
    {
        public Location start;
        public string text;
        public CargoItem(Location s, string t)
        {
            start = s;
            text = t;
        }
    }
    private List<CargoItem> packages;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("Duplicate cargo manager detected.");
        }
        instance = this;
        instance.packages = new List<CargoItem>();
    }

    public static void AddPackage(Location start, Location target)
    {
        // instance.packages++;
        instance.packages.Add(new CargoItem(start, Assets.Scripts.StorySystem.JobsGenerator.GetTextForCargoUI(start, target)));
        GlobalState.AddKnownLocation(target);
        GlobalState.instance.cargoUIController.UpdateFilledCargo(instance.packages);
    }

    public static void RemovePackage(Location start)
    {
        instance.packages.RemoveAll(p => p.start == start);
        GlobalState.instance.cargoUIController.UpdateFilledCargo(instance.packages);
    }

    public static int GetPackageCount() {
        return instance.packages.Count;
    }
}
