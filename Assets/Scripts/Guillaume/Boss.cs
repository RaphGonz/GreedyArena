using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Guillaume;
using UnityEngine;

public class Boss : Entity
{
    public Vector2[] tpPositions;

    private GameObject raccoon;

    public float tpCooldown = 15f;
    private float timeStart;
    private int currentPosition = 0;

    public bool invulnerable = false;
    public SpriteRenderer bossColor;

    // Start is called before the first frame update
    void Start()
    {
        timeStart = Time.time;
        transform.position = tpPositions[currentPosition];
        _health = maxHealth;
        raccoon = GameObject.Find("Racoon");
        if (raccoon != null)
        {
            raccoon.SetActive(false);
        }
        else
        {
            Debug.Log("Raccoon not found");
        }
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
        if (!invulnerable)
        {
            StartCoroutine(inVulnerabilityFrames());
            _health -= damage;

            if (_health <= 0)
            {
                Debug.Log("Partie Terminée !");
                Destroy(gameObject);
            }
        }
    }

    IEnumerator inVulnerabilityFrames()
    {
        invulnerable = true;
        var _initialColor = bossColor.color;
        var _color = new Color(bossColor.color.r, bossColor.color.g, bossColor.color.b, 0.2f);

        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
                bossColor.color = _color;
            else
                bossColor.color = _initialColor;
            yield return new WaitForSeconds(0.1f);

        }
        invulnerable = false;
    }


}