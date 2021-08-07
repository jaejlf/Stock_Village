using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class profileCtrl : MonoBehaviour
{
    public GameObject setPage;
    public GameObject optionPage;
    public GameObject editPage;
    public GameObject profilePage;
    public GameObject cashEditPage;

    public portfolio myPortfolio;

    //setPage ÇÊµå
    public Text cash;
    public Text invest;
    public Text gain;

    void Update()
    {
        double gainRatio;
        cash.text = myPortfolio.Cash.ToString();
        invest.text = myPortfolio.update_Total_StInvest().ToString();
        if(myPortfolio.update_Total_StInvest() != 0) {
            gainRatio = (myPortfolio.update_Total_Stprice() / myPortfolio.update_Total_StInvest()) * 100;
            gain.text = myPortfolio.totalGain.ToString() + " (" + gainRatio.ToString() + " %)";
        }
        else
        {
            gain.text = myPortfolio.totalGain.ToString() + " (0 %)";
        }
    }
    public void cashEditBtnClick()
    {
        closeAll();
        cashEditPage.SetActive(true);
    }
    void closeAll()
    {
        setPage.SetActive(false);
        optionPage.SetActive(false);
        editPage.SetActive(false);
        profilePage.SetActive(false);
        cashEditPage.SetActive(false);
    }
}