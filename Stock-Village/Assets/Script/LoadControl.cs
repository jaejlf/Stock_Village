using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System;

public class LoadControl : MonoBehaviour
{
    IEnumerator coroutine1;

    int count = 0; //불러올 종목 정보 개수
    public Slider LoadSlider; //로딩바
    public StockList stockList; //주식 정보 저장 객체
    public API api;

    //주식 종목 코드 배열
    List<string> symbolList = new List<string>() { "MSFT" }; //"GOOGL", "SBUX", "PYPL" };
    /*List<string> symbolList = new List<string>() { "MSFT", "ORCL", "AAPL", "IBM", "GOOGL", "FB", "NFLX", "DIS", //IT
                                                    "AMZN", "TSLA", "SBUX", "NKE","WMT", "COST", "KO", "PEP", //Consumer
                                                    "V", "PYPL", "BAC", "C", //Financial
                                                    "JNJ", "PFE", "UNH", //Healthcare
                                                    "HON", "UNP", "LMT", //Industrial
                                                    "AMT", "EQIX", "PLD", "O"}; //Real Estate    */

    void Start()
    {
        LoadSlider.value = 0; //슬라이더 바 상태 초기화

        apiCall();
        coroutine1 = gameLoading();
        StartCoroutine(coroutine1);
    }

    IEnumerator gameLoading()
    {
        AsyncOperation ingame = SceneManager.LoadSceneAsync("InGame");
        ingame.allowSceneActivation = false;

        //인게임 씬 로드가 완료되지 않았으면
        while (!ingame.isDone)
        {
            yield return null;

            //로딩바 50% 채우기
            float progress1 = Mathf.Clamp01(ingame.progress / .9f) * 5f; 
            LoadSlider.value = progress1;

            //로딩바 50~100% 
            if (progress1 >= 5)
            {
                //주식 정보 로드 진행 비율만큼 채우기
                while (count < symbolList.Count)
                {
                    float progress2 = Mathf.Clamp01(count / symbolList.Count) * 5f;
                    LoadSlider.value = 5f + progress2;
                    yield return null;
                }
                ingame.allowSceneActivation = true; //InGame 씬 로드

                yield break;
            }
        }
    }

    async Task apiCall()
    {
        try
        {
            foreach (string symbol in symbolList)
            {
                await BeginNetwork(symbol);
            }
        }
        catch (Exception)
        {
            Debug.Log("API call error !");
        }
    }

    async Task BeginNetwork(string symbol)
    {

        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v2/get-summary?symbol=" + symbol + "&region=US"),
            Headers =
    {
        { "x-rapidapi-key", api.key },
        { "x-rapidapi-host", "apidojo-yahoo-finance-v1.p.rapidapi.com" },
    },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            JObject obj = JObject.Parse(body);

            //current price
            string tmp_cp = (string)obj["financialData"]["currentPrice"]["raw"];
            double.TryParse(tmp_cp, out double cp);

            //dividened date
            string dd = (string)obj["calendarEvents"]["dividendDate"]["fmt"];

            //dividened rate
            string tmp_dr = (string)obj["summaryDetail"]["dividendRate"]["raw"];
            double.TryParse(tmp_dr, out double dr);

            //sector
            string sec = (string)obj["summaryProfile"]["sector"];

            //market cap
            string tmp_mc = (string)obj["price"]["marketCap"]["raw"];
            double.TryParse(tmp_mc, out double mc);

            //PER
            string tmp_per = (string)obj["summaryDetail"]["forwardPE"]["raw"];
            double.TryParse(tmp_per, out double per);

            //52 week change
            string tmp_wc = (string)obj["defaultKeyStatistics"]["52WeekChange"]["raw"];
            double.TryParse(tmp_wc, out double wc);

            //previous close
            string tmp_pc = (string)obj["price"]["regularMarketPreviousClose"]["raw"];
            double.TryParse(tmp_pc, out double pc);

            count++;

            //apiInfo 정보 업데이트
            if (stockList.apiInfo.ContainsKey(symbol))
            {
                stockList.apiInfo[symbol].api_marketprice = cp;
                stockList.apiInfo[symbol].api_divDate = dd;
                stockList.apiInfo[symbol].api_divRate = dr;
                stockList.apiInfo[symbol].api_sector = sec;
                stockList.apiInfo[symbol].api_marketcap = mc;
                stockList.apiInfo[symbol].api_per = per;
                stockList.apiInfo[symbol].api_52week = wc;
                stockList.apiInfo[symbol].api_preclose = pc;
            }
            else
            {
                stockList.add(symbol, cp, dd, dr, sec, mc, per, wc, pc);
            }
        };
    }
}