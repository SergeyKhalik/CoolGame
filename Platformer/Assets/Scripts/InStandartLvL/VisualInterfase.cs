using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class VisualInterfase : MonoBehaviour {
    private Hero hero;
    private Text mon;
    private Image HP;
    private Image charge;
    private Image chargef;
    private Image f1;
    private Image f2;
    private Image f3;
    private Image f4;
    private Image f5;
    private void Start()
    {
        hero = GameObject.FindGameObjectWithTag("player").GetComponent<Hero>();
        mon = GameObject.FindGameObjectWithTag("MoneysCount").GetComponent<Text>();
        HP = GameObject.FindGameObjectWithTag("VisualHP").GetComponent<Image>();
        charge = GameObject.FindGameObjectWithTag("Charge").GetComponent<Image>();
        chargef = GameObject.FindGameObjectWithTag("BackCharge").GetComponent<Image>();
        f1 = GameObject.FindGameObjectWithTag("f1").GetComponent<Image>();
        f2 = GameObject.FindGameObjectWithTag("f2").GetComponent<Image>();
        f3 = GameObject.FindGameObjectWithTag("f3").GetComponent<Image>();
        f4 = GameObject.FindGameObjectWithTag("f4").GetComponent<Image>();
        f5 = GameObject.FindGameObjectWithTag("f5").GetComponent<Image>();
    }
    private void Update()
    {
        charge.transform.localScale = new Vector3(hero.energy/100,1,1);
        mon.text = Convert.ToString(Hero.coins);
        HP.transform.localScale = new Vector3(Hero.heart/100,1f,1f);
        if (Hero.fields >= 1) f1.enabled = true;
        else if (Hero.fields < 1) f1.enabled = false;
        if (Hero.fields >= 2) f2.enabled = true;
        else if (Hero.fields < 2) f2.enabled = false;
        if (Hero.fields >= 3) f3.enabled = true;
        else if (Hero.fields < 3) f3.enabled = false;
        if (Hero.fields >= 4) f4.enabled = true;
        else if (Hero.fields < 4) f4.enabled = false;
        if (Hero.fields >= 5) f5.enabled = true;
        else if (Hero.fields < 5) f5.enabled = false;
    }
}
