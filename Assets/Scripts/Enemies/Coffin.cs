using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffin : EnemyBase
{
    [SerializeField] private GameObject[] enemies;    
    [SerializeField] private float cooldown;
    [SerializeField] private int maxEnemies;
       
    private int lastEnemyIndex;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private Animator animator;

    private bool canSpawn = true;

    private new void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        RemoveDestroyed();

        if (CanSpawn())
            PreSpawn();
    }

    private void PreSpawn()
    {
        animator.SetBool("IsSpawning", true);
    }

    //Called by the Summon animation
    private void SpawnEnemy()
    {
        if(enemies != null && enemies.Length > 0)
        {
            lastEnemyIndex++;
            if (lastEnemyIndex > enemies.Length - 1)
                lastEnemyIndex = 0;

            GameObject enemyToSpawn = enemies[lastEnemyIndex];

            Vector3 pos = gameObject.transform.position + Vector3.down;

            GameObject newEnemy = Instantiate(enemyToSpawn, pos, Quaternion.identity);

            spawnedEnemies.Add(newEnemy);

            animator.SetBool("IsSpawning", false);

            StartCoroutine(CooldownCo());
        }
    }

    private void RemoveDestroyed()
    {
        if (spawnedEnemies.Count > 0)
            spawnedEnemies.RemoveAll(x => x == null);
    }

    private bool CanSpawn()
    {
        return canSpawn && CharacterInRange != null && spawnedEnemies.Count < maxEnemies;
    }

    IEnumerator CooldownCo()
    {
        canSpawn = false;
        yield return new WaitForSeconds(cooldown);
        canSpawn = true;
    }
}
