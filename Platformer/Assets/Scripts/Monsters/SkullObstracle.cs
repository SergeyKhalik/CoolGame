using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullObstracle : MonoBehaviour {
    [SerializeField]
    private int damage = 20;
    private Hero hero;
    private void Awake()
    {
        hero = GameObject.FindObjectOfType<Hero>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            //hero.HaveDamage(damage, (transform.position.x > hero.transform.position.x ? new Vector2(-1, 1) : new Vector2(1, 1)));
        }
    }
}
