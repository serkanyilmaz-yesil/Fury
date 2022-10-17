using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mancinik : MonoBehaviour
{

    public static Mancinik man;


    public NavMeshAgent agent;
    public float OverlapRadius = 500.0f;
    private float distance;
    private Transform player;

    public float bulletSpeed;
    public Transform attackPoint;




    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject bullet, dustTrail;

    public float attackRange;


    private float laserDamage;
    private float ultiDamage;
    private float particleDamage;
    private Animator animator;

    public bool panel;

    private void Awake()
    {
        man = this;
        agent = GetComponent<NavMeshAgent>();
    }


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dustTrail.SetActive(false);
        animator = GetComponentInChildren<Animator>();
        EnemySpawner.spawner.amountMancinik++;

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
            animator.CrossFadeInFixedTime("mancinikShoot", 1f);
            Rigidbody rb = Instantiate(bullet, attackPoint.position, attackPoint.rotation).GetComponent<Rigidbody>();
            rb.AddForce(attackPoint.forward * bulletSpeed, ForceMode.Impulse) ;
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
        EnemySpawner.spawner.amountMancinik--;

    }

}

