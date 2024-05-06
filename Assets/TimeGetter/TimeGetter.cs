using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGetter : MonoBehaviour
{
    [SerializeField] private ScrollMechanic hours;
    [SerializeField] private ScrollMechanic minutes;
    [SerializeField] private ScrollMechanic time;

    public string GetTime()
    {
        return hours.GetCurrentValueStr() + ":" + minutes.GetCurrentValueStr() + " " + time.GetCurrentValueStr();
    }
}
