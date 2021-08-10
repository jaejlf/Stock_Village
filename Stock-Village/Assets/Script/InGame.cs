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
    public Toggle scale; //scaling 옵션 체크

    public GameObject[] totalMode; //totalMode 건물 오브젝트
    public GameObject[] portfolioMode; //portfolioMode 건물 오브젝트

    void Start()
    {
        myPortfolio.renew = false; //포트폴리오 갱신 플래그 false
        totalBtnClick();
    }
    void Update()
    {
        //포트폴리오가 갱신된 경우
        if (myPortfolio.renew)
        {
            settingPortfolio();
            myPortfolio.renew = false;
        }
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
    }

    //portfolioMode
    public void portfolioBtnClick()
    {
        //totalMode 태그 비활성화
        for (int i = 0; i < totalMode.Length; i++)
        {
            totalMode[i].SetActive(false);
        }

        //portfolioMode 태그 활성화
        for (int i = 0; i < portfolioMode.Length; i++)
        {
            portfolioMode[i].SetActive(true);
        }
        settingPortfolio(); //포트폴리오 건물 설치
    }

    //포트폴리오 건물 설치
    public void settingPortfolio()
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
            path = "Buildings/" + key;
            GameObject a = (GameObject)Instantiate(Resources.Load(path));
            a.name = key;
            a.gameObject.tag = "portfolioMode";
            myPortfolio.pfBuildings.Add(a);

            a.transform.position = new Vector3(pos.x, pos.y, pos.z);

            //scale 옵션이 켜져있으면
            if (scale.isOn)
            {
                Debug.Log("scale option is ON !");
                
                //수익률에 따른 크기 스케일링 (1 ~ 5단계)
                double ratio = (myPortfolio.updateStPrice(key) - myPortfolio.updateStInvest(key)) / myPortfolio.updateStInvest(key) * 100; // (평가금액 - 평균 매수 금액) / 평균 매수 금액 * 100
                double scale = 1f;
                if (ratio <= -10) { scale = 0.5f; }
                else if (ratio < 0) { scale = 0.75f; }
                else if (ratio >= 0 && ratio < 10) { scale = 1f; }
                else if (ratio >= 10 && ratio <= 30) { scale = 1.25f; }
                else { scale = 1.5f; }
            
                a.transform.localScale = new Vector3((float)(scale * a.transform.localScale.x), (float)(scale * a.transform.localScale.y), (float)(scale * a.transform.localScale.z));
                
            }
        }
    }
}