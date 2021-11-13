using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Guillaume;
using UnityEngine;

public class Boss : Entity
{
    public Weapon[] Weapons;

    private Weapon[] _activeWeapons;

    public float MinRange;

    public float MaxRange;

    public GameObject Target;

    private Vector2 _shootingDirection;

    private StringBuilder _attitude = new StringBuilder("hostile");

    private float _elapsedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        _health = maxHealth;
        _activeWeapons = new Weapon[] {Weapons[0], Weapons[1]};
    }

    // Update is called once per frame
    private void RotatePivotPoint()
    {
        transform.localScale = new Vector2(1, 1);

        _shootingDirection =
            (Target.transform.position -
             new Vector3(transform.position.x, transform.position.y, 0) * transform.localScale.x).normalized;
        float angleShoot = Vector2.SignedAngle(new Vector2(1, 0), _shootingDirection);


        if (angleShoot > 90 || angleShoot < -90)
        {
            gameObject.transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector2(1, 1);
        }
        //
        // Quaternion rotationArms = Quaternion.Euler(0, 0, angleShoot);
        // pivotPoint.transform.rotation = rotationArms;

        /*
        Quaternion rotationArms = Quaternion.Euler(0,0,angleArms);
        pivotPoint.transform.rotation = rotationArms;
        */
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > 1)
        {
            Shoot();
            _elapsedTime = 0;
        }
    }


        public void Shoot()
        {
            if (_activeWeapons.Length > 0)
            {
                foreach (var weapon in _activeWeapons)
                {
                    Debug.Log("Boss Shoot : Pew Pew Pew");
                    weapon.Shoot(_shootingDirection);
                }
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