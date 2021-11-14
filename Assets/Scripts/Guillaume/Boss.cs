using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Guillaume;
using UnityEngine;

public class Boss : Entity
{
    public Vector2[] tpPositions;

    public float tpCooldown = 15f;
    private float timeStart;
    private int currentPosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeStart = Time.time;
        transform.position = tpPositions[currentPosition];
        _health = maxHealth;
    }


    void Update()
    {
        if (Time.time - timeStart > tpCooldown)
        {
            currentPosition = (currentPosition + 1) % tpPositions.Length;
            transform.position = tpPositions[currentPosition];
            timeStart = Time.time;
        }
    }


    public override void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Debug.Log("Partie Terminée !");
            Destroy(gameObject);
        }
    }
}