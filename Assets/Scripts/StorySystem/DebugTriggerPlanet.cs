using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTriggerPlanet : MonoBehaviour {
    [SerializeField]
    private Location planet;

    public void OnClick() {
        DialogManager.Instance.StartDialog(planet);
    }
}
