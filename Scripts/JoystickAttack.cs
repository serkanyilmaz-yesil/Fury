using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class JoystickAttack : MonoBehaviour

{

    public static JoystickAttack jy;
    [SerializeField]
    public LineRenderer LR;

    [SerializeField]
    Joystick attack;

    [SerializeField]
    Transform attackLookAtPoint;

    [SerializeField]
    public float trailDistance = 20f;

    [SerializeField]
    Transform player;

    [SerializeField]
    Transform particle;

    [SerializeField]
    float BulletYAxis;

    RaycastHit hit;
    public bool shoot;

    private void Awake()
    {
        jy = this;
    }

    private void FixedUpdate()
    {

        if (Mathf.Abs(attack.Horizontal) >= 0.2f || Mathf.Abs(attack.Vertical) >= 0.2f)
        {
            shoot = true;
            if (LR.gameObject.activeInHierarchy == false)
            {
                LR.gameObject.SetActive(true);
            }
            transform.position = new Vector3(player.position.x, 0.3f, player.position.z);

            attackLookAtPoint.position = new Vector3(attack.Horizontal + player.position.x, 0.3f, attack.Vertical + player.position.z);
            transform.LookAt(new Vector3(attackLookAtPoint.position.x, 0.3f, attackLookAtPoint.position.z));

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            LR.SetPosition(0, transform.position);

            if (Physics.Raycast(transform.position, transform.forward, out hit, trailDistance))
            {
                LR.SetPosition(1, hit.point);
            }

            else
            {
                LR.SetPosition(1, transform.position + transform.forward * trailDistance);
                LR.SetPosition(1, new Vector3(LR.GetPosition(1).x, 0, LR.GetPosition(1).z));
            }

        }

        else if (Mathf.Abs(attack.Horizontal) < 0.2f || Mathf.Abs(attack.Vertical) < 0.2f && LR.gameObject.activeInHierarchy == true)
        {

            LR.gameObject.SetActive(false);
            shoot = false;
        }


    }




    public void ShootBullet()


    {
        player.LookAt(attackLookAtPoint);
        player.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        Instantiate(particle, new Vector3(attackLookAtPoint.position.x, BulletYAxis, attackLookAtPoint.position.z), attackLookAtPoint.rotation);


    }

}



