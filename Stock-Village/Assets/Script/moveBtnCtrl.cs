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
    }
    void UpdateKeyboard()
    {
        if (GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp) { return; } //열려있는 팝업창이 있다면 실행하지 않음
        
        //키보드를 통해 카메라 이동
        if (Input.GetKey(KeyCode.LeftArrow)) { Camera.main.transform.Translate(-0.2f, 0.0f, 0.0f); } // 왼쪽으로 이동
        if (Input.GetKey(KeyCode.RightArrow)) { Camera.main.transform.Translate(0.2f, 0.0f, 0.0f); } // 오른쪽으로 이동
        if (Input.GetKey(KeyCode.UpArrow)) { Camera.main.transform.Translate(0.0f, 0.0f, 0.2f); } // 앞으로 이동 (zoom-in)
        if (Input.GetKey(KeyCode.DownArrow)) { Camera.main.transform.Translate(0.0f, 0.0f, -0.2f); } // 뒤로 이동 (zoom-out)
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
}
