using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public NavPointer navPointer;

    TMP_Dropdown.OptionData defaultOption = new TMP_Dropdown.OptionData("---");


    private void Start()
    {
        // Set global state and add Terrus, this will also UpdateItems.
        GlobalState.AddKnownLocation(GameObject.Find("Terrus"));
        GlobalState.AddKnownLocation(GameObject.Find("Lunas"));

        // Create Listener.
        dropdown.onValueChanged.AddListener(delegate { SelectDestination(dropdown.options[dropdown.value].text); });

        // Hide pointer on start.
        SelectDestination(defaultOption.text);
    }

    void SelectDestination(string dest)
    {
        Debug.Log("SelectDestination");
        Transform target = null;
        float planetRadius = 0f;
        if (dest != defaultOption.text)
        {
            // find destination
            foreach (var loc in GlobalState.instance.knownLocations)
            {
                if (loc.gameObject.name == dest)
                {
                    target = loc.transform;
                    planetRadius = loc.planetRadius;
                    // TODO can set mindistance based on planet values too?
                    break;
                }
            }
        }
        navPointer.target = target;
        navPointer.planetRadius = planetRadius;
    }

    public void UpdateItems()
    {
        string selectedOption = dropdown.options[dropdown.value].text;
        List<TMP_Dropdown.OptionData> items = new List<TMP_Dropdown.OptionData>(GlobalState.instance.knownLocations.Count);
        items.Add(defaultOption);
        foreach (var loc in GlobalState.instance.knownLocations)
        {
            items.Add(new TMP_Dropdown.OptionData(loc.gameObject.name));
        }
        items.Sort((a, b) => a.text.CompareTo(b.text));
        dropdown.ClearOptions();
        dropdown.AddOptions(items);
        // Need to keep selected option selected.
        int i = 0;
        foreach (var item in items)
        {
            if (item.text == selectedOption)
            {
                break;
            }
            ++i;
        }
        if (i >= items.Count)
        {
            // didn't find object so default to 0.
            i = 0;
        }
        dropdown.value = i;
    }

}