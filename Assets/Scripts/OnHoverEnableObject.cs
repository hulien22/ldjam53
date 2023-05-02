using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class OnHoverEnableObjects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // https://forum.unity.com/threads/some-textmeshpro-fonts-dissapear-on-window-resize-target-wasm-chrome-on-windows.901190/
    // wtf is this bug??

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
