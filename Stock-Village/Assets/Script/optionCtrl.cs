using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionCtrl : MonoBehaviour
{
    public GameObject optionPage;
    public GameObject editPage;
    public GameObject setPage;
    public GameObject cashEditPage;

    public portfolio myPortfolio;
    public void optionBtnClick()
    {
        editPage.SetActive(false);
        setPage.SetActive(false);

        //창이 열려있으면
        if (optionPage.activeSelf == true)
        {
            optionPage.SetActive(false);
        }
        else
        {
            optionPage.SetActive(true);
        }
    }
}
