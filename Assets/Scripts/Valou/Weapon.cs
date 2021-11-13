using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject BulletGameObject;
    
    public int fireRate;

    private float nextAllowedFire;

    public Transform canon;

    public Animator animator;


    private bool start = false;

    void Start()
    {
        
    }

    public void Shoot(Vector2 direction)
    {
        //À mettre dans le void Start quand il fonctionnera (quand la classe ne sera plus abstraite).
        if (!start)
        {
            //isHeld devra commencer à false
            animator.SetBool("isHeld", true);
            animator.SetBool("isShooting", false);
            start = true;
        }

        float currentUse = Time.time;
        if (currentUse >= nextAllowedFire)
        {
            animator.SetBool("isShooting", true);
            // fire bullet
            GameObject newBulletGameObject = Instantiate(BulletGameObject, canon.position, transform.rotation);
            Bullet newBullet = newBulletGameObject.GetComponent<Bullet>();
            newBullet.gameObject.GetComponent<Rigidbody2D>().AddForce(newBullet.Speed * direction.normalized, ForceMode2D.Impulse);
            
            // update lastUse timer to handle cooldown of fire rate
            nextAllowedFire = currentUse + 1.0f / fireRate;
            //animator.SetBool("isShooting", false);
        }
    }

    

    // Update is called once per frame
    void Update()
    {

    }
}
