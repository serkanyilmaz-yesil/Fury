using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss boss;
    private float rocketDamage;
    public bool die;

    public int attackCount;
    public int attackCountStart;
    public Transform[] targetPoints;
    public int target;
    public float time;
    public float maxTime;
    public GameObject rocket;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float rocketSpeed;
    public Transform attackPoint;
    bool attacked = false;

    private float laserDamage;
    private float ultiDamage;
    private float particleDamage;

    public bool panel;


    private void Awake()
    {
        boss = this;
        die = false;
    }

    private void Start()
    {
        rocketDamage = GameManager.gm.rocketDamage;
        laserDamage = GameManager.gm.laserDamage;
        ultiDamage = GameManager.gm.ultiDamage;
        particleDamage = GameManager.gm.particleDamage;
        panel = false;
        attackCountStart = attackCount;
    }

    private void Update()
    {
        if (Updatee.upd.mainPanel) panel = true; else panel = false;

        rocketDamage = GameManager.gm.rocketDamage;

        if (time >= maxTime)
        {
            attacked = true;
        }
        else
            time += Time.deltaTime;


        if (attacked)
        {
            if (attackCount > 0 )
            {
                if (!panel)
                {
                    AttackPlayer();

                }

            }
            else
            {
                time = 0;

                attacked = false;
                attackCount = attackCountStart;
            }

        }
    }

    private void AttackPlayer()
    {

        if (!alreadyAttacked)
        {
            attackCount--;
            target = Random.Range(0, targetPoints.Length);
            attackPoint.LookAt(targetPoints[target]);
            Rigidbody rb = Instantiate(rocket, attackPoint.position, attackPoint.rotation).GetComponent<Rigidbody>();
            rb.AddForce(attackPoint.transform.forward * rocketSpeed, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);


        }

    }

    private void ResetAttack()
    {

        alreadyAttacked = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rocket"))
        {
            gameObject.GetComponent<EnemyHealtBar>().currentHealt -= rocketDamage;
            GameManager.gm.coin += rocketDamage;

            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Laser"))
        {
            gameObject.GetComponent<EnemyHealtBar>().currentHealt -= laserDamage;
            GameManager.gm.coin += laserDamage;
        }
        if (other.gameObject.CompareTag("Ulti"))
        {
            gameObject.GetComponent<EnemyHealtBar>().currentHealt -= ultiDamage;
            GameManager.gm.coin += ultiDamage;

        }
        if (other.gameObject.CompareTag("PlayerParticle"))
        {
            gameObject.GetComponent<EnemyHealtBar>().currentHealt -= particleDamage;
            GameManager.gm.coin += particleDamage;

        }


    }

    private void OnDestroy()
    {
        die = true;
        MainTower.mainTower.attack = false;
        GameManager.gm.coin += 2000f;
    }

}
