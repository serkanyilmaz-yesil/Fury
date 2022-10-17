using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using GameAnalyticsSDK;

public class GameManager : MonoBehaviour
{

    public static GameManager gm;

    public TextMeshProUGUI cointext;
    public float coin;
    public float laserDamage, ultiDamage, particleDamage, rocketDamage;
    public int level;

    public GameObject nextButton, restartButton, buttons;

    private void Awake()
    {
        gm = this;
        Load();

    }
    // Start is called before the first frame update
    void Start()
    {
        nextButton.SetActive(false);
        restartButton.SetActive(false);
        GameAnalytics.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime((int)coin);
        Save();


        if (Boss.boss.die)
        {
            Invoke("NextButtonDelay", 3f);
            buttons.SetActive(false);

        }

        if (CharacterMove.ctrl.die)
        {
            Invoke("RestartButtonDelay", 3f);

        }
    }

    void NextButtonDelay()
    {
        nextButton.SetActive(true);

    }

    void RestartButtonDelay()
    {
        restartButton.SetActive(true);

    }


    public void NextLevel()
    {
        level++;
        if (level > 4)
        {
            level = 1;
        }
        SceneManager.LoadScene(level - 1);
    }

    public void RestartLevel()
    {
        HealtBar.Pb.currentHealt = HealtBar.Pb.maximumHealt;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    void DisplayTime(int timeToDisplay)
    {
        cointext.text = "" + timeToDisplay;
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("coin", coin);
        PlayerPrefs.SetFloat("laserDamage", laserDamage);
        PlayerPrefs.SetFloat("ultiDamage", ultiDamage);
        PlayerPrefs.SetFloat("particleDamage", particleDamage);
        PlayerPrefs.SetFloat("rocketDamage", rocketDamage);
        PlayerPrefs.SetFloat("rocketDamage", rocketDamage);
        PlayerPrefs.SetFloat("currentHealt", HealtBar.Pb.currentHealt);
        PlayerPrefs.SetFloat("shieldActiveTime", ShieldTimer.sh.shieldActiveTime);
        PlayerPrefs.SetInt("level", level);

        
    }


    public void Load()
    {
        if (PlayerPrefs.HasKey("coin"))
        {
            coin = PlayerPrefs.GetFloat("coin");
        }
        if (PlayerPrefs.HasKey("laserDamage"))
        {
            laserDamage = PlayerPrefs.GetFloat("laserDamage");
        }
        if (PlayerPrefs.HasKey("ultiDamage"))
        {
            ultiDamage = PlayerPrefs.GetFloat("ultiDamage");
        }
        if (PlayerPrefs.HasKey("particleDamage"))
        {
            particleDamage = PlayerPrefs.GetFloat("particleDamage");
        }
        if (PlayerPrefs.HasKey("rocketDamage"))
        {
            rocketDamage = PlayerPrefs.GetFloat("rocketDamage");
        }

        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }



    }




}
