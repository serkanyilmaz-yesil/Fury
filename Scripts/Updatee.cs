using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Updatee : MonoBehaviour
{
    public static Updatee upd;

    Vector3 startScale;
    Vector3 endScale;
    public GameObject updatePanel, notEnoughMoney,buttons;
    public bool mainPanel;
    public float healthCoin, damageCoin, rocketCoin, shieldCoin;
    public float difference;
    public float percent;


    private void Awake()
    {
        upd = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
        endScale = new Vector3(transform.localScale.x + .15f, transform.localScale.y + .15f, transform.localScale.z + .15f);
        mainPanel = false;
        notEnoughMoney.SetActive(false);
    }

    void Update()
    {
        if (mainPanel)
        {
            difference = HealtBar.Pb.maximumHealt - HealtBar.Pb.currentHealt;
            percent = HealtBar.Pb.currentHealt * .1f;
            buttons.SetActive(false);
        }
        else
        {

            buttons.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.localScale = endScale;
            Invoke("UpdatePanelOpen", 0.5f);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.localScale = startScale;
        }

    }

    void UpdatePanelOpen()
    {
        updatePanel.SetActive(true);
        mainPanel = true;
    }

    public void UpdatePanelClose()
    {
        updatePanel.SetActive(false);
        mainPanel = false;
    }

    public void HealthIncrease()
    {
        if (GameManager.gm.coin >= healthCoin)
        {
            HealtBar.Pb.currentHealt *= 1.1f;

            GameManager.gm.coin -= healthCoin;

            if (difference < percent)
            {
                float diff = percent - difference;
                HealtBar.Pb.maximumHealt += diff;
                HealtBar.Pb.currentHealt = HealtBar.Pb.maximumHealt;

            }
            else
                HealtBar.Pb.currentHealt *= 1.1f;



        }
        else
            StartCoroutine(EnoughMoney());



    }
    public void ShieldIncrease()
    {
        if (GameManager.gm.coin >= shieldCoin)
        {
            ShieldTimer.sh.shieldActiveTime *= 1.1f;
            GameManager.gm.coin -= shieldCoin;
        }
        else
            StartCoroutine(EnoughMoney());
    }
    public void DamageIncrease()
    {
        if (GameManager.gm.coin >= damageCoin)
        {
            GameManager.gm.laserDamage *= 1.1f;
            GameManager.gm.ultiDamage *= 1.1f;
            GameManager.gm.particleDamage *= 1.1f;
            GameManager.gm.rocketDamage *= 1.1f;

            GameManager.gm.coin -= damageCoin;

        }
        else
            StartCoroutine(EnoughMoney());


    }
    public void RocketIncrease()
    {
        if (GameManager.gm.coin >= rocketCoin)
        {
            MainTower.mainTower.rocketCount += 5;
            GameManager.gm.coin -= rocketCoin;
        }
        else
            StartCoroutine(EnoughMoney());

    }

    IEnumerator EnoughMoney()
    {
        notEnoughMoney.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        notEnoughMoney.SetActive(false);

    }

}
