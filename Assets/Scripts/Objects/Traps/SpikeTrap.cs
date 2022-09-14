using Assets.Scripts.General;
using UnityEngine;

public class SpikeTrap : Trap
{
    public override void Hit()
    {
        if(_character != null && _canHit)
        {
            var damage = Calculator.CalculateDamage(minDamage, maxDamage);            
            _character.DealDamage(damage, Calculator.IsLastRollCritical);

            StartCoroutine(CooldownCO());
        }
    }

    
}
