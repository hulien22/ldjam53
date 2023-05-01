
using CleverCrow.Fluid.Databases;
using UnityEngine;

public class LocationManager : MonoBehaviour {
    private static LocationManager instance;
    [SerializeField]
    private KeyValueDefinitionInt location;
    private void Awake() {
        if (instance != null) {
            Debug.LogError("Duplicate location manager detected.");
            return;
        }
        instance = this;
    }

    public static Location GetLocation() {
        return (Location)GlobalDatabaseManager.Instance.Database.Ints.Get(instance.location.Key, instance.location.defaultValue);
    }

    public static void SetLocation(Location location) {
        GlobalDatabaseManager.Instance.Database.Ints.Set(instance.location.Key, (int)location);
    }
}

