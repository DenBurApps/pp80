using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCanvas : MonoBehaviour
{
    public static Canvas canvas;
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }
}
