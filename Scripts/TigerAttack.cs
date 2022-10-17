using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TigerAttack : MonoBehaviour
{

    public static TigerAttack tigeer;


    public NavMeshAgent agent;
    public float OverlapRadius = 500.0f;
    private float distance;
    private Transform player;

    public float bulletSpeed;
    public Transform attackPoint;




    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile, dustTrail;

    public float attackRange;


    private float laserDamage;
    private float ultiDamage;
    private float particleDamage;
    public bool panel;

    private void Awake()
    {
        tigeer = this;
        agent = GetComponent<NavMeshAgent>();
    }


    private void Start()
    {
        dustTrail.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        EnemySpawner.spawner.amountTiger++;

        laserDamage = GameManager.gm.laserDamage;
        ultiDamage = GameManager.gm.ultiDamage;
        particleDamage = GameManager.gm.particleDamage;

        panel = false;
    }
    private void FixedUpdate()
    {
        if (Updatee.upd.mainPanel) panel = true; else panel = false;


        if (!CharacterMove.ctrl.die)
        {
            if (!panel)
            {
                distance = Vector3.Distance(transform.position, player.position);

                if (distance < attackRange)
                {
                    transform.LookAt(player);
                    AttackPlayer();
                    dustTrail.SetActive(false);


                }
                else
                    dustTrail.SetActive(true);

                agent.SetDestination(player.position);

            }

        }


    }
    private void AttackPlayer()
    {

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, attackPoint.position, attackPoint.rotation).GetComponent<Rigidbody>();
            rb.AddForce(attackPoint.transform.forward * bulletSpeed);
            rb.AddForce(attackPoint.up * 5f, ForceMode.Impulse);

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
        EnemySpawner.spawner.amountTiger--;

    }


}
