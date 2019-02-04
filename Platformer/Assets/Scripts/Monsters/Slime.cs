using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {
    [SerializeField]
    private float speed = 0.03f;
    [SerializeField]
    private int damage = 10;
    bool fight = false;
    SpriteRenderer sp;
    Transform tr;
    Animator an;
    bool first = false;
    bool hd = false;
    float time = 0.5f;
    float timet = 0;
    private Hero hero;
    private void Awake()
    {
        hero = GameObject.FindObjectOfType<Hero>();
        sp = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
    }
    private void Update()
    {
        timet -= Time.deltaTime;
        an.SetBool("Fight", fight);
        if (!LRP()) {
            fight = false;
            if (LR() || !Down()) sp.flipX = sp.flipX == true ? false : true;
            transform.position += (sp.flipX == true ? Vector3.right : Vector3.left) * speed;
        }
        else
        {
            fight = true;
            if (!first)
            {
                first = true;
                timet = time;
            }
        }
        if (fight && timet <= 0.17 && !hd)
        {
            hd = true;
            //hero.HaveDamage(damage, (transform.position.x > hero.transform.position.x ? new Vector2(-1, 1) : new Vector2(1, 1)));
        }
        if (timet <= 0)
        {
            first = false;
            hd = false;
        }
    }
    private bool Down()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + (sp.flipX == true ? 0.46f : -0.46f), transform.position.y - 0.43f), new Vector2(0.09f, 0.09f), 0);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Solid")
            {
                return true;
            }
        }
        return false;
    }
    private bool LR()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + (sp.flipX == true ? 0.5f : -0.5f) , transform.position.y - 0.13f), new Vector2(0.2f, 0.38f),0);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "solid")
            {
                return true;
            }
        }
        return false;
    }
    private bool LRP()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + (sp.flipX == true ? 0.5f : -0.5f), transform.position.y - 0.13f), new Vector2(0.2f, 0.38f), 0);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "player")
            {
                return true;
            }
        }
        return false;
    }
}
