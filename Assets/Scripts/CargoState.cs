using CleverCrow.Fluid.Databases;
using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Search;
using UnityEngine;

using static Assets.Scripts.StorySystem.JobsGenerator;

public class CargoState : MonoBehaviour {
    public static CargoState instance;
    // private int packages = 0;

    public class CargoItem
    {
        public Location start;
        public Location target;
        public string text;
        public CargoItem(Location s, Location t, string name)
        {
            start = s;
            target = t;
            text = name;
        }
    }
    private List<CargoItem> packages;
    public CargoItem specialCargo;
    public int terrusUpgrades;
    public int kantoraUpgrades;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("Duplicate cargo manager detected.");
        }
        instance = this;
        instance.packages = new List<CargoItem>();
        instance.specialCargo = null;
    }

    public static void AddPackage(Location start, Location target)
    {
        // instance.packages++;
        instance.packages.Add(new CargoItem(start, target, Assets.Scripts.StorySystem.JobsGenerator.GetTextForCargoUI(start, target)));
        GlobalState.AddKnownLocation(target);
        GlobalState.instance.cargoUIController.UpdateFilledCargo(instance.specialCargo, instance.packages);
    }

    public static void RemovePackage(Location start)
    {
        instance.packages.RemoveAll(p => p.start == start);
        GlobalState.instance.cargoUIController.UpdateFilledCargo(instance.specialCargo, instance.packages);
    }

    public static int GetPackageCount() {
        int i = (instance.specialCargo == null ? 0 : 1);
        return instance.packages.Count + i;
    }

    public static void AddCargoUpgrade(int val, Location loc)
    {
        if (loc == Location.Terrus)
        {
            instance.terrusUpgrades = val;
        }
        else
        {
            instance.kantoraUpgrades = val;
        }
        GlobalState.instance.cargoUIController.UpdateNumTotalCargo(instance.terrusUpgrades + instance.kantoraUpgrades + 3);
    }

    public static void AddSpecialCargo(Location start, Location target, string name)
    {
        string desc = Assets.Scripts.StorySystem.JobsGenerator.GetTextForSpecialCargo(start, target, name);
        instance.specialCargo = new CargoItem(start, target, desc);
        GlobalState.instance.cargoUIController.UpdateFilledCargo(instance.specialCargo, instance.packages);
    }
    public static void RemoveSpecialCargo()
    {
        instance.specialCargo = null;
        GlobalState.instance.cargoUIController.UpdateFilledCargo(instance.specialCargo, instance.packages);
    }
}
