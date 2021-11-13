using System.Collections;
using System.Collections.Generic;
using Guillaume;
using UnityEngine;

public class PiercingBullet : Bullet
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.tag.Equals(TargetTag))
        {
            collision.gameObject.GetComponent<Entity>().TakeDamage(Damage);
        }
    }
}
