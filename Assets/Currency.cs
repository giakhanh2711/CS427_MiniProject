using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField] int amount;
    [SerializeField] TMPro.TextMeshProUGUI text;

    private void Start()
    {
        //amount = 0;
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = amount.ToString();
    }
    public void AddMoney(int moneyGain)
    {
        amount += moneyGain;
        UpdateText();
    }

    public bool CheckEnoughMoney(int price)
    {
        return amount >= price;
    }

    internal void DecreaseMoney(int price)
    {
        amount -= price;

        if (amount < 0)
            amount = 0;

        UpdateText();
    }
}
