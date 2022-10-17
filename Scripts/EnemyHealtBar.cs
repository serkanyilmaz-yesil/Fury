using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealtBar : MonoBehaviour
{

    public static EnemyHealtBar enemyBar;

    public float maximumHealt;
    public float currentHealt;
    public Image healtimage;



    public bool die;
    public GameObject destroyEffect;

    private void Awake()
    {
        enemyBar = this;


        currentHealt = maximumHealt;
        die = false;
    }



    void Update()

    {
        HealtUi();
        ColorChange();

        if (currentHealt <=0 && !die)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);

            die = true;
            Destroy(gameObject);

        }

        if (Boss.boss.die)
        {
            Destroy(gameObject);

        }

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

}

