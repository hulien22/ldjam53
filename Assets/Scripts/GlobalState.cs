using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour
{
    public static GlobalState instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Duplicate audio manager detected.");
        }
        instance = this;
        knownLocations = new HashSet<Planet>();
        lastPlanetVisited = terrus.GetComponent<Planet>();
    }

    public HashSet<Planet> knownLocations { get; set; }
    public PlanetDropdown planetDropdown;

    public Planet lastPlanetVisited;

    [SerializeField] private GameObject terrus;
    [SerializeField] private GameObject donatus;
    [SerializeField] private GameObject kantora;
    [SerializeField] private GameObject alban;
    [SerializeField] private GameObject scrapton;
    [SerializeField] private GameObject haldor;
    [SerializeField] private GameObject erabus;
    [SerializeField] private GameObject iris1;
    [SerializeField] private GameObject iris2;
    [SerializeField] private GameObject iris3;
    [SerializeField] private GameObject lunas;
    [SerializeField] private GameObject miram;
    [SerializeField] private GameObject taldoris;

    public static GameObject GetPlanet(Location location)
    {
        switch (location)
        {
            case Location.Terrus:
                return instance.terrus;
            case Location.Donatus:
                return instance.donatus;
            case Location.Kantora:
                return instance.kantora;
            case Location.Alban:
                return instance.alban;
            case Location.Scrapton:
                return instance.scrapton;
            case Location.Haldor:
                return instance.haldor;
            case Location.Erabus:
                return instance.erabus;
            case Location.Iris1:
                return instance.iris1;
            case Location.Iris2:
                return instance.iris2;
            case Location.Iris3:
                return instance.iris3;
            case Location.Lunas:
                return instance.lunas;
            case Location.Miram:
                return instance.miram;
            case Location.Taldoris:
                return instance.taldoris;
            default:
                Debug.LogError($"Got side quest from {location} which does not have side quests.");
                return instance.terrus;
        }
    }

    public static void AddKnownLocation(Location loc)
    {
        AddKnownLocation(GetPlanet(loc));
    }

    public static void AddKnownLocation(GameObject obj)
    {
        Planet planet = obj.GetComponent<Planet>();
        if (planet)
        {
            // Debug.Log(knownLocations.Count);
            // knownLocations.Add(obj);
            if (instance.knownLocations.Add(planet) && instance.planetDropdown)
            {
                instance.planetDropdown.UpdateItems();
            }

        }
        Debug.Log("added planet " + planet + " | " + instance.knownLocations.Count);
    }
}