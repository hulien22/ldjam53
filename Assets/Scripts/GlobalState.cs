using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalState
{

    public static HashSet<Planet> knownLocations { get; }
    public static PlanetDropdown planetDropdown { get; set; }

    public static void AddKnownLocation(GameObject obj)
    {
        Planet planet = obj.GetComponent<Planet>();
        if (planet)
        {
            // Debug.Log(knownLocations.Count);
            // knownLocations.Add(obj);
            if (knownLocations.Add(planet) && planetDropdown)
            {
                planetDropdown.UpdateItems();
            }

        }
        Debug.Log("added planet " + planet + " | " + knownLocations.Count);
    }

    static GlobalState()
    {
        knownLocations = new HashSet<Planet>();
    }
}