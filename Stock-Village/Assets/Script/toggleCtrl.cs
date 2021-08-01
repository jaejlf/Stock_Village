using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class toggleCtrl : MonoBehaviour
{
    public StockList stockList;
    public portfolio myPortfolio;

    public Toggle mystockCk; //only my stocks 모드 체크
    public GameObject tog0; //토글 on,off 체크 표시

    public Toggle weather; //weather 옵션 체크
    public GameObject tog1; //토글 on,off 체크 표시
    public Toggle mktime; //market time 옵션 체크
    public GameObject tog2; //토글 on,off 체크 표시
    public Toggle scale; //scaling 옵션 체크
    public GameObject tog3; //토글 on,off 체크 표시

    public void toggle_mystock()
    {
        if (mystockCk.isOn)
        {
            tog0.SetActive(true);
            GameObject.Find("InGameControl").GetComponent<InGame>().portfolioBtnClick();
        }
        else
        {
            tog0.SetActive(false);
            GameObject.Find("InGameControl").GetComponent<InGame>().totalBtnClick();
        }
    }
    public void toggle_weather()
    {

        //weather 옵션
        if (weather.isOn)
        {
            tog1.SetActive(true);
        }
        else
        {
            tog1.SetActive(false);
        }
    }
    public void toggle_mktime()
    {
        //market time 옵션
        if (mktime.isOn)
        {
            tog2.SetActive(true);
        }
        else
        {
            tog2.SetActive(false);
        }
    }
    public void toggle_scale()
    {
        //scale 옵션
        if (scale.isOn)
        {
            tog3.SetActive(true);
            GameObject.Find("InGameControl").GetComponent<InGame>().settingPortfolio();
        }
        else
        {
            tog3.SetActive(false);
            GameObject.Find("InGameControl").GetComponent<InGame>().settingPortfolio();
        }
    }
}
