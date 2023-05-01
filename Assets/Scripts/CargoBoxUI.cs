using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CargoBoxUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Image image;
    public Sprite empty;
    public Sprite full;

    public string text;

    public TextMeshProUGUI textUi;

    private void Start()
    {
        image.sprite = empty;
        textUi.text = "";
    }

    public void EmptyBox()
    {
        image.sprite = empty;
        text = "";
        textUi.text = "";
    }

    public void FillBox(string t)
    {
        text = t;
        image.sprite = full;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textUi.text = text;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        textUi.text = "";
    }
}
