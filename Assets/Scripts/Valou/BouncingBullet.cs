using System.Collections;
using System.Collections.Generic;
using Guillaume;
using UnityEngine;

public class BouncingBullet : Bullet
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collide");
        // don't forget to add bouncing property in physics component
        if (collision.collider.transform.tag.Equals(TargetTag))
        {
            collision.gameObject.GetComponent<Entity>().TakeDamage(Damage);

            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.SetBool("toDestroy", true);
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            //Destroy(gameObject);
        }
    }
}
