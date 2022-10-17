using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterRobotAttack : MonoBehaviour
{

    public static ShooterRobotAttack shootRobot;



    public NavMeshAgent agent;
    public float OverlapRadius = 500.0f;
    private float distance;
    private Transform player;

    public float bulletSpeed;
    public Transform attackPoint;
    public GameObject dustTrail;

    private Animator animator;


    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public float attackRange;

    private float laserDamage;
    private float ultiDamage;
    private float particleDamage;

    public bool panel;


    private void Awake()
    {
        shootRobot = this;
        agent = GetComponent<NavMeshAgent>();
    }


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponentInChildren<Animator>();
        dustTrail.SetActive(false);
        EnemySpawner.spawner.amountShooterRobot++;

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
                    animator.SetBool("Shoot", true);
                    animator.SetBool("Walk", false);

                    transform.LookAt(player);
                    AttackPlayer();
                    dustTrail.SetActive(false);


                }
                else
                {
                    animator.SetBool("Walk", true);
                    animator.SetBool("Shoot", false);
                    dustTrail.SetActive(true);

                }

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
            rb.AddForce(attackPoint.up * 3f, ForceMode.Impulse);

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
        EnemySpawner.spawner.amountShooterRobot--;

    }

}
