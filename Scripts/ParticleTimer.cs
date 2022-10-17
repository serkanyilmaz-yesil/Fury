using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ParticleTimer : MonoBehaviour
{
    public static ParticleTimer timeParticle;

    [HideInInspector]
    public float timer;

    public int maxTime;
    public TextMeshProUGUI timeText;
    public Image fillBar;

    public GameObject buttonEffect;

    private void Awake()
    {
        timeParticle = this;
    }
    void Update()
    {
        if (timer < maxTime)
        {
            timer += Time.deltaTime;
            DisplayTime(maxTime - (int)timer);
            buttonEffect.SetActive(false);
        }
        else
        {
            buttonEffect.SetActive(true);
            timeText.text = "0";
        }




        fillBar.fillAmount = timer / maxTime;
    }



    void DisplayTime(int timeToDisplay)
    {
        timeText.text = "" + timeToDisplay;
    }

}



