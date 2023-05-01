using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CargoUIController : MonoBehaviour
{
    public CargoBoxUI[] cargoBoxes;
    private int maxCargo;

    private void Start()
    {
        UpdateNumTotalCargo(3);
    }


    public void UpdateNumTotalCargo(int num)
    {
        for (int i = 0; i < cargoBoxes.Length; i++)
        {
            cargoBoxes[i].gameObject.SetActive(i < num);
        }
        maxCargo = num;
    }

    public void UpdateFilledCargo(CargoState.CargoItem specialPackage, List<CargoState.CargoItem> packages)
    {
        Debug.Log("UPDATEFILLED");
        if (specialPackage != null)
        {
            cargoBoxes[0].FillBox(specialPackage.text);
        }
        else
        {
            cargoBoxes[0].EmptyBox();
        }
        for (int i = 1; i < maxCargo; i++)
        {
            if ((i - 1) < packages.Count)
            {
                cargoBoxes[i].FillBox(packages[(i - 1)].text);
            }
            else
            {
                cargoBoxes[i].EmptyBox();
            }
        }
    }
}