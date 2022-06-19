using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPlacementHelper
{
    public List<EnemyPositionData> PlaceEnemies(EnemiesDataBase dataBase, int dungeonlevel, HashSet<Vector2Int> room, int minEnemies, int maxEnemies)
    {
        var enemyPositions = new List<EnemyPositionData>();

        var enemies = dataBase.Enemies.Where(x => (x.GetComponent<EnemyBase>()).Level == dungeonlevel).ToList();

        var helper = new ItemPlacementHelper(room, room);

        int numEnemies = Random.Range(minEnemies, maxEnemies + 1);
        for(int i = 0; i < numEnemies; i++)
        {
            var position = helper.GetItemPlacementPosition(PlacementType.OpenSpace, 1000, Vector2Int.one, false);

            var enemy = enemies[Random.Range(1, enemies.Count)];

            enemyPositions.Add(new EnemyPositionData
            {
                Position = position.Value,
                Enemy = enemy
            });
        }

        return enemyPositions;
    }
}

public class EnemyPositionData
{
    public GameObject Enemy;
    public Vector2 Position;

}
