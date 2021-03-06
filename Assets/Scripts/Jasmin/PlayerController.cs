using System.Collections;
using System.Collections.Generic;
using Guillaume;
using UnityEngine;

public class PlayerController : Entity
{
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] BoxCollider2D playerCollider;
    [SerializeField] GameObject pivotPoint;
    [SerializeField] RacoonManager racoon;
    [SerializeField] SpriteRenderer playerColor;

    // Movement config on the X axis
    [SerializeField] float _xForce = 100;
    [SerializeField] float _xDeceleration = 80;
    [SerializeField] float _xMaxVelocity = 3;
    [SerializeField] float _xMinSpeed = 0.1f;

    // Jump config
    [SerializeField] float _jumpForce = 20;
    [SerializeField] float __distanceFloorDetection = 0.1f;
    [SerializeField] bool _canJump = false;
    [SerializeField] LayerMask _maskSolid;

    // weapons
    [SerializeField] Weapon weapon;
    [SerializeField] Vector2 _shootingDirection;
    [SerializeField] float _weaponEjectionForce = 5;
    [SerializeField] bool _inChangeWeaponMenu = false;

    // body of the player (sprite)
    [SerializeField] GameObject body;

    //UI Manager : to notify death

    [SerializeField] UIManager uiManager;

    //particle

    public ParticleSystem jumpParticle;

    public bool invulnerable = false;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        _health = maxHealth;
        _maskSolid = LayerMask.GetMask("Solid", "SolidGround");

        uiManager.SetMaxHealth(_health);
    }

    // Update is called once per frame
    void Update()
    {
        ApplyDeceleration();
    }

    public void MoveX(float xValue)
    {
        //playerRigidbody.AddForce(new Vector2(xValue, 0) * _xMovementForce, ForceMode2D.Force);
        if (xValue > 0 && playerRigidbody.velocity.x <= _xMaxVelocity)
        {
            playerRigidbody.velocity += new Vector2(xValue, 0.001f) * _xForce * Time.deltaTime;
        }
        else if (xValue < 0 && playerRigidbody.velocity.x >= -_xMaxVelocity) {
            playerRigidbody.velocity += new Vector2(xValue, 0.001f) * _xForce * Time.deltaTime;
        }
        

    }

    void ApplyDeceleration()
    {
        if (playerRigidbody.velocity.x > _xMinSpeed)
        {
            playerRigidbody.velocity -= new Vector2(1, 0) * _xDeceleration * Time.deltaTime;
        }
        else if (playerRigidbody.velocity.x < -_xMinSpeed)
        {
            playerRigidbody.velocity += new Vector2(1, 0) * _xDeceleration * Time.deltaTime;
        }
    }



    public void Jump()
    {
        JumpRayCast();
        if (_canJump)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            playerRigidbody.AddForce(new Vector2(0, 1) * _jumpForce, ForceMode2D.Impulse);

            jumpParticle.Play();
        }
    }

    public void JumpRayCast()
    {
        Vector2 origin = new Vector2(playerCollider.bounds.min.x, playerCollider.bounds.min.y);

        RaycastHit2D ray = Physics2D.Raycast(origin, Vector2.down, __distanceFloorDetection, _maskSolid);
        Debug.DrawLine(origin, origin + Vector2.down * __distanceFloorDetection, Color.red, 0.1f);
        if (ray)
        {
            _canJump = true;
            return;
        }

        origin.x = playerCollider.bounds.max.x;
        ray = Physics2D.Raycast(origin, Vector2.down, __distanceFloorDetection, _maskSolid);
        Debug.DrawLine(origin, origin + Vector2.down * __distanceFloorDetection, Color.red, 0.1f);
        if (ray)
        {
            _canJump = true;
            return;
        }
       
        
        _canJump = false;
        
    }



    public void RotatePivotPoint(Vector2 mousePosition)
    {
        transform.localScale = new Vector2(1, 1);

        _shootingDirection = (mousePosition - new Vector2(transform.position.x, transform.position.y) * transform.localScale.x).normalized;
        float angleArms = Vector2.SignedAngle(new Vector2(1,0), _shootingDirection);





        if (angleArms > 90 || angleArms < -90)
        {
            body.transform.localScale = new Vector2(-1, 1);
            pivotPoint.transform.localScale = new Vector2(-1, 1);
            angleArms += 180;
        }
        else
        {
            body.transform.localScale = new Vector2(1, 1);
            pivotPoint.transform.localScale = new Vector2(1, 1);
        }

        

        Quaternion rotationArms = Quaternion.Euler(0, 0, angleArms);
        pivotPoint.transform.rotation = rotationArms;

        /*
        Quaternion rotationArms = Quaternion.Euler(0,0,angleArms);
        pivotPoint.transform.rotation = rotationArms;
        */
    }

    public void Shoot()
    {
        weapon.Shoot(_shootingDirection);
    }


    private void ChangeWeapon(GameObject newWeapon)
    {
        newWeapon.transform.position = new Vector2(0, 0);
        newWeapon.transform.parent = pivotPoint.transform;
        newWeapon.GetComponent<Rigidbody2D>().isKinematic = true;
        newWeapon.transform.SetPositionAndRotation(weapon.transform.position, weapon.transform.rotation);

        if (pivotPoint.transform.localScale.x < 0)
        {
            newWeapon.transform.localScale = new Vector2(-newWeapon.transform.localScale.x, newWeapon.transform.localScale.y);
        }
        newWeapon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        newWeapon.layer = LayerMask.NameToLayer("Default");

        weapon.transform.parent = null;
        weapon.GetComponent<Rigidbody2D>().isKinematic = false;
        weapon.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1,1) * _weaponEjectionForce);
        weapon.GetComponent<Rigidbody2D>().freezeRotation = false;
        weapon.gameObject.layer = LayerMask.NameToLayer("WeaponTrash");
        weapon.GetComponent<Animator>().SetBool("isHeld", false);
        weapon = newWeapon.GetComponent<Weapon>();
        racoon.TriggerRacoon();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            //Debug.Log("WeaponFound");

            ChangeWeapon(collision.gameObject);

            _inChangeWeaponMenu = true;
            OpenChangeWeaponMenu();
        }
    }

    void OpenChangeWeaponMenu()
    {

    }

    public override void TakeDamage(int damage)
    {

        if (!invulnerable)
        {
            StartCoroutine(inVulnerabilityFrames());
            _health -= damage;
            int rNumber = UnityEngine.Random.Range(1, 3);
            audioManager.Play("PlayerHurt" + rNumber);
            uiManager.SetHealth(_health);

            Debug.Log(damage + " point(s) de vie perdu(s) : current health = " + _health);
            if (_health <= 0)
            {
                Debug.Log("Game Over !!");
                uiManager.GameOver();
            }
        }
    }  

    IEnumerator inVulnerabilityFrames()
    {
        invulnerable = true;
        var _initialColor = playerColor.color;
        var _color = new Color(playerColor.color.r, playerColor.color.g, playerColor.color.b, 0.2f);

        for (int i = 0; i < 10; i++)
        {
            if(i%2==0)
                playerColor.color = _color;
            else
                playerColor.color = _initialColor;
            yield return new WaitForSeconds(0.1f);
            
        }
        invulnerable = false;
    }


}
