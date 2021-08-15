using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class pageCtrl : MonoBehaviour
{
    public GameObject setPage;
    public GameObject optionPage;
    public GameObject editPage;
    public GameObject profilePage;
    public GameObject cashEditPage;

    public bool popUp; //팝업창이 열려있는지 확인
    List<GameObject> PageList = new List<GameObject>();

    void Start()
    {
        popUp = false;
        PageList.Add(setPage);
        PageList.Add(optionPage);
        PageList.Add(editPage);
        PageList.Add(profilePage);
        PageList.Add(cashEditPage);
    }
    public void setButtonClick()
    {
        //열려있는 창이 있으면 모든 창 내리기
        if(popUp == true)
        {
            closeAll();
            popUp = false; //모든 팝업창이 닫혀있음
        }
        //모든 창이 닫혀있다면 setPage open
        else
        {
            setPage.SetActive(true);
            popUp = true; //팝업창이 열려있음
        }
    }
    public void opBtnClick()
    {
        closeAll(); //모든 창 내리기
        optionPage.SetActive(true);
    }
    
    public void edBtnClick()
    {
        closeAll(); //모든 창 내리기
        editPage.SetActive(true);
    }

    public void profileBtnClick()
    {
        closeAll(); //모든 창 내리기
        profilePage.SetActive(true);
    }

    public void backBtnClick()
    {
        closeAll(); //모든 창 내리기
        setPage.SetActive(true);
    }

    public void cashEd_backBtnClick()
    {
        closeAll(); //모든 창 내리기
        profilePage.SetActive(true);
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
