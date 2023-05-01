using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Slider slider;
    public int[] maxVals;
    public Sprite[] uiSprites;
    public Image image;

    public void SetVal(float val)
    {
        slider.value = val;
    }

    public void UpgradeTo(int upgradeLevel)
    {
        image.sprite = uiSprites[upgradeLevel];
        slider.maxValue = maxVals[upgradeLevel];
        SetVal(0);
    }

    private void Start()
    {
        UpgradeTo(0);
    }


}
