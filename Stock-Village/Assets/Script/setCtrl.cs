using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class setCtrl : MonoBehaviour
{
    public GameObject optionPage;
    public GameObject editPage;
    public GameObject setPage;
    public GameObject cashEditPage;

    public portfolio myPortfolio;

    //setPage 필드
    public Text cash;
    public Text invest;
    public Text gain;
    public Text gainRatio;

    void Update()
    {
        cash.text = myPortfolio.Cash.ToString();
    }

    public void setBtnClick()
    {
        optionPage.SetActive(false);
        editPage.SetActive(false);

        //창이 열려있으면
        if (setPage.activeSelf == true)
        {
            setPage.SetActive(false);
        }
        else
        {
            setPage.SetActive(true);
            cash.text = myPortfolio.Cash.ToString();
            invest.text = myPortfolio.update_Total_StInvest().ToString();
            gain.text = myPortfolio.totalGain.ToString();
            gainRatio.text = ((myPortfolio.update_Total_Stprice() / myPortfolio.update_Total_StInvest()) * 100).ToString();
        }
    }
    public void cashEditBtnClick()
    {
        optionPage.SetActive(false);
        editPage.SetActive(false);
        setPage.SetActive(false);
        cashEditPage.SetActive(true);
    }
}
