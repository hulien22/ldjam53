using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Slider slider;

    public int maxVal;
    public int[] startVals;
    public int startVal;
    public Sprite[] uiSprites;
    public Image image;

    // val is damage / fuel used
    public void SetVal(float val)
    {
        slider.value = val + startVal;
    }

    public void UpgradeTo(int upgradeLevel)
    {
        image.sprite = uiSprites[upgradeLevel];
        startVal = startVals[upgradeLevel];
        SetVal(0);
    }

    private void Start()
    {
        UpgradeTo(0);
        slider.maxValue = maxVal;
    }


}
