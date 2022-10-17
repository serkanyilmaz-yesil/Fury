using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UltiTimer : MonoBehaviour
{
    private float timer;

    public int maxTime;
    public TextMeshProUGUI timeText;
    public Image fillBar;
    //public CameraController shake;


    public GameObject ulti, lightt;
    private bool ultiEffect;
    public Transform player;

    public GameObject buttonEffect;


    private void Start()
    {
        ultiEffect = false;
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

    public void ButtonTrue()
    {
        if (timer >= maxTime)
        {
            lightt.GetComponent<Light>().intensity = 0.6f;

            if (!ultiEffect)
            {
                Instantiate(ulti, player.transform.position, Quaternion.identity);

                ultiEffect = true;

            }
            timer = 0;
            //StartCoroutine(shake.Shake(.3f, .9f));

        }
        if (ultiEffect)
        {
            ultiEffect = false;
        }

        Invoke("LightControl", 2);
    }

    void LightControl()
    {
        lightt.GetComponent<Light>().intensity = .8f;

    }


}
