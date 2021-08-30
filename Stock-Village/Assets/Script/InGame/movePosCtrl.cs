using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

public class movePosCtrl : MonoBehaviour
{
    public GameObject movePosPage; //건물 위치 변경 페이지
    private RaycastHit hit; //마우스에 클릭된 객체
    public Text SectorName;

    public Button[] posBtn; //버튼 배열
    public Text[] btnText; //버튼 안의 텍스트 배열

    Dictionary<string, List<string>> sectorCtrl
        = new Dictionary<string, List<string>>() { { "IT", new List<string> { "MSFT", "ORCL", "AAPL", "IBM", "GOOGL", "FB", "DIS", "NFLX" } },
                                                   { "Industrial", new List<string> { "HON", "UNP", "LMT" } },
                                                   { "Consumer", new List<string> { "AMZN", "TSLA", "SBUX", "NKE", "WMT", "COST", "KO", "PEP" } },
                                                   { "Health Care", new List<string> { "JNJ", "PFE", "UNH" } },
                                                   { "Real Estate", new List<string> { "AMT", "EQIX", "PLD", "O" } },
                                                   { "Financial", new List<string> { "V", "PYPL", "BAC", "C" } } };

    void FixedUpdate()
    {
        rigntClick();
    }

    void rigntClick()
    {
        if (GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp) { return; } //열려있는 팝업창이 있다면 실행하지 않음

        //우클릭 시
        if (Input.GetMouseButtonDown(1))
        {
            string symbol = "";
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "stock" || hit.collider.gameObject.tag == "portfolioMode")//건물 오브젝트를 클릭한 경우
                {
                    movePageSet();
                }
            }
        }

    }

    void movePageSet()
    {
        List<string> secList; //현재 섹터 종목 리스트
        string clicekd = hit.collider.gameObject.name; //클릭된 건물 오브젝트 이름
        string curSector = SectorName.text; //현재 섹터명

        if (!sectorCtrl.ContainsKey(curSector)) { return; } //Full Shot 모드일 경우, 실행 X
        else
        {
            movePosPage.SetActive(true);
            GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp = true;

            secList = sectorCtrl[SectorName.text]; //현재 섹터 종목 리스트

            //버튼 초기 세팅
            for (int i = 0; i < 8; i++)
            {
                btnText[i].text = "-"; //텍스트 초기화
                posBtn[i].interactable = false; //버튼 비활성화
            }

            //섹터별 버튼 세팅
            for (int i = 0; i < secList.Count; i++)
            {
                btnText[i].text = secList[i];
                if (!btnText[i].text.Equals(clicekd)) { posBtn[i].interactable = true; } //현재 빌딩이 아닐 경우만 버튼 활성화
            }
        }
    }

    public void moveBtnClick()
    {
        Vector3 targetPos, beforePos;
        int index = -1;

        //클릭한 버튼 찾기
        string clicked = EventSystem.current.currentSelectedGameObject.name;
        for (int i = 0; i < 8; i++)
        {
            if (clicked.Equals("pos" + i))
            {
                index = i;
                break;
            }
        }

        //건물 위치 반환
        targetPos = hit.collider.gameObject.transform.position;
        beforePos = GameObject.Find(btnText[index].text).gameObject.transform.position;

        //건물 이동
        GameObject.Find(btnText[index].text).gameObject.transform.position = new Vector3(targetPos.x, beforePos.y, targetPos.z);
        hit.collider.gameObject.transform.position = new Vector3(beforePos.x, targetPos.y, beforePos.z);

        //페이지 닫기
        movePosPage.SetActive(false);
        GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp = false;
    }

    public void exitBtnClick()
    {
        movePosPage.SetActive(false);
        GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp = false;
    }
}
