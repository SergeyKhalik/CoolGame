using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
    private Hero hero;
    private void Awake()
    {
        hero = GameObject.FindObjectOfType<Hero>(); 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            hero.HaveDamage(100);
            hero.HaveDamage(100);
            hero.HaveDamage(100);
            hero.HaveDamage(100);
            hero.HaveDamage(100);
            hero.HaveDamage(100);
        }
    }
}
