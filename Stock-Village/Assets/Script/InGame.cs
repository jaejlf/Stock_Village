using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InGame : MonoBehaviour
{
    public StockList stockList;
    public portfolio myPortfolio;

    public Text totalGain;

    public Toggle mystockCk; //only my stocks 모드 체크
    public GameObject tog0; //토글 on,off 체크 표시

    public GameObject[] totalMode; //totalMode 건물 오브젝트
    public GameObject[] portfolioMode; //portfolioMode 건물 오브젝트
    public GameObject[] others; //꾸미기용 건물 오브젝트

    public Toggle weather; //weather 옵션 체크
    public GameObject tog1; //토글 on,off 체크 표시
    public Toggle mktime; //market time 옵션 체크
    public GameObject tog2; //토글 on,off 체크 표시
    public Toggle scale; //scaling 옵션 체크
    public GameObject tog3; //토글 on,off 체크 표시

    void Start()
    {
        myPortfolio.renew = false; //포트폴리오 갱신 플래그 false
        totalBtnClick();
    }
    void Update()
    {
        if (mystockCk.isOn)
        {
            tog0.SetActive(true);
            portfolioBtnClick();
        }
        else
        {
            tog0.SetActive(false);
            totalBtnClick();
        }
        //포트폴리오가 갱신된 경우
        if (myPortfolio.renew)
        {
            settingPortfolio();
            myPortfolio.renew = false;
        }
        toggleControl();
        
    }
    //totalMode
    public void totalBtnClick()
    {
        //portfolioMode 태그 비활성화
        foreach (GameObject tmp in myPortfolio.pfBuildings)
        {
            tmp.SetActive(false);
        }
        //totalMode 태그 활성화
        for (int i = 0; i < totalMode.Length; i++)
        {
            totalMode[i].SetActive(true);
        }
        //others 태그 활성화
        for (int i = 0; i < others.Length; i++)
        {
            others[i].SetActive(true);
        }
    }

    //portfolioMode
    public void portfolioBtnClick()
    {
        //totalMode 태그 비활성화
        for (int i = 0; i < totalMode.Length; i++)
        {
            totalMode[i].SetActive(false);
        }
        //others 태그 비활성화
        for (int i = 0; i < others.Length; i++)
        {
            others[i].SetActive(false);
        }
        //portfolioMode 태그 활성화
        for (int i = 0; i < portfolioMode.Length; i++)
        {
            portfolioMode[i].SetActive(true);
        }
        settingPortfolio(); //포트폴리오 건물 설치
    }

    //포트폴리오 건물 설치
    void settingPortfolio()
    {
        string path = "";

        //포트폴리오 건물 전체 삭제
        foreach (GameObject tmp in myPortfolio.pfBuildings) { Destroy(tmp); }
        myPortfolio.pfBuildings.Clear();

        //전체 수익금 = 전체 평가금액 - 나의 전체 투자금액
        totalGain.text = (myPortfolio.update_Total_Stprice() - myPortfolio.update_Total_StInvest()).ToString();

        //건물 설치
        foreach (var key in myPortfolio.stockInfo.Keys.ToList())
        {
            if (myPortfolio.stockInfo[key].shares == 0) { continue; } //보유수량이 0인 경우 제외

            //기존 건물 위치 반환
            Vector3 pos;
            pos = totalMode[0].transform.Find(key).position;

            //건물 설치
            path = "Prefabs/Buildings/" + key;
            GameObject a = (GameObject)Instantiate(Resources.Load(path));
            a.name = key;
            a.gameObject.tag = "portfolioMode";
            myPortfolio.pfBuildings.Add(a);

            a.transform.position = new Vector3(pos.x, pos.y, pos.z);

            //scale 옵션이 켜져있으면
            if (scale.isOn)
            {
                Debug.Log("scale option ON !");
                /*
                //수익률에 따른 크기 스케일링 (1 ~ 5단계)
                double ratio = (myPortfolio.updateCost(key) - myPortfolio.updateInvest(key)) / myPortfolio.updateInvest(key) * 100; // (평가금액 - 평균 매수 금액) / 평균 매수 금액 * 100
                double scale = 1f;
                if (ratio <= -10) { scale = 0.5f; }
                else if (ratio < 0) { scale = 0.75f; }
                else if (ratio >= 0 && ratio < 10) { scale = 1f; }
                else if (ratio >= 10 && ratio <= 30) { scale = 1.25f; }
                else { scale = 1.5f; }
            
                a.transform.localScale = new Vector3(scale * a.transform.localScale.x, scale * a.transform.localScale.y, scale * a.transform.localScale.z);
                */
            }
            else
            {
                Debug.Log("scale option OFF !");
            }
        }
    }
    void toggleControl()
    {
        //weather 옵션
        if (weather.isOn) {
            tog1.SetActive(true);
        }
        else
        {
            tog1.SetActive(false);
        }

        //market time 옵션
        if (mktime.isOn)
        {
            tog2.SetActive(true);
        }
        else
        {
            tog2.SetActive(false);
        }
        //scale 옵션
        if (scale.isOn)
        {
            tog3.SetActive(true);
            settingPortfolio();
        }
        else
        {
            tog3.SetActive(false);
            settingPortfolio();
        }
    }
}
