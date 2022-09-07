using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : EnemyBase
{
    [SerializeField] private GameObject[] enemies;    
    [SerializeField] private float cooldown;
    [SerializeField] private int maxEnemies;
       
    private int lastEnemyIndex;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private bool canSpawn = true;

    void FixedUpdate()
    {
        RemoveDestroyed();

        if (CanSpawn())
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if(enemies != null && enemies.Length > 0)
        {
            lastEnemyIndex++;
            if (lastEnemyIndex > enemies.Length - 1)
                lastEnemyIndex = 0;

            GameObject enemyToSpawn = enemies[lastEnemyIndex];

            GameObject newEnemy = Instantiate(enemyToSpawn, this.gameObject.transform.position, this.gameObject.transform.rotation);

            spawnedEnemies.Add(newEnemy);

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
        return canSpawn && characterInRange != null && spawnedEnemies.Count < maxEnemies;
    }

    IEnumerator CooldownCo()
    {
        canSpawn = false;
        yield return new WaitForSeconds(cooldown);
        canSpawn = true;
    }
}
