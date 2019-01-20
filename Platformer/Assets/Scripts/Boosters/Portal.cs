using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    [SerializeField]
    private Vector3 point1;
    [SerializeField]
    private Vector3 point;
    [SerializeField]
    private bool flip = false;
    [SerializeField]
    private bool flip1 = false;
    private float time = 2;
    private float timet = -10;
    bool use = false;
    bool close = false;
    bool open = false;
    int state = 1;
    Animator an;
    GameObject Her;
    SpriteRenderer sp;
    private void Awake()
    {
        an = GetComponent<Animator>();
        Her = GameObject.FindGameObjectWithTag("player");
        sp = GetComponent<SpriteRenderer>();
        sp.flipX = flip;
    }
    private void Update()
    {
        timet -= Time.deltaTime;
        an.SetBool("close", close);
        an.SetBool("open", open);
        if (use)
        {
            if (timet <= 0.35 && !close)
            {
                close = true;
            }
            else if (timet <= 0 && !open)
            {
                open = true;
                transform.position = state == 1 ? point1 : point;
                sp.flipX = state == 1 ? flip1 : flip;
                Hero.tr.position = new Vector3(transform.position.x,transform.position.y,-1);
            }
            else if (timet <= -1.2f)
            {
                Her.gameObject.SetActive(true);
                Hero.win = false;
                state = state == 1 ? 2 : 1;
                use = false; close = false;open = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player" && timet <= -3)
        {
            timet = time;
            Hero.win = true;
            use = true;
            Her.gameObject.SetActive(false);
            Hero.tr.position = transform.position;
        }
    }
}
