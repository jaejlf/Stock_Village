using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class moveBtnCtrl : MonoBehaviour
{
    public Camera[] sectorCamera; //섹터별 카메라 리스트
    public Text SectorName; //화면에 띄울 섹터명

    string[] SectorNames = { "Full Shot", "IT", "Consumer", "Financial", "Health Care", "Industrial", "Real Estate" };
    private int SectorIndex;

    void Start()
    {
        SectorIndex = 0;
    }
    void FixedUpdate()
    {
        UpdateKeyboard();
        
        //풀샷 카메라가 아닐 경우, 카메라 이동에 따른 섹터명 업데이트
        if(SectorIndex != 0)
        {
           UpdateSectorName();
        }
    }
    void UpdateKeyboard()
    {
        if (GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp) { return; } //열려있는 팝업창이 있다면 실행하지 않음

        if (Input.GetKey(KeyCode.LeftArrow)) { Camera.main.transform.Translate(-0.3f, 0.0f, 0.0f); } // 왼쪽으로 이동
        if (Input.GetKey(KeyCode.RightArrow)) { Camera.main.transform.Translate(0.3f, 0.0f, 0.0f); } // 오른쪽으로 이동
        if (Input.GetKey(KeyCode.UpArrow)) { Camera.main.transform.Translate(0.0f, 0.2f, 0.3f); } // 앞으로 이동
        if (Input.GetKey(KeyCode.DownArrow)) { Camera.main.transform.Translate(0.0f, -0.2f, -0.3f); } // 뒤로 이동

    }
    public void leftBtnClick()
    {
        //다음 카메라 인덱스
        SectorIndex = (SectorIndex - 1);
        if (SectorIndex <= 0) { SectorIndex = 6; }
        SectorName.text = SectorNames[SectorIndex];

        //이동할 카메라 position, rotation 반환
        Vector3 pos = sectorCamera[SectorIndex].transform.position;
        Quaternion rot = sectorCamera[SectorIndex].transform.rotation;

        //카메라 이동
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, pos, 1);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, rot, 1);
    }
    public void rightBtnClick()
    {
        //다음 카메라 인덱스
        SectorIndex = (SectorIndex + 1);
        if (SectorIndex > 6) { SectorIndex = 1; }
        SectorName.text = SectorNames[SectorIndex];

        //이동할 카메라 position, rotation 반환
        Vector3 pos = sectorCamera[SectorIndex].transform.position;
        Quaternion rot = sectorCamera[SectorIndex].transform.rotation;

        //카메라 이동
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, pos, 1);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, rot, 1);
    }
    public void fullShotBtnClick()
    {
        //다음 카메라 인덱스(풀샷 카메라)
        SectorIndex = 0;
        SectorName.text = SectorNames[SectorIndex];

        //이동할 카메라 position, rotation 반환
        Vector3 pos = sectorCamera[SectorIndex].transform.position;
        Quaternion rot = sectorCamera[SectorIndex].transform.rotation;

        //카메라 이동
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, pos, 1);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, rot, 1);
    }

    //카메라 이동에 따른 섹터명 업데이트
    void UpdateSectorName()
    {
        float minDist = 100000000000;
        float dist = 0f;
        int tmpIdx = 1;

        for (int i = 1; i <= 6; i++)
        {
            dist = Vector3.Distance(Camera.main.transform.position, sectorCamera[i].transform.position);
            if (minDist > dist)
            {
                tmpIdx = i;
                minDist = dist;
            }
        }
        SectorIndex = tmpIdx;
        SectorName.text = SectorNames[SectorIndex]; //섹터명 업데이트
    }
}