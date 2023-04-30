using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalState
{

    public static HashSet<Planet> knownLocations { get; }

    public static void AddKnownLocation(GameObject obj)
    {
        Planet planet = obj.GetComponent<Planet>();
        if (planet)
        {
            // Debug.Log(knownLocations.Count);
            // knownLocations.Add(obj);
            knownLocations.Add(planet);
        }
        Debug.Log("added planet " + planet + " | " + knownLocations.Count);

    }

    static GlobalState()
    {
        knownLocations = new HashSet<Planet>();
    }
}