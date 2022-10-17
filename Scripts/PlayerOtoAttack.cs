using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOtoAttack : MonoBehaviour
{
    public float OverlapRadius = 100.0f;

    private Transform nearestEnemy;
    private int enemyLayer;

    public float bulletSpeed;
    public Transform attackPoint;

    public GameObject startEffect;

    //private Animator animator;

    //bool anim;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile, dustTrail;

    public float attackRange;

    private Vector3 lookPos;

    private void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        dustTrail.SetActive(false);

    }
    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, OverlapRadius, 1 << enemyLayer);
        float minimumDistance = Mathf.Infinity;

        if (!Updatee.upd.mainPanel)
        {
            foreach (Collider collider in hitColliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    nearestEnemy = collider.transform;

                    lookPos = new Vector3(nearestEnemy.position.x, nearestEnemy.position.y + 1, nearestEnemy.position.z);
                }

            }
            if (minimumDistance < attackRange)
            {
                attackPoint.LookAt(lookPos);
                AttackPlayer();
                dustTrail.SetActive(false);
            }
            else
                dustTrail.SetActive(true);


        }

        if (CharacterMove.ctrl.die)
        {
            gameObject.SetActive(false);
        }


    }
    private void AttackPlayer()
    {

        if (!alreadyAttacked)
        {
            Instantiate(projectile, attackPoint.position, attackPoint.rotation);
            Instantiate(startEffect, attackPoint.position, attackPoint.rotation);
            //Rigidbody rb = Instantiate(projectile, attackPoint.position, attackPoint.rotation).GetComponent<Rigidbody>();
            //rb.AddForce(attackPoint.transform.forward * bulletSpeed);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);


        }

    }

    private void ResetAttack()
    {

        alreadyAttacked = false;
    }


}
