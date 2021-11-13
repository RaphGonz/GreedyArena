using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
     protected abstract Vector2 NewPosition(float dt);
     
     public Rigidbody2D body;

     public float Speed; 
     
     public Vector2 Velocity;

     public int dammage;

     public float lifetime;

     void Start()
     {
          body = GetComponent<Rigidbody2D>();
     }

     void Update()
     {
          Vector2 newPos = NewPosition(Time.deltaTime);
          transform.position = newPos;
     }
    
}
