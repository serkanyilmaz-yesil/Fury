using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldTimer : MonoBehaviour
{
    public static ShieldTimer sh;
    private float timer;

    public int maxTime;
    public TextMeshProUGUI timeText;
    public Image fillBar;

    public GameObject shield;
    public Transform player;

    public GameObject buttonEffect;
    public bool shieldActive = false;
    public float shieldActiveTime;

    private void Awake()
    {
        sh = this;
        if (PlayerPrefs.HasKey("shieldActiveTime"))
        {
            shieldActiveTime = PlayerPrefs.GetFloat("shieldActiveTime");
        }

    }

    private void Start()
    {
        shield.SetActive(false);
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

        if (shieldActive)
        {
            Invoke("ShieldPassive", shieldActiveTime);

        }


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
            shieldActive = true;
            shield.SetActive(true);


            timer = 0;
        }
    }

    void ShieldPassive()
    {
        shield.SetActive(false);
        shieldActive = false;
    }

}
