using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Horn : MonoBehaviour
{
    [SerializeField]
    private bool Easter_lvl;
    private SpriteRenderer sp;
    private string fi = "Right";
    [SerializeField]
    private float speed = 0.04f;
    private int damage = 20;
    [SerializeField]
    private float yst1;
    [SerializeField]
    private float yst2;
    [SerializeField]
    private float x1;
    [SerializeField]
    private float x2;
    private double a = 0;
    private float b;
    private double c = 1;
    private float A;
    [SerializeField]
    private float V1;
    [SerializeField]
    private float V2;
    private float Ast;
    double w, q;
    System.Random rand = new System.Random();
    private Hero hero;
    //Animator an;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            hero.HaveDamage(damage);
        }
    }
    private void Start()
    {
        switch (Global.difficulty)
        {
            case 1:
                speed = 0.04f;
                damage = 10;
                break;
            case 2:
                speed = 0.045f;
                damage = 15;
                break;
            case 3:
                speed = 0.05f;
                damage = 20;
                break;
            case 4:
                speed = 0.055f;
                damage = 25;
                break;
            case 5:
                speed = 0.06f;
                damage = 30;
                break;
        }
        if (Easter_lvl)
        { damage = 100; }
        A = Math.Abs(yst1 - yst2) / 2;
        Ast = A;
        b = (yst1 + yst2) / 2;
    }
    private void Awake()
    {
        hero = GameObject.FindObjectOfType<Hero>();
        sp = GetComponent<SpriteRenderer>();
        //an = GetComponent<Animator>();
    }
    void Update()
    {
        if (Time.timeScale != 0)
        {
            transform.position = new Vector3(transform.position.x, A * (float)Math.Sin(c * transform.position.x - a) + b, transform.position.z);
            if (fi == "Right")
            {
                transform.position += Vector3.right * speed;
                sp.flipX = false;
            }
            else
            {
                transform.position += Vector3.left * speed;
                sp.flipX = true;
            }
            if (fi == "Right")
            {
                if (transform.position.x >= x2)
                {
                    c = rand.Next(5000, 20001) / 10000;
                    while (transform.position.y > b + A || transform.position.y < b - A)
                    {
                        w = (Ast - V1) * 1000000000;
                        q = (Ast - V2) * 1000000000;
                        A = rand.Next(Convert.ToInt32(w), Convert.ToInt32(q) + 1);
                    }
                    a = c * transform.position.x - Math.Asin((transform.position.y - b) / A);
                    fi = "Left";
                }
            }
            else
            {
                if (transform.position.x <= x1)
                {
                    c = rand.Next(5000, 20001) / 10000;
                    while (transform.position.y > b + A || transform.position.y < b - A)
                    {
                        w = (Ast - V1) * 1000000000;
                        q = (Ast - V2) * 1000000000;
                        A = rand.Next(Convert.ToInt32(w), Convert.ToInt32(q) + 1);
                    }
                    a = c * transform.position.x - Math.Asin((transform.position.y - b) / A);
                    fi = "Right";
                }
            }
        }
    }
}
