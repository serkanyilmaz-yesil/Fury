using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner spawner;

    public Transform[] spawnPoint;
    public GameObject boxingRobot, shooterRobot, mancinik, tiger,tank,enemy1;
    public int amountBoxingRobot;
    public int amountShooterRobot;
    public int amountTiger;
    public int amountMancinik;
    public int amountEnemy1;
    public int amountTank;
    public int maxAmount;

    public int waveAttack;
    public float waveTime;
    public float waveMaxTime;
    public float nextAttack;
    public float nextAttackStart;
    public TextMeshProUGUI nextAttackTime;
    public GameObject nextAttackedObj;
    private bool nextAttacked = false;

    private void Awake()
    {
        spawner = this;
    }

    private void Start()
    {
        nextAttackedObj.SetActive(false);
    }

    private void Update()
    {
        if (!CharacterMove.ctrl.die)
        {

            if (!nextAttacked)
            {
                waveTime += Time.deltaTime;

            }

            if (!Boss.boss.die && waveTime < waveMaxTime)
            {
                if (amountBoxingRobot < maxAmount)
                {
                    Instantiate(boxingRobot, spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity);
                }
                if (amountShooterRobot < maxAmount)
                {
                    Instantiate(shooterRobot, spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity);
                }
                if (amountTiger < maxAmount)
                {
                    Instantiate(tiger, spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity);
                }
                if (amountMancinik < maxAmount)
                {
                    Instantiate(mancinik, spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity);
                }
                if (amountEnemy1 < maxAmount)
                {
                    Instantiate(enemy1, spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity);
                }
                if (amountTank < maxAmount)
                {
                    Instantiate(tank, spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity);
                }


                nextAttackedObj.SetActive(false);

            }

            DisplayTime((int)nextAttack);
            StartCoroutine(AttackTimeControl());

            if (nextAttack > 0)
            {
                nextAttack -= Time.deltaTime;
            }

        }
    }

    IEnumerator AttackTimeControl()
    {
        if (waveTime >= waveMaxTime)
        {
            if (!nextAttacked)
            {
                nextAttack = nextAttackStart;
                waveAttack++;
                nextAttacked = true;
            }
            nextAttackedObj.SetActive(true);

            yield return new WaitForSeconds(nextAttack);
            nextAttacked = false;
            waveTime = 0;



        }


    }

    void DisplayTime(int timeToDisplay)
    {
        nextAttackTime.text = "Next Attack : " + timeToDisplay;
    }

}
