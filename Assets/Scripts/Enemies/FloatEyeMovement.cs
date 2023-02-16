using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEyeMovement : EnemyMovement
{
    protected new void FixedUpdate()
    {
        base.FixedUpdate();
        this.enemyBase.FlipSpriteX(facingDirection.x > 0);

    }
}
