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
    public static bool won = false;
    public static int damtaken = 0;
    [Header("Сила прыжка персонажа")]
    [SerializeField]
    public float jumpForse = 30.0f;
    [Header("Количество прыжков")]
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
    private GameObject cameran;
    private GameObject visual;
    private GameObject eventsystem;
    public static Animator anim;
    private GameObject hug;
    SpriteRenderer sp;
    BoxCollider2D bc;
    private bool grounded;
    [SerializeField]
    private bool speedMode = false;
    private float timersM = 0;
    private float timerdam = 0;
    private bool shift = false;
    [SerializeField]
    private float delay = 0.7f;
    [SerializeField]
    private int attacks = 0;
    [SerializeField]
    public float energy = 100;
    [SerializeField]
    public float EergyRegeneration = 150;
    [SerializeField]
    private bool move = true;
    [SerializeField]
    private bool attack = false;
    [SerializeField]
    private bool damageget = false;
    [SerializeField]
    private bool death = false;
    [SerializeField]
    private bool dhd = false;
    private void Start()
    {
        heart = 100;
        win = false;won = false;dhd = false;
        Time.timeScale = 1;
        win = false;
        damtaken = 0;
        colorbutton = "blue";
        if (!FindObjectOfType<Camera>()) {
            GameObject clone1 = Instantiate(cameran);
            CameraController cc = clone1.GetComponent<CameraController>();
            cc.target = this.transform;
            GameObject clone2 = Instantiate(canvas);
            Canvas cv = clone2.GetComponent<Canvas>();
            cv.renderMode = RenderMode.ScreenSpaceCamera;
            cv.worldCamera = clone1.GetComponent<Camera>();
            Instantiate(visual);
            Instantiate(eventsystem);
        }
    }
    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        set = Resources.Load("Pause") as GameObject;
        hug = Resources.Load("Hug") as GameObject;
        canvas = Resources.Load("Canvas") as GameObject;
        cameran = Resources.Load("Main Camera") as GameObject;
        visual = Resources.Load("Visual") as GameObject;
        eventsystem = Resources.Load("EventSystem") as GameObject;
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sp = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameObject.FindGameObjectWithTag("Pause"))
        {
            Time.timeScale = 0;
            Canvas ca = FindObjectOfType<Canvas>();
            Instantiate(hug, ca.transform);
            Instantiate(set, ca.transform);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameObject.FindGameObjectWithTag("Pause"))
        {
            Time.timeScale = 1;
            Destroy(GameObject.FindGameObjectWithTag("Pause"));
            Destroy(GameObject.FindGameObjectWithTag("hug"));
        }
        if (Time.timeScale != 0)
        {
            timersM -= Time.deltaTime;
            timerdam -= Time.deltaTime;
            grounded = isGround();
            #region Animator
            anim.SetFloat("Horizontal", Math.Abs(Input.GetAxis("Horizontal")));
            anim.SetBool("IsGrounded", grounded);
            anim.SetBool("speedMode", speedMode);
            anim.SetBool("Damaget", damageget);
            anim.SetFloat("Veloncity", rb.velocity.y);
            anim.SetBool("Shift", shift);
            #endregion
            if (timerdam < 0) { win = false; damageget = false; move = true; }
            if (timersM < 0) win = false;
            if (heart <= 0) { move = false; attack = false; damageget = false; death = true;won = true; }
            if (!won && !win)
            {
                #region Collider
                if (grounded && rb.velocity.y >= 0.1)
                {
                    bc.offset.Set(-0.00943f, -0.2993f);
                    bc.size.Set(0.3287f, 0.9996f);
                }
                else
                {
                    bc.offset.Set((float)-0.009435f, (float)-0.1496f);
                    bc.size.Set((float)0.32878f, (float)1.2992f);
                }
                #endregion
                #region SpeedMode
                if (!speedMode) shift = Input.GetKey(KeyCode.LeftShift);
                if (Input.GetKeyDown(KeyCode.Tab) && timersM < 0 && Math.Abs(Input.GetAxis("Horizontal")) < 0.01 && grounded && move)
                {
                    speedMode = !speedMode;
                    timersM = 0.52f;
                    win = true;
                    shift = true;
                }
                #endregion
                if (move)
                {
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
                    else if (Input.GetAxis("Horizontal") != 0)
                    {
                        sp.flipX = false;
                    }
                }
                else if (attack)
                {

                }
                else if (damageget)
                {
                    won = true;
                    timerdam = 1f;
                }
                /*
                if (!win)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0) && attacks < 3 && energy > 20 && timersM < 0)
                    {
                        attacks++;
                        Attack = true;
                        energy -= 20;
                    }
                    if (timerdam < 0)
                    {
                        damaget = false;
                        win = false;
                    }
                    else
                    {
                        sp.color = new Color(255, sp.color.b + (255 * Time.deltaTime) / delay, sp.color.b + (255 * Time.deltaTime) / delay);
                    }
                }
                */
            }
            if (death)
            {
                Global.deads++;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    public void HaveDamage(int dam, Vector2 dir, float imp)
    {
        if (!won && Time.timeScale != 0 && !dhd)
        {
            #region Fix health
            if (fields <= 0)
            {
                heart -= dam;
                damtaken += dam;
            }
            else
            {
                fields--;
            }
            #endregion
            win = true;
            damageget = true; move = false; attack = false; death = false;
            if (timersM > 0) { timersM = -1; speedMode = false;  }
            HaveImpulse(imp, dir);
        }
    }
    void FixedUpdate()
    {
        if (!win && !won)
        {
            if(move)rb.velocity = new Vector2(Input.GetAxis("Horizontal") * (speed +  (shift ? 70 : 0) ) * Time.deltaTime, rb.velocity.y);
        }
        if (energy < 100) { energy += 0.001f * EergyRegeneration; }
    }
    void Jump()
    {
        rb.AddForce(transform.up * jumpForse, ForceMode2D.Impulse);
    }
    public void HaveImpulse(float n, Vector2 dir)
    {
        rb.AddForce(dir * n, ForceMode2D.Impulse);
    }
    private bool isGround()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(tr.position.x - 0.0095f, tr.position.y - 0.92f), new Vector2(0.431f, 0.08f), 0.01f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Solid")
            {
                return true;
            }
        }
        return false;
    }
}
