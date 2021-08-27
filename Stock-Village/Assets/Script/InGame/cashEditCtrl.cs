using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class cashEditCtrl : MonoBehaviour
{
    public portfolio myPortfolio;

    //cashEditPage 필드
    public Text cash;
    public InputField input_cash;
    public Text editedCash;

    void Update()
    {
        cash.text = myPortfolio.Cash.ToString();
    }

    public void plusBtnClick()
    {
        editedCash.text = (double.Parse(cash.text) + double.Parse(input_cash.text)).ToString();
    }
    public void minusBtnClick()
    {
        editedCash.text = (double.Parse(cash.text) - double.Parse(input_cash.text)).ToString();
    }
    public void editEndBtnClick()
    {
        if (input_cash.text == "")
        {
            Debug.Log("모든 필드에 값을 입력하세요.");
        }
        else
        {
            if(cash.text == editedCash.text) { Debug.Log("plus 또는 minus 버튼을 클릭하세요."); }
            if(double.Parse(editedCash.text) < 0) { Debug.Log("출금 가능한 현금이 부족합니다."); }
            else
            {
                myPortfolio.Cash = double.Parse(editedCash.text);

                //필드 초기화
                input_cash.text = "";
                editedCash.text = "";
            }
        }
    }
}