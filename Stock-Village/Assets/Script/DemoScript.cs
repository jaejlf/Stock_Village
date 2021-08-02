using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DigitalRuby.RainMaker
{
    public class DemoScript : MonoBehaviour
    {
        public portfolio myPortfolio;
        public StockList stockList;

        public RainScript RainScript; //날씨 제어를 위한 오브젝트
        public GameObject Sun; //낮, 밤 제어를 위한 오브젝트

        private void Start()
        {
            RainScript.RainIntensity = 0f; //날씨 off
            RainScript.EnableWind = true;
        }

        public void weatherClick(int onf)
        {
            //on
            if (onf == 0) {
                RainScript.RainIntensity = 3.0f;
            }
            //off
            else {
                RainScript.RainIntensity = 0.0f;
            }
        }

        public void mktimeClick(int onf)
        {
            //on
            if (onf == 0) {
                Sun.GetComponent<Light>().intensity = 1f;
            }
            //off
            else {
                Sun.GetComponent<Light>().intensity = 0f;
            }
        }
    }
}