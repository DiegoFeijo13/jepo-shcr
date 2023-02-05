using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static void SpawnEnemies(Vector3 roomPosition, int roomRadius)
    {
        int lvl = GameManager.Instance.CurrentLevel();

        var enemiesDB = GameAssets.Instance.EnemiesDatabase;
        var enemies = enemiesDB.GetEnemies(lvl);
        var enemySpawnPoint = GameAssets.Instance.EnemySpawnPoint;

        if ((enemies?.Count).GetValueOrDefault() == 0)
            return;

        int maxEnemies = Random.Range(1, GameManager.Instance.EnemiesPerRoom() + 1);

        int spawnedEnemies = 0;

        //Positions
        int minX = (int)roomPosition.x - (roomRadius - 1);
        int maxX = (int)roomPosition.x + (roomRadius - 1);
        int minY = (int)roomPosition.y + (roomRadius - 1);
        int maxY = (int)roomPosition.y + (roomRadius - 1);

        var positionsUsed = new List<Vector3>();

        while (spawnedEnemies < maxEnemies)
        {
            int rndXPos = Random.Range(minX, maxX + 1);
            int rndYPos = Random.Range(minY, maxY + 1);            
            
            var v3pos = new Vector3(rndXPos, rndYPos);
            if (positionsUsed.Contains(v3pos))
                continue;

            positionsUsed.Add(v3pos);
            var enemy = enemies[Random.Range(0, enemies.Count)];
            enemySpawnPoint.enemy = enemy;
            Instantiate(enemySpawnPoint, v3pos, Quaternion.identity);

            spawnedEnemies++;
        }

    }
}
