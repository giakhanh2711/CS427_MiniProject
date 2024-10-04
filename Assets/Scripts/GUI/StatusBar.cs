using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Slider bar;

    public void Set(int curVal, int maxVal)
    {
        bar.maxValue = maxVal;
        bar.value = curVal;

        text.text = curVal.ToString() + "/" + maxVal.ToString();
    }
}
