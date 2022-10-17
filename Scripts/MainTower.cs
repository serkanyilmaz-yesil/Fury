using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainTower : MonoBehaviour
{
    public static MainTower mainTower;

    private Transform boss;

    public float rocketSpeed;
    public Transform attackPoint;

    public int rocketCount;


    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject rocket;
    public bool attack;
    public TextMeshProUGUI rocketCountText;

    private void Awake()
    {
        mainTower = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        attack = true;
    }

    // Update is called once per frame
    void Update()
    {
        rocketCountText.text = rocketCount.ToString();

        if (Updatee.upd.mainPanel) attack = false; else attack = true;

        if (rocketCount > 0 && attack)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {

        if (!alreadyAttacked)
        {
            rocketCount--;
            Rigidbody rb = Instantiate(rocket, attackPoint.position, attackPoint.rotation).GetComponent<Rigidbody>();
            rb.AddForce(attackPoint.forward * rocketSpeed, ForceMode.Impulse);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}
