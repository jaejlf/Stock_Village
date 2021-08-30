using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InGame : MonoBehaviour
{
    public StockList stockList;
    public portfolio myPortfolio;

    public Toggle scale; //scaling �ɼ� üũ

    public GameObject[] totalMode; //totalMode �ǹ� ������Ʈ
    public GameObject[] portfolioMode; //portfolioMode �ǹ� ������Ʈ

    void Start()
    {
        myPortfolio.renew = false; //��Ʈ������ ���� �÷��� false
        totalBtnClick();
    }
    void Update()
    {
        //��Ʈ�������� ���ŵ� ���
        if (myPortfolio.renew)
        {
            settingPortfolio();
            myPortfolio.renew = false;
        }
    }
    //totalMode
    public void totalBtnClick()
    {
        //portfolioMode �±� ��Ȱ��ȭ
        foreach (GameObject tmp in myPortfolio.pfBuildings)
        {
            tmp.SetActive(false);
        }
        //totalMode �±� Ȱ��ȭ
        for (int i = 0; i < totalMode.Length; i++)
        {
            totalMode[i].SetActive(true);
        }
    }

    //portfolioMode
    public void portfolioBtnClick()
    {
        //totalMode �±� ��Ȱ��ȭ
        for (int i = 0; i < totalMode.Length; i++)
        {
            totalMode[i].SetActive(false);
        }

        //portfolioMode �±� Ȱ��ȭ
        for (int i = 0; i < portfolioMode.Length; i++)
        {
            portfolioMode[i].SetActive(true);
        }
        settingPortfolio(); //��Ʈ������ �ǹ� ��ġ
    }

    //��Ʈ������ �ǹ� ��ġ
    public void settingPortfolio()
    {
        string path = "";

        //��Ʈ������ �ǹ� ��ü ����
        foreach (GameObject tmp in myPortfolio.pfBuildings) { Destroy(tmp); }
        myPortfolio.pfBuildings.Clear();

        //�ǹ� ��ġ
        foreach (var key in myPortfolio.stockInfo.Keys.ToList())
        {
            if (myPortfolio.stockInfo[key].shares == 0) { continue; } //���������� 0�� ��� ����

            //���� �ǹ� ��ġ ��ȯ
            Vector3 pos;
            pos = totalMode[0].transform.Find(key).position;

            //�ǹ� ��ġ
            path = "Buildings/" + key;
            GameObject a = (GameObject)Instantiate(Resources.Load(path));
            a.name = key;
            a.gameObject.tag = "portfolioMode";
            myPortfolio.pfBuildings.Add(a);

            a.transform.position = new Vector3(pos.x, pos.y, pos.z);

            //scale �ɼ��� ����������
            if (scale.isOn)
            {
                Debug.Log("scale option is ON !");
                
                //���ͷ��� ���� ũ�� �����ϸ� (1 ~ 3�ܰ�)
                double ratio = (myPortfolio.updateStPrice(key) - myPortfolio.updateStInvest(key)) / myPortfolio.updateStInvest(key) * 100; // (�򰡱ݾ� - ��� �ż� �ݾ�) / ��� �ż� �ݾ� * 100
                double scale = 1f;
                if (ratio < 0) { scale = 0.7f; }
                else if (ratio >= 0 && ratio < 10) { scale = 1f; }
                else if (ratio >= 10 && ratio <= 30) { scale = 1.3f; }
            
                a.transform.localScale = new Vector3((float)(scale * a.transform.localScale.x), (float)(scale * a.transform.localScale.y), (float)(scale * a.transform.localScale.z));   
            }
        }
    }
}