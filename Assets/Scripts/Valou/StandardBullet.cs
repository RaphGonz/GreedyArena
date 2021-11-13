using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : Bullet
{
    // Start is called before the first frame update
    protected override Vector2 NewPosition(float dt)
    {
        return new Vector2(transform.position.x + Velocity.x * dt, transform.position.y + Velocity.y * dt);
    }
}
