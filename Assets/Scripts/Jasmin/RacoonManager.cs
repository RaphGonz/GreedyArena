using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacoonManager : MonoBehaviour
{
    [SerializeField] float timeDurationBeforeAnimation = 5;
    [SerializeField] float timeDurationOfAnimation = 2;
    [SerializeField] float racoonSpeed = 5;
    [SerializeField] float timeStart;
    [SerializeField] bool animationTriggered = false;
    [SerializeField] bool moving = false;

    [SerializeField] bool inAnimation = false;
    [SerializeField] Vector2 initialPosition;

    [SerializeField] Rigidbody2D rigidbody;


    [SerializeField] List<GameObject> weaponList;



    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        weaponList = new List<GameObject>(10);
    }

    // Update is called once per frame
    void Update()
    {
        if (animationTriggered)
        {
            if (Time.time - timeStart > timeDurationBeforeAnimation)
            {
                moving = true;
                animationTriggered = false;
            }
        }

        if (moving)
        {
            rigidbody.velocity = new Vector2(racoonSpeed, 0);
        }

        if (inAnimation)
        {
            rigidbody.velocity = new Vector2(0, 0);

            if (Time.time - timeStart > timeDurationOfAnimation)
            {
                moving = true;
                inAnimation = false;

                // ces trois lignes permettent de reset le racoon
                /*
                rigidbody.velocity = new Vector2(0, 0);
                moving = false;
                transform.position = initialPosition;
                */
            }
        }

        if (transform.position.x > 20)
        {
            rigidbody.velocity = new Vector2(0, 0);
            moving = false;
            transform.position = initialPosition;
        }
    }

    public void TriggerRacoon()
    {
        timeStart = Time.time;
        animationTriggered = true;
    }

    void StartAnimation()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            rigidbody.velocity = new Vector2(0, 0);
            StartAnimation();
            inAnimation = true;
            timeStart = Time.time;
            weaponList.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}
