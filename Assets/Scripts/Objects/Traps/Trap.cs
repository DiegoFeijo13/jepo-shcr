using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float DamagePerHit;
    public float Cooldown;
    public SpriteRenderer SpriteRenderer;
    public Sprite ActiveSprite;    

    protected bool _canHit;
    protected bool _active;
    protected Character _character;
    protected Sprite _defaultSprite;

    private void Awake()
    {
        if(SpriteRenderer != null)
            _defaultSprite = SpriteRenderer.sprite;
    }

    private void Update()
    {
        if (_active)
        {
            _canHit = true;
            Hit();
        }
        else
        {
            _canHit = false;
        }

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            this._character = collision.GetComponent<Character>();
            this._active = true;            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            this._character = null;
            this._active = false;
        }
    }

    protected IEnumerator CooldownCO()
    {
        _canHit = false;
        ChangeSprite(ActiveSprite);
        yield return new WaitForSeconds(Cooldown);
        _canHit = true;
        ChangeSprite(_defaultSprite);
    }

    protected void ChangeSprite(Sprite sprite)
    {
        if (sprite != null && SpriteRenderer != null)
            SpriteRenderer.sprite = sprite;
    }

    public virtual void Hit()
    {
        Debug.LogError("Not implemented!");
    }

    
}
