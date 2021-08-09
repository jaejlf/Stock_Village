using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class clickBuilding : MonoBehaviour
{
    public portfolio myPortfolio; //포트폴리오 정보 저장자료
    public StockList stockList;//주식 api정보를 이용하기 위한 stockList
    private RaycastHit hit; //마우스에 클릭된 객체

    //종목 정보 UI
    public GameObject stockInfo; //종목 정보 UI 페이지
    public Text _symbol; //종목 코드
    public Text _mkPrice; //현재 시가
    public Text _sector; //관련 업종별 분류
    public Text _mkCap; //시가총액
    public Text _per; //PER
    public Text _52week; //시가 성장 변화(52 week change)
    public Text _volume; //거래량
    public Text _divDate; //배당일

    //포트폴리오 정보 UI
    public GameObject pfInfo; //포트폴리오 정보 UI 페이지
    public Text _shares; //보유 수량
    public Text _valueCost; //평가금액
    public Text _buy; //매수 금액
    public Text _gain; //수익금/률
    public Text _dividend; //배당금
    public Text _tradeDate; //최근 거래일

    private void FixedUpdate()
    {
        buildingClick();
    }
    void buildingClick()
    {
        Debug.Log("Click");
        if (GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp) { return; } //열려있는 팝업창이 있다면 실행하지 않음

        //클릭한 객체 이름 출력
        if (Input.GetMouseButtonDown(0))
        {
            string symbol = "";
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("stock"))//건물 오브젝트를 클릭한 경우
                {
                    stockInfo.SetActive(true);
                    GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp = true;
                    symbol = hit.collider.gameObject.name;
                    settingStockInfo(symbol);
                }

            }
        }
    }
    void settingStockInfo(string symbol)
    {
        if (!stockList.apiInfo.ContainsKey(symbol)) { return; } //정보가 없으면 return

        _symbol.text = "[ " + symbol + " ]";
        _mkPrice.text = stockList.apiInfo[symbol].api_marketprice.ToString("F3") + "$";
        _sector.text = stockList.apiInfo[symbol].api_sector;
        _per.text = stockList.apiInfo[symbol].api_per.ToString("F3");
        _52week.text = stockList.apiInfo[symbol].api_52week.ToString("F3");
        _volume.text = stockList.apiInfo[symbol].api_volume.ToString();
        _divDate.text = GameObject.Find("InGameControl").GetComponent<dividendCtrl>().divDate(symbol);

        //_mkCap
        double tmp = stockList.apiInfo[symbol].api_marketcap;
        string marketcap = "";
        if (tmp >= 1000000000000) { marketcap = (tmp / 1000000000000).ToString("F3") + " T"; } //trillion
        else if (tmp >= 100000000000) { marketcap = (tmp / 100000000000).ToString("F3") + " B"; } //billion
        else { marketcap = tmp + ""; } //T, B 외의 단위 있다면 추가하기
        _mkCap.text = marketcap;
    }
    
    public void closeBtnClick()
    {
        GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp = false;
        stockInfo.SetActive(false);
    }
    public void pfInfoBtnClick()
    {
        string symbol = _symbol.ToString();
        double tmp_gain;
        double tmp_gainRatio;

        stockInfo.SetActive(false);
        pfInfo.SetActive(true);

        _shares.text = myPortfolio.stockInfo[symbol].shares.ToString();
        _valueCost.text = myPortfolio.updateStPrice(symbol).ToString();
        _buy.text = myPortfolio.updateStInvest(symbol).ToString();

        tmp_gain = myPortfolio.updateStPrice(symbol) - myPortfolio.updateStInvest(symbol); //평가금액 - 평균 매수 금액
        tmp_gainRatio = (tmp_gain / myPortfolio.updateStInvest(symbol)) * 100; // (평가금액 - 평균 매수 금액) / 평균 매수 금액 * 100
        _gain.text = tmp_gain + " / " + tmp_gainRatio;

        _dividend.text = GameObject.Find("InGameControl").GetComponent<dividendCtrl>().dividend(symbol).ToString();
        //_tradeDate
    }
    public void backBtnClick()
    {
        pfInfo.SetActive(false);
        stockInfo.SetActive(true);
    }

}
