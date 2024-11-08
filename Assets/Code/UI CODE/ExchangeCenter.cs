using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeCenter : MonoBehaviour
{
    #region instance
    public static ExchangeCenter instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    public int available;
    public int cubecount;
    public int itemcount;

    public void AdjustExchange()
    {
        itemcount = (int)UIManager.instance._exchnageAdjuster.value;
        UIManager.instance._itemExchangeText.text = "X" + itemcount.ToString();
        cubecount = itemcount * 5;
        UIManager.instance._getCurrencyText.text = "X" + cubecount.ToString();
    }

    public void ExchangeToCurrency()
    {
        available -= itemcount;
        UIManager.instance._itemExchangeText.text = "X" + itemcount.ToString();
        cubecount = itemcount * 5;
        UIManager.instance._getCurrencyText.text = "X" + cubecount.ToString();

        UIManager.instance._exchnageAdjuster.maxValue = available;
        UIManager.instance._maxExchangeText.text = available.ToString();
    }
}
