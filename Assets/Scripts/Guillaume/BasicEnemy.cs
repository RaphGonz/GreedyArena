using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Guillaume;
using UnityEngine;

public class BasicEnemy : Entity
{
    public GameObject Target;
    private Transform _targetTransform;

    public Vector2 Destination;

    public int Damage;
    public float Speed;


    // Start is called before the first frame update
    void Start()
    {
        _health = maxHealth;
        Debug.Log("Pouet pouet cacahuète | _health =" +_health+" | maxHealth "+maxHealth);
        Target = GameObject.Find("Player");
        _targetTransform = Target.GetComponent<Transform>();
        Destination = _targetTransform.position;
        Debug.Log("Speed  = " + Speed + " | Last Destination = " + Destination);
    }

    // Update is called once per frame
    void Update()
    {
        float step = Time.deltaTime * Speed;
        Behaviour(Time.deltaTime);
    }

    private void Behaviour(float deltaTime)
    {
        var position = transform.position;
        Vector3 lastPosition = position;
        float step = Speed * deltaTime;
        Vector3 newPosition = Vector3.MoveTowards(position, _targetTransform.position, step);
        transform.position = newPosition;
    }

    public override void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.tag.Equals("PlayerBody"))
        {
            collision.gameObject.GetComponent<Entity>().TakeDamage(1);
        }
    }
}