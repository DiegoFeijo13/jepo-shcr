﻿using Assets.Scripts.General;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : AttackableBase
{
    [SerializeField] public int Level;
    [SerializeField] protected EnemyType EnemyType;
    [SerializeField] protected int MaxHealth;
    [SerializeField] protected GameObject MainObject;
    [SerializeField] protected int Range;
    [SerializeField] protected float HitPushStrength;
    [SerializeField] protected float HitPushDuration;
    [SerializeField] protected float DestroyDelayOnDeath;
    [SerializeField] protected GameObject DeathFX;
    [SerializeField] protected float DelayDeathFX;
    [SerializeField] protected int MinDamage;
    [SerializeField] protected int MaxDamage;
    [SerializeField] protected LootTable LootTable;
    [SerializeField] protected GameObject Visuals;
    [SerializeField] protected GameObject CollisionTrigger;
    [SerializeField] protected EnemyScore EnemyScore;

    internal GameObject CharacterInRange;

    protected float _health;

    protected EnemyMovement movement;    

    protected Vector3 startPos;

    protected SpriteRenderer _spriteRenderer;
    protected Color _defaultColor;
    
    protected EnemyState CurrentState { get; set; }

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();        

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (_spriteRenderer != null)
            _defaultColor = _spriteRenderer.color;

        CurrentState = EnemyState.idle;
        _health = MaxHealth;
        if (MainObject != null)
        {
            startPos = MainObject.transform.position;
        }
    }

    private void MakeLoot()
    {
        if (LootTable != null)
        {
            DroppableItem current = LootTable.LootItem();
            if (current != null)
            {
                Instantiate(current.gameObject, base.transform.position, Quaternion.identity);
            }
        }
    }

    protected virtual void OnDie()
    {
        if (Visuals != null)
            Visuals.SetActive(false);
        if (CollisionTrigger != null)
            CollisionTrigger.SetActive(false);

        if (DeathFX != null)
        {
            StartCoroutine(DeathFXCo(DelayDeathFX));
        }

        EnemyScore.UpdateKills(this.EnemyType);
        MakeLoot();
        
        StartCoroutine(DestroyCo(DestroyDelayOnDeath));
    }

    #region Public Methods
    public virtual void OnAttack(GameObject character)
    {
        throw new System.NotImplementedException();
    }

    public override void OnHit(Vector2 pushDirection, ItemType itemType, int damage, bool isCritical)
    {
        _health -= damage;

        StartCoroutine(HitEffectCo(0.2f, damage, isCritical));

        if (_health <= 0)
        {
            OnDie();
        }
    }

    public void FullRestore()
    {
        MainObject.SetActive(true);
        Visuals.SetActive(true);
        CollisionTrigger.SetActive(true);
        MainObject.transform.position = startPos;
        _health = MaxHealth;
        CurrentState = EnemyState.idle;
    }
    #endregion Public Methods

    #region Coroutines
    IEnumerator DestroyCo(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(MainObject);
    }

    IEnumerator DeathFXCo(float delay)
    {
        yield return new WaitForSeconds(delay);

        Instantiate(DeathFX, base.transform.position, Quaternion.identity, MainObject.transform);
    }

    IEnumerator HitEffectCo(float delay, int damage, bool isCritical)
    {
        if (_spriteRenderer == null)
            yield break;

        _spriteRenderer.color = Color.red;
        var collider = gameObject.GetComponent<BoxCollider2D>();
        if(collider != null)
            collider.enabled = false;
        
        TextPopup.ShowDamage(damage, transform.position, isCritical);

        yield return new WaitForSeconds(delay);
        _spriteRenderer.color = _defaultColor;

        if (collider != null && _health > 0)
            collider.enabled = true;


    }

    internal float GetHealth() => _health;
    #endregion Coroutines
}
