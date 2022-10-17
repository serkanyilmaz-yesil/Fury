using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorUlti : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(-200f,200f) * 10 * Time.deltaTime));
    }
}
