using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject BulletGameObject;
    
    public int fireRate;

    public Transform canon;
    
    public void Shoot(Vector2 direction)
    {
        GameObject newBulletGameObject = Instantiate(BulletGameObject, canon.position, transform.rotation);
        Bullet newBullet = newBulletGameObject.GetComponent<Bullet>();
        newBullet.Velocity = newBullet.Speed * direction.normalized;
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
