using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoxingRobot : MonoBehaviour
{
    public static BoxingRobot boxing;

    public NavMeshAgent agent;
    public float OverlapRadius = 500.0f;
    private float distance;

    private Transform player;

    public GameObject dustTrail;

    private Animator animator;

    public float time;

    public float attackRange;


    private float laserDamage;
    private float ultiDamage;
    private float particleDamage;


    [HideInInspector]
    public AudioSource audioSource;
    public AudioClip[] soundtrack;

    public bool panel;


    private void Awake()
    {
        boxing = this;

        agent = GetComponent<NavMeshAgent>();
    }


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponentInChildren<Animator>();
        dustTrail.SetActive(false);
        EnemySpawner.spawner.amountBoxingRobot++;
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;

        laserDamage = GameManager.gm.laserDamage;
        ultiDamage = GameManager.gm.ultiDamage;
        particleDamage = GameManager.gm.particleDamage;

        panel = false;

    }
    private void FixedUpdate()
    {
        if (Updatee.upd.mainPanel) panel = true; else panel = false;


        distance = Vector3.Distance(transform.position, player.position);


        if (!CharacterMove.ctrl.die)
        {
            if (!panel)
            {
                if (distance < attackRange)
                {

                    animator.SetBool("Boxing", true);
                    animator.SetBool("MiniRoboWalk", false);
                    dustTrail.SetActive(false);
                    time += Time.deltaTime;


                }
                else
                {
                    animator.SetBool("Boxing", false);
                    animator.SetBool("MiniRoboWalk", true);
                    dustTrail.SetActive(true);

                }
                agent.SetDestination(player.position);

            }
        }


        if (Boss.boss.die)
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (!audioSource.isPlaying && distance < attackRange && !CharacterMove.ctrl.die)
        {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }
    }
    private AudioClip GetRandomClip()
    {
        return soundtrack[Random.Range(0, soundtrack.Length)];
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
        EnemySpawner.spawner.amountBoxingRobot--;

    }

}
