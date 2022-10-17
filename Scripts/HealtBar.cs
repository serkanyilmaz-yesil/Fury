using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealtBar : MonoBehaviour
{

    public static HealtBar Pb;

    public float maximumHealt;
    public float currentHealt;
    public Image healtimage;
    public TextMeshProUGUI healtText;

    public float boxingDamage;

    public Transform cam;


    private void Awake()
    {
        Pb = this;

        cam = Camera.main.transform;
        if (PlayerPrefs.HasKey("currentHealt"))
        {
            currentHealt = PlayerPrefs.GetFloat("currentHealt");
        }


    }


    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }

    void Update()

    {
        HealtUi();
        ColorChange();

        if (EnemySpawner.spawner.amountBoxingRobot > 0)
        {
            if (BoxingRobot.boxing.time >= 3f && !ShieldTimer.sh.shieldActive)
            {
                currentHealt -= boxingDamage;
                BoxingRobot.boxing.time = 0;
            }

        }

        if (currentHealt <= 0)
        {
            CharacterMove.ctrl.die = true;
            currentHealt = 0;
        }

        DisplayTime((int)currentHealt);

    }



    private void HealtUi()
    {
        healtimage.fillAmount = currentHealt / maximumHealt;
    }
    void ColorChange()
    {
        Color color = Color.Lerp(Color.red, Color.green, (currentHealt / maximumHealt));
        healtimage.color = color;
    }


    void DisplayTime(int timeToDisplay)
    {
        healtText.text = "" + timeToDisplay;
    }

}

