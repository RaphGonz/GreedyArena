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
    private bool _destinationReached = false;


    public int MaxHealth;
    private int _health;

    public int Damage;
    public float Speed;

    private float _elapsedTime = 0;
    public float WaitTime;
    private float _timeToWait;


    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player");
        _targetTransform = Target.GetComponent<Transform>();
        Destination = _targetTransform.position;
        _health = MaxHealth;
        Debug.Log("Speed  = " + Speed + " | Last Destination = " + Destination);
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        float step = Time.deltaTime * Speed;
        Behaviour(Time.deltaTime);
    }

    private void Behaviour(float deltaTime)
    {
        if (!_destinationReached)
        {
            var position = transform.position;
            Vector3 lastPosition = position;
            float step = Speed * deltaTime;
            Vector3 newPosition = Vector3.MoveTowards(position, Destination, step);
            transform.position = newPosition;

            if (lastPosition.Equals(newPosition))
            {
                Debug.Log("Last position " + lastPosition + " | "+newPosition);
                _destinationReached = true;
                _timeToWait = _elapsedTime + WaitTime;
            }
        }
        else if (_elapsedTime > _timeToWait)
        {
            Destination = _targetTransform.position;
            Debug.Log("New Destination = " + Destination);
            _destinationReached = false;
        }
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
        if (collision.gameObject.CompareTag("PlayerBody"))
        {
            Debug.Log("Oof !");
            // vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
            // Jasmin, ici pour prendre des dégâts
            //
            // collision.gameObject.TakeDamage
            // ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        }
    }
}