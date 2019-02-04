using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undead : MonoBehaviour {
    [SerializeField]
    private float left = -10;
    [SerializeField]
    private float right = 10;
    [SerializeField]
    private float speed = 10;
    private Transform tr;
    private SpriteRenderer sp;
    private Animator an;
    private bool diraction = true;
    private bool Idle = true;
    private bool walk = false;
    private bool attack = false;
    private bool damaget = false;
    private bool death = false;
    private float attacktime = 0;
    [SerializeField]
    private float ImpulseToHero = 250;
    private bool attached = false;
    private bool takedamage = false;
    [SerializeField]
    private int Damage = 50;
    private Hero hero;
    private void Awake()
    {
        tr = GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
        hero = FindObjectOfType<Hero>();
    }
    private void Start()
    {
        while (!Check())
        {
            tr.position = new Vector2(tr.position.x,tr.position.y-0.0001f);
        }
    }
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            attacktime -= Time.deltaTime;
            if (Idle)
            {
                if (Hero.tr.position.x > left && Hero.tr.position.x < right)
                {
                    walk = true;
                    Idle = false;
                    an.SetBool("Walk", walk);
                    an.SetBool("Idle", Idle);
                }
            }
            else if (walk)
            {
                if (tr.position.x > right) diraction = false;
                if (tr.position.x < left) diraction = true;
                sp.flipX = !diraction;
                tr.position = tr.position = new Vector2(tr.position.x + (diraction == true ? speed / 1000 : -speed / 1000), tr.position.y);
                if (CheckHero())
                {
                    attack = true;
                    walk = false;
                    an.SetBool("Attack", attack);
                    an.SetBool("Walk", walk);
                }
            }
            else if (attack)
            {
                if (attacktime < 0 && !attached)
                {
                    attacktime = 1;
                    attached = true;
                }
                else if (attacktime > 0)
                {
                    if (attacktime < 0.6f && !takedamage)
                    {
                        takedamage = true;
                        if (CheckHero()) hero.HaveDamage(Damage,diraction ? new Vector2(1, 1) : new Vector2(-1, 1), ImpulseToHero);
                    }
                }
                else
                {
                    takedamage = false;
                    attached = false;
                    attack = false;
                    walk = true;
                    an.SetBool("Attack", attack);
                    an.SetBool("Walk", walk);
                }
            }
            else if (damaget)
            {

            }
            else if (death)
            {

            }
        }
    }
    private bool Check()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(new Vector2(tr.position.x,tr.position.y-0.8832f));
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Solid")
            {
                return true;
            }
        }
        return false;
    }
    private bool CheckHero()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(tr.position.x+ (diraction == true ? 0.7f : -0.7f), tr.position.y-0.17f),new Vector2(1.2f,1.3f),0);
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
