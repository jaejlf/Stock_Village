using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class moveBtnCtrl : MonoBehaviour
{
    public Camera[] sectorCamera; //���ͺ� ī�޶� ����Ʈ
    public Text SectorName; //ȭ�鿡 ��� ���͸�
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
        if (GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp) { return; } //�����ִ� �˾�â�� �ִٸ� �������� ����
        
        //Ű���带 ���� ī�޶� �̵�
        if (Input.GetKey(KeyCode.LeftArrow)) { Camera.main.transform.Translate(-0.2f, 0.0f, 0.0f); } // �������� �̵�
        if (Input.GetKey(KeyCode.RightArrow)) { Camera.main.transform.Translate(0.2f, 0.0f, 0.0f); } // ���������� �̵�
        if (Input.GetKey(KeyCode.UpArrow)) { Camera.main.transform.Translate(0.0f, 0.0f, 0.2f); } // ������ �̵� (zoom-in)
        if (Input.GetKey(KeyCode.DownArrow)) { Camera.main.transform.Translate(0.0f, 0.0f, -0.2f); } // �ڷ� �̵� (zoom-out)
    }
    public void leftBtnClick()
    {
        //���� ī�޶� �ε���
        SectorIndex = (SectorIndex - 1);
        if (SectorIndex <= 0) { SectorIndex = 6; }
        SectorName.text = SectorNames[SectorIndex];
    }
    public void rightBtnClick()
    {
        //���� ī�޶� �ε���
        SectorIndex = (SectorIndex + 1);
        if (SectorIndex > 6) { SectorIndex = 1; }
        SectorName.text = SectorNames[SectorIndex];
    }
    public void fullShotBtnClick()
    {
        //���� ī�޶� �ε���(Ǯ�� ī�޶�)
        SectorIndex = 0;
        SectorName.text = SectorNames[SectorIndex];

    }
}