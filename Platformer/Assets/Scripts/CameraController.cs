﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float lensh = -10;
    private void Awake()
    {
        if (!target) target = FindObjectOfType<Hero>().transform;
    }

    private void Update()
    {
        Vector3 poz = target.position; poz.z = lensh;
        speed = Vector3.Distance(poz, target.position)/2;
        transform.position = Vector3.Lerp(transform.position, poz, speed * Time.deltaTime);
    }
}