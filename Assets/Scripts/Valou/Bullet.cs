using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
     public float Speed;

     public int Damage;

     public float Lifetime;

     private float _startTime;

     public String TargetTag;

     void Start()
     {
          _startTime = Time.time;
     }

     void Update()
     {
          if (Time.time - _startTime > Lifetime)
          {
               Destroy(gameObject);
          }
     }

     protected abstract void OnCollisionEnter2D(Collision2D collision);
}
