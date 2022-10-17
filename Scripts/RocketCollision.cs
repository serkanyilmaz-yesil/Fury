using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollision : MonoBehaviour
{
    public GameObject startParticle,endParticle;


    void Start()
    {
        Destroy(gameObject, 6);
        Instantiate(startParticle, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            Instantiate(endParticle, other.transform.position, Quaternion.identity);

        }
    }

}
