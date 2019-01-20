using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour {
    public static float heart = 100f;
    public static float speed = 230;
    public static bool win = false;
    public static int damtaken = 0;
    [SerializeField]
    public float jumpForse = 30.0f;
    [SerializeField]
    public int jumps = 0;
    public static int coins = 0;
    public static int fields = 3;
    public static int stars = 0;
    public static string colorbutton = "blue";
    public static int CoinsBoost = 1;
    public static float bulletspeed = 0f;
    public static int bullets = 30;
    public static float timetodestroybullet = 30;
    public static float MaxHP = 100f;
    public static float prom = 0;
    public static float promtime = 0;
    public static bool lop = false;
    public static Rigidbody2D rb;
    public static float diley = 0;
    public static Transform tr;
    private GameObject set;
    private GameObject canvas;
    private GameObject camera;
    private GameObject visual;
    private GameObject eventsystem;
    private GameObject pan1;
    private GameObject pan2;
    private Image pan;
    public static BGpanel p;
    public static Animator anim;
    private GameObject hug;
    SpriteRenderer sp;
    private bool grounded;
    private void Start()
    {
        
        Time.timeScale = 1;
        win = false;
        damtaken = 0;
        colorbutton = "blue";
        if (!FindObjectOfType<Camera>()) {
            GameObject clone1 = Instantiate(camera);
            CameraController cc = clone1.GetComponent<CameraController>();
            cc.target = this.transform;
            GameObject clone2 = Instantiate(canvas);
            Canvas cv = clone2.GetComponent<Canvas>();
            cv.renderMode = RenderMode.ScreenSpaceCamera;
            cv.worldCamera = clone1.GetComponent<Camera>();
            Instantiate(visual);
            Instantiate(eventsystem);
        }
        pan1 = GameObject.FindGameObjectWithTag("pan1");
        pan2 = GameObject.FindGameObjectWithTag("pan2");
        p = pan2.GetComponent<BGpanel>();
        pan = GetComponent<Image>();
    }
    private void Awake()
    {
        set = Resources.Load("Pause") as GameObject;
        hug = Resources.Load("Hug") as GameObject;
        canvas = Resources.Load("Canvas") as GameObject;
        camera = Resources.Load("Main Camera") as GameObject;
        visual = Resources.Load("Visual") as GameObject;
        eventsystem = Resources.Load("EventSystem") as GameObject;
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sp = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        grounded = isGround();
        anim.SetFloat("Horizontal", Math.Abs(Input.GetAxis("Horizontal"))); 
        anim.SetBool("IsGrounded", grounded); 
        anim.SetFloat("Veloncity", rb.velocity.y);
        anim.SetBool("Shift", Input.GetKey(KeyCode.LeftShift));
        if (!win)
        {
            if (heart <= 0)
            {
                Global.deads++;
                heart = 100;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                heart = 100;
            }
            if (Input.GetKeyDown(KeyCode.Escape) && !GameObject.FindGameObjectWithTag("Pause"))
            {
                Time.timeScale = 0;
                Canvas ca = FindObjectOfType<Canvas>();
                Instantiate(hug,ca.transform);
                Instantiate(set, ca.transform);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && GameObject.FindGameObjectWithTag("Pause"))
            {
                Time.timeScale = 1;
                Destroy(GameObject.FindGameObjectWithTag("Pause"));
                Destroy(GameObject.FindGameObjectWithTag("hug"));
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (grounded == false)
                {
                    if (jumps < 2)
                    {
                        Jump();
                        jumps++;
                    }
                }
                else
                {
                    jumps = 0;
                    Jump();
                    jumps++;
                }
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                sp.flipX = true;
            }
            else if(Input.GetAxis("Horizontal") != 0)
            {
                sp.flipX = false;
            }  
        }
    }
    public static void HaveDamage(int dam)
    {
        if (!win) {
            p.state = 1;
            if (fields <= 0)
            {
                heart -= dam;
                damtaken += dam;
            }
            else
            {
                fields--;
            }
        }
    }
    void FixedUpdate()
    {
        if (!win)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * (speed +  (Input.GetKey(KeyCode.LeftShift) ? 70 : 0) ) * Time.deltaTime, rb.velocity.y);
        }
    }
    void Jump()
    {
        rb.AddForce(transform.up * jumpForse, ForceMode2D.Impulse);
    }
    private bool isGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(new Vector2(tr.position.x-0.0095f,tr.position.y-0.9f),new Vector2(0.42f,0.045f), CapsuleDirection2D.Horizontal, 0);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Solid")
            {
                return true;
            }
        }
        return false;
    }
    public static void HaveImpulse(float n)
    {
        rb.AddForce(Vector2.up * n, ForceMode2D.Impulse);
    }
}
