using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class editCtrl : MonoBehaviour
{
    public portfolio myPortfolio;

    //editPage 필드
    public InputField symbol;
    public InputField avgCost;
    public InputField shares;
    public InputField date;

    public void buyBtnClick()
    {
        if (checkEditData() == true)
        {
            Debug.Log("BUY button Click !");
            myPortfolio.BUY(symbol.text, double.Parse(avgCost.text), Int32.Parse(shares.text), date.text, 0);
            inputsClear();
        }
        else
        {
            Debug.Log("모든 필드에 값을 입력하세요.");
        }

    }
    public void sellBtnClick()
    {
        if (checkEditData() == true)
        {
            Debug.Log("SELL button Click !");
            myPortfolio.SELL(symbol.text, double.Parse(avgCost.text), Int32.Parse(shares.text), date.text, 1);
            inputsClear();
        }
        else
        {
            Debug.Log("모든 필드에 값을 입력하세요.");
        }
    }
    void inputsClear()
    {
        symbol.text = "";
        date.text = "";
        shares.text = "";
        avgCost.text = "";
    }
    bool checkEditData()
    {
        string[] editData = { symbol.text, date.text, shares.text, avgCost.text };
        for (int i = 0; i < editData.Length; i++)
        {
            if (editData[i] == "")
            {
                return false;
            }
        }
        return true;
    }
}