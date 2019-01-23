using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Hero.HaveDamage(100);
            Hero.HaveDamage(100);
            Hero.HaveDamage(100);
            Hero.HaveDamage(100);
            Hero.HaveDamage(100);
            Hero.HaveDamage(100);
        }
    }
}
