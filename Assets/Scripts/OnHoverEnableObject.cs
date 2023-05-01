using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class OnHoverEnableObjects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject obj;

    private void Start()
    {
        obj.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        obj.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        obj.SetActive(false);
    }
}
