
using System.ComponentModel;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {
    private static UpgradeManager instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("Duplicate location manager detected.");
            return;
        }
        instance = this;
    }

    [SerializeField]
    private Rocket rocket;

    [SerializeField]
    private GameObject mists;


    public static void SetThrust(int thrust) {
        instance.rocket.thrustModifier = thrust + 1;
    }

    public static void SetHull(int hull) {
        instance.rocket.maxHealth = 100 * hull + 100;
        instance.rocket.health = 100 * hull + 100;
        GlobalState.instance.healthBar.UpgradeTo(hull);
        if (hull >= 3) {
            instance.rocket.immuneToSun = true;
        }
    }

    public static void SetFuelCapacity(int fuelCapacity) {
        instance.rocket.maxFuel = 1000 * (fuelCapacity + 1);
        instance.rocket.fuel = 1000 * (fuelCapacity + 1);
        GlobalState.instance.fuelBar.UpgradeTo(fuelCapacity);
    }

    public static void SetSensor() {
        instance.rocket.sensor = true;
        instance.mists.SetActive(false);
    }
}
