using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : EnemyBase
{
    [SerializeField] private GameObject[] enemies;    
    [SerializeField] private float cooldown;
       
    private int _lastEnemyIndex;

    private bool canSpawn = true;

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if(enemies != null && enemies.Length > 0)
        {
            _lastEnemyIndex++;
            if (_lastEnemyIndex > enemies.Length - 1)
                _lastEnemyIndex = 0;

            GameObject enemyToSpawn = enemies[_lastEnemyIndex];

            Instantiate(enemyToSpawn, this.gameObject.transform.position, this.gameObject.transform.rotation);

            StartCoroutine(CooldownCo());
        }
    }

    IEnumerator CooldownCo()
    {
        canSpawn = false;
        yield return new WaitForSeconds(cooldown);
        canSpawn = true;
    }
}
