using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static void SpawnEnemies(HashSet<Vector3Int> positions, EnemiesDataBase db, int dgLevel)
    {
        if (positions == null || positions.Count == 0 || db == null)
            return;

        int maxEnemies = Random.Range(1, 5);

        int spawnedEnemies = 0;
        var enemies = db.GetEnemies(dgLevel);

        if ((enemies?.Count).GetValueOrDefault() == 0)
            return;

        while (spawnedEnemies < maxEnemies)
        {
            int rndPos = Random.Range(1, positions.Count);
            int rndEnemy = Random.Range(0, enemies.Count);
            var pos = positions.ElementAt(rndPos);
            var v3pos = new Vector3(pos.x, pos.y);
            var enemy = enemies.ElementAt(rndEnemy);

            var rotation = new Quaternion(0, 0, 0, 0);
            var enemyObject = Instantiate(enemy, v3pos, rotation);

            spawnedEnemies++;
        }

    }
}
