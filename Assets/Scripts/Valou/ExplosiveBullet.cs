using System.Collections;
using System.Collections.Generic;
using Guillaume;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    public ParticleSystem explosion;

    public float ExplosionRadius;
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        Collider2D[] result = new Collider2D[10];
        Physics2D.OverlapCircle(collision.GetContact(0).point, ExplosionRadius, new ContactFilter2D(), result);
        foreach (var col in result)
        {
            if (col && col.transform.tag.Equals(TargetTag))
            {
                Entity tmp = col.transform.gameObject.GetComponent<Entity>();
                tmp.TakeDamage(Damage);
            }
        }

        Animator animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("toDestroy", true);
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        //Destroy(gameObject);
    }
}
