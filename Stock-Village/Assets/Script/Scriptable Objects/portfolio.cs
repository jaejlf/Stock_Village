using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "portfolio")]

public class portfolio : ScriptableObject
{
    public List<GameObject> pfBuildings; // 포트폴리오 건물 오브젝트 리스트
    public StockList stockList; //주식 정보 저장
    public double Cash = 0; //현금
    public double totalGain = 0; //전체 수익금
    public double totalStPrice = 0; //전체 평가금액
    public double totalStInvest = 0; //전체 투자금
    public bool renew = false; //포트폴리오 정보 변경 확인 변수

    public Dictionary<string, StockStat> stockInfo = new Dictionary<string, StockStat>(); //종목별 데이터
    public Dictionary<string, List<Trade>> tradeList = new Dictionary<string, List<Trade>>(); //종목별 거래 정보

    public struct StockStat //종목별 데이터(보유량, 평균 매수 금액)
    {
        public int shares;
        public double avgCostPerShare;
    };

    public class Trade //API에서 불러온 데이터 return 하기 위한 클래스
    {
        public string tradeDate; //매매 날짜
        public int shares; //매매 수량
        public double costPerShare; //평균 매매 금액
        public int state; //buy: 0, sell: 1
        public Trade(string tradeDate, int shares, double costPerShare, int state)
        {
            this.tradeDate = tradeDate;
            this.shares = shares;
            this.costPerShare = costPerShare;
            this.state = state;
        }
    }
    public void BUY(string symbol, double costPerShare, int shares, string tradeDate, int state)
    {
        //기존 거래 정보에 존재하는 종목인 경우
        if (tradeList.ContainsKey(symbol))
        {
            //입력한 자금이 부족하지 않은 경우
            if (Cash >= shares * costPerShare)
            {
                tradeList[symbol].Add(new Trade(tradeDate, shares, costPerShare, state)); //일일 거래 데이터 추가

                StockStat tmp = stockInfo[symbol];
                tmp.avgCostPerShare = (shares * costPerShare + tmp.avgCostPerShare * tmp.shares) / (tmp.shares + shares);
                tmp.shares += shares;
                stockInfo[symbol] = tmp;
                Cash -= shares * costPerShare;
                renew = true;
            }
            else
            {
                Debug.Log("매수할 수 있는 자금이 부족합니다.");
            }
        }
        //새로운 종목인 경우
        else
        {
            //입력한 자금이 부족하지 않은 경우
            if (Cash >= shares * costPerShare)
            {
                tradeList.Add(symbol, new List<Trade>());
                tradeList[symbol].Add(new Trade(tradeDate, shares, costPerShare, state)); //일일 거래 데이터 추가

                stockInfo.Add(symbol, new StockStat());
                StockStat tmp = stockInfo[symbol];
                tmp.shares = shares;
                tmp.avgCostPerShare = costPerShare;
                stockInfo[symbol] = tmp;
                Cash -= shares * costPerShare;
                renew = true;
            }
            else
            {
                Debug.Log("매수할 수 있는 자금이 부족합니다.");
            }
        }
    }
    public void SELL(string symbol, double costPerShare, int shares, string tradeDate, int state)
    {
        //기존 거래 정보에 존재하는 종목인 경우
        if (tradeList.ContainsKey(symbol))
        {
            if (stockInfo[symbol].shares < shares)
            {
                Debug.Log("보유종목보다 많이 매도할 수 없습니다.");
            }
            else
            {
                tradeList[symbol].Add(new Trade(tradeDate, shares, costPerShare, state)); //일일 거래 데이터 추가

                StockStat tmp = stockInfo[symbol];
                tmp.avgCostPerShare = (tmp.avgCostPerShare * tmp.shares - shares * costPerShare) / (tmp.shares - shares);
                tmp.shares -= shares;
                stockInfo[symbol] = tmp;
                Cash += shares * costPerShare;
                renew = true;
            }
        }
        else
        {
            Debug.Log("보유하지 않은 종목을 매도할 수 없습니다");
        }
    }

    //종목별 평가 금액 (시장가격 X 수량)
    public double updateStPrice(string symbol)
    {
        return stockList.apiInfo[symbol].api_marketprice * stockInfo[symbol].shares;
    }

    //종목별 투자금 (평균 매수 금액 * 보유 수량)
    public double updateStInvest(string symbol)
    {
        return stockInfo[symbol].avgCostPerShare * stockInfo[symbol].shares;
    }

    //전체 평가 금액
    public double update_Total_Stprice()
    {
        totalStPrice = 0;

        foreach (var key in stockInfo.Keys.ToList())
        {
            if (stockInfo[key].shares == 0) { continue; } //보유수량이 0인 경우 제외
            totalStPrice += updateStPrice(key);
        }

        return totalStPrice;
    }
    
    //전체 투자금
    public double update_Total_StInvest()
    {
        totalStInvest = 0;

        //전체 투자금
        foreach (var key in stockInfo.Keys.ToList())
        {
            totalStInvest += updateStInvest(key);
        }

        return totalStInvest;
    }
}