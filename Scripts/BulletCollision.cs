using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public GameObject startParticle, endParticle;
    public float damage;


    void Start()
    {
        Destroy(gameObject, 3);
        Instantiate(startParticle, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!ShieldTimer.sh.shieldActive)
            {
                HealtBar.Pb.currentHealt -= damage;

            }

            Instantiate(endParticle, other.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Environment"))
        {
            Instantiate(endParticle, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }



    }


}
