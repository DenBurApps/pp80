using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillMinutes : MonoBehaviour
{
    private ScrollMechanic scroll;

    private void Awake()
    {
        scroll = GetComponent<ScrollMechanic>();

        scroll.testData = new string[61];
        for (int i = 0; i <= 60; i++) 
        {
            string str = "";
            if (i < 10)
                str = "0";
            str += i.ToString();

            scroll.testData[i] = str;
        }

        scroll.initTest = true;
    }
}
