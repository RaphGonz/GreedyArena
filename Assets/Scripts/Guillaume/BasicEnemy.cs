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
    public int Damage;
    public float Speed;

    private Rigidbody2D _body;


    // Start is called before the first frame update
    void Start()
    {
        _health = maxHealth;
        Debug.Log("Pouet pouet cacahuète | _health =" +_health+" | maxHealth "+maxHealth);
        Target = GameObject.Find("Player");
        _targetTransform = Target.GetComponent<Transform>();
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _body.velocity = Speed * _body.velocity.normalized;
        Vector2 direction = (_targetTransform.position - transform.position);
        _body.AddForce(direction.normalized, ForceMode2D.Impulse);
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
            collision.gameObject.GetComponent<Entity>().TakeDamage(Damage);
        }
    }
}