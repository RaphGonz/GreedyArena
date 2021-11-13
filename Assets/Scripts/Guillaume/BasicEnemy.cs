using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public int MaxHealth;
    private int _health;

    public float Speed;

    public Vector2 Destination;

    private float _elapsedTime = 0;

    private bool _destinationReached;

    private float _timeToWait;

    // Start is called before the first frame update
    void Start()
    {
        _health = MaxHealth;
        Speed = 2f;
        Destination = new Vector2(5, 3);
        _elapsedTime = 0;
        _destinationReached = false;
        Debug.Log("Speed  = " + Speed + " | Last Destination = " + Destination);
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        float step = Time.deltaTime * Speed;
        Behaviour(Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            takeDamage(1);
        }
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
                _destinationReached = true;
                _timeToWait = _elapsedTime + 3;
            }
        }
        else if (_elapsedTime > _timeToWait)
        {
            System.Random rd = new System.Random();
            int rdX = rd.Next(-5, 5);
            int rdY = rd.Next(-5, 5);
            Destination = new Vector2(rdX, rdY);
            Debug.Log("New Destination = " + Destination);
            _destinationReached = false;
        }
    }

    private void takeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy " + this.name + " destroyed");
        }
    }
}