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
    
    public void Shoot(Vector2 direction)
    {
        float currentUse = Time.time;
        if (currentUse >= nextAllowedFire)
        {
            // fire bullet
            GameObject newBulletGameObject = Instantiate(BulletGameObject, canon.position, transform.rotation);
            Bullet newBullet = newBulletGameObject.GetComponent<Bullet>();
            newBullet.gameObject.GetComponent<Rigidbody2D>().AddForce(newBullet.Speed * direction.normalized, ForceMode2D.Impulse);
            
            // update lastUse timer to handle cooldown of fire rate
            nextAllowedFire = currentUse + 1.0f / fireRate;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
