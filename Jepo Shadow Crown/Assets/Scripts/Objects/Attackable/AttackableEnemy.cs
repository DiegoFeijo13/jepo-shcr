using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackableEnemy : AttackableBase
{
    public int MaxHealth;
    public GameObject DestroyObjectOnDeath;
    public BaseMovementModel MovementModel;
    public float HitPushStrength;
    public float HitPushDuration;
    public float DestroyDelayOnDeath;
    public GameObject DeathFX;
    public float DelayDeathFX;

    private int _health;

    private void Awake()
    {
        _health = MaxHealth;
    }

    public int GetHealth() => _health;    

    public override void OnHit(Collider2D collider, ItemType itemType)
    {
        _health--;

        if(MovementModel != null)
        {
            Vector3 pushDirection = transform.position - collider.gameObject.transform.position;
            pushDirection = pushDirection.normalized * HitPushStrength;

            MovementModel.PushCharacter(pushDirection, HitPushDuration);
        }

        if(_health <= 0)
        {
            StartCoroutine(DestroyCo(DestroyDelayOnDeath));

            if(DeathFX != null)
            {
                StartCoroutine(DeathFXCo(DelayDeathFX));
            }
        }
    }

    IEnumerator DestroyCo(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(DestroyObjectOnDeath);

        BroadcastMessage("OnLootDrop", SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator DeathFXCo(float delay)
    {
        yield return new WaitForSeconds(delay);

        Instantiate(DeathFX, transform.position, Quaternion.identity);
    }
}
