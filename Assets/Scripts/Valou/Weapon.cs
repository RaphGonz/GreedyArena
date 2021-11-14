using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject BulletGameObject;

    public int type;
    
    public int fireRate;

    private float nextAllowedFire;

    public Transform canon;

    public Animator animator;

    protected AudioManager _audioManager;
    
    private bool start = false;

    void Start()
    {
        
    }

    private void playSound()
    {
        if (BulletGameObject.GetComponent<Bullet>().GetType() == typeof(StandardBullet))
        {
            _audioManager.Play("BasicGun");
        }
        else if (BulletGameObject.GetComponent<Bullet>().GetType() == typeof(BouncingBullet))
        {
            _audioManager.Play("Laser");
        }
        else if (BulletGameObject.GetComponent<Bullet>().GetType() == typeof(PiercingBullet))
        {
            _audioManager.Play("Sniper");
        }
        else
        {
            _audioManager.Play("Launch");
        }
    }

    public void Shoot(Vector2 direction)
    {
        //� mettre dans le void Start quand il fonctionnera (quand la classe ne sera plus abstraite).
        if (!start)
        {
            //isHeld devra commencer � false
            animator.SetBool("isHeld", true);
            animator.SetBool("isShooting", false);
            start = true;
        }

        float currentUse = Time.time;
        if (currentUse >= nextAllowedFire)
        {
            StartCoroutine(shootingAnimation());
            // fire bullet
            playSound();
            GameObject newBulletGameObject = Instantiate(BulletGameObject, canon.position, transform.rotation);
            Bullet newBullet = newBulletGameObject.GetComponent<Bullet>();
            newBullet.gameObject.GetComponent<Rigidbody2D>().AddForce(newBullet.Speed * direction.normalized, ForceMode2D.Impulse);
            
            // update lastUse timer to handle cooldown of fire rate
            nextAllowedFire = currentUse + 1.0f / fireRate;
        }
    }

    IEnumerator shootingAnimation()
    {
        animator.SetBool("isShooting", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isShooting", false);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
