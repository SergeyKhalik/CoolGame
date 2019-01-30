using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
    public enum Component { Up, Down, Left, Right };
    public Component component;
    private Vector2 Imp;
    private Hero hero;
    [SerializeField]
    private float Impulse = 5;
    [SerializeField]
    private int Damaage = 15;
    [SerializeField]
    private bool Move = false;
    [SerializeField]
    private Vector2 point1;
    [SerializeField]
    private Vector2 point2;
    [SerializeField]
    private float time = 5;
    [SerializeField]
    private float speed = 5;
    private float timet = 0;
    private void Start()
    {
        hero = FindObjectOfType<Hero>();
        switch (component)
        {
            case Component.Up:
                Imp = Vector2.up;
                break;
            case Component.Down:
                Imp = Vector2.down;
                break;
            case Component.Left:
                Imp = Vector2.left;
                break;
            case Component.Right:
                Imp = Vector2.right;
                break;
        }
        if (Move) { transform.position = point1; }
    }
    private void Update()
    {
        if (Move)
        {
            timet -= Time.deltaTime;
            if (timet > 0)
            {
                transform.position = Vector2.Lerp(transform.position, point2, speed*Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.Lerp(transform.position, point1, speed * Time.deltaTime);
            }
            if (timet < -time) timet = time;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            switch (component)
            {
                case Component.Up:
                    Hero.rb.velocity = new Vector2(Hero.rb.velocity.x,0);
                    break;
                case Component.Down:
                    Hero.rb.velocity = new Vector2(Hero.rb.velocity.x, 0);
                    break;
                case Component.Left:
                    Hero.rb.velocity = new Vector2(0, Hero.rb.velocity.y);
                    break;
                case Component.Right:
                    Hero.rb.velocity = new Vector2(0, Hero.rb.velocity.y);
                    break;
            }
            if (Hero.fields <= 0)
            {
                Hero.heart -= Damaage;
            }
            else
            {
                Hero.fields--;
            }
            hero.HaveImpulse(Impulse, Imp);
        }
    }
}
