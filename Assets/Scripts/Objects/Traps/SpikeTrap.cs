using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{
    public override void Hit()
    {
        Debug.Log("hit " + DamagePerHit);
        if(_character != null && _canHit)
        {
            _character.DealDamage(DamagePerHit);

            StartCoroutine(CooldownCO());
        }
    }

    
}
