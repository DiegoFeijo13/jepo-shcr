using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableBush : AttackableBase
{
    public Sprite DestroyedSprite;
    public GameObject DestroyEffect;
    public float DestroyDelay;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override void OnHit(Collider2D collider, ItemType itemType)
    {
        _spriteRenderer.sprite = DestroyedSprite;

        if(GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = false;
        }

        if(DestroyEffect != null)
        {
            StartCoroutine(DestroyFXCo(DestroyDelay));
        }

        BroadcastMessage("OnLootDrop", SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator DestroyFXCo(float delay)
    {
        GameObject destroyEffect = (GameObject)Instantiate(DestroyEffect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(delay);
        Destroy(destroyEffect);
    }
}
