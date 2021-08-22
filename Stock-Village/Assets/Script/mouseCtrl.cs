using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseCtrl : MonoBehaviour
{
    private Camera cam; //게임 화면 카메라
    private GameObject mouseOn; //이벤트 조건별 발생하는 이펙트UI
    private Text symbol;
    private RaycastHit hit;

    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        mouseOn = GameObject.Find("Canvas").transform.Find("mouseOn").gameObject;
        symbol = mouseOn.transform.GetChild(0).GetComponent<Text>();
        mouseOn.SetActive(false); //말풍선 off
    }

    //해당 오브젝트 위에 마우스가 올라가면 말풍선 on
    private void OnMouseEnter()
    {
        if (GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp) { return; }

        var ray = cam.ScreenPointToRay(Input.mousePosition);
        var wantedPos = cam.WorldToScreenPoint(transform.position);

        Physics.Raycast(ray, out hit); //마우스 커서가 올라간 건물 오브젝트

        symbol.text = hit.collider.gameObject.name; //말풍선 텍스트 종목 이름으로 변경
        mouseOn.transform.position = new Vector3(wantedPos.x + 50f, wantedPos.y + 100f, wantedPos.z); //말풍선 위치 변경
        mouseOn.SetActive(true);

    }

    //해당 오브젝트 위에 마우스가 올라가면 말풍선 off
    private void OnMouseExit()
    {
        mouseOn.SetActive(false);
    }
}
