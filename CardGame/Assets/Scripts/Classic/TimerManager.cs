using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static float timeLeft = 0;
    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            this.GetComponent<TextMeshProUGUI>().text = Math.Round(timeLeft).ToString();
        }
    }
}
