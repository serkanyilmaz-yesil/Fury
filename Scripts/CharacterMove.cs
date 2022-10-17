using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public static CharacterMove ctrl;

    [SerializeField]
    Joystick Joystick;
    public float speed;
    private Rigidbody rb;
    private Animator anim;
    public GameObject dustTrail;
    public bool die = false;

    private AudioSource sound;
    private AudioClip dieSound;
    bool dieSoundd = false;

    public GameObject buttons;

    private void Awake()
    {
        ctrl = this;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        dustTrail.SetActive(false);
        sound = GetComponent<AudioSource>();
        dieSound = Resources.Load<AudioClip>("playerDie");
    }

    private void Update()
    {


    }
    void FixedUpdate()
    {
        if (!die)
        {
            Hareket();

        }
        else
        {
            buttons.SetActive(false);
            anim.SetBool("die", true);
            anim.SetBool("idle", false);
            anim.SetBool("run", false);
            dustTrail.SetActive(false);
            if (!dieSoundd)
            {
                sound.PlayOneShot(dieSound, .2f);
                dieSoundd = true;
            }
            MainTower.mainTower.attack = false;
        }
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -24f, 24f);
        pos.z = Mathf.Clamp(pos.z, -10f, 76f);
        transform.position = pos;
    }

    void Hareket()
    {

        rb.velocity = new Vector3(Joystick.Horizontal * speed * Time.deltaTime, rb.velocity.y, Joystick.Vertical * speed * Time.deltaTime);

        if (Joystick.Horizontal != 0f || Joystick.Vertical != 0f)
        {

            transform.rotation = Quaternion.LookRotation(rb.velocity);
            anim.SetBool("run", true);
            anim.SetBool("idle", false);
            dustTrail.SetActive(true);


        }
        else
        {
            anim.SetBool("run", false);
            anim.SetBool("idle", true);
            dustTrail.SetActive(false);

        }


    }


}
