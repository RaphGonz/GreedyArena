using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
     protected abstract Vector2 NewPosition(float dt);
     
     public Rigidbody2D body;

     public float Speed; 
     
     public Vector2 Velocity;

     public int Dammage;

     public float Lifetime;

     private float _startTime;

     public String TargetTag;

     void Start()
     {
          body = GetComponent<Rigidbody2D>();
          _startTime = Time.time;
     }

     void Update()
     {
          if (Time.time - _startTime > Lifetime)
          {
               Destroy(gameObject);
          }
          else
          {
               Vector2 newPos = NewPosition(Time.deltaTime);
               transform.position = newPos;
          }
     }
}
