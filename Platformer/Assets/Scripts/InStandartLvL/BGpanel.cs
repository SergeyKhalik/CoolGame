using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGpanel : MonoBehaviour {
    [SerializeField]
    public int state = 0;
    [SerializeField]
    Animator an;
    private void Awake()
    {
       // an.GetComponent<Animator>();
    }
    private void Update()
    {
        an.SetInteger("State", state);
    }
}
