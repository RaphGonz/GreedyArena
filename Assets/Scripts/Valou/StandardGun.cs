using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardGun : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShootCoroutine");
    }
    
    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot(new Vector2(0.7f, 0.3f));
            yield return new WaitForSeconds(0.5f);
        }
    }
}