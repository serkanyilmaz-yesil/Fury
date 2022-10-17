using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CounterTime : MonoBehaviour
{
    private float timer;

    public int maxTime;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public Image fillBar;

    private void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timer < maxTime)
        {
            timer += Time.deltaTime;
            DisplayTime(maxTime - (int)timer);
        }
        else
            timeText.text = "0";




        fillBar.fillAmount = timer / maxTime;
    }



    void DisplayTime(int timeToDisplay)
    {
        timeText.text = "" + timeToDisplay;
    }

    public void ButtonTrue()
    {
        if (timer >= maxTime)
        {
            timer = 0;
        }
    }

   
}
