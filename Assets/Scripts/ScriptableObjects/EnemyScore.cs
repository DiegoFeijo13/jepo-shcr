using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyScore : ScriptableObject
{
    public Dictionary<EnemyType, int> Score = new Dictionary<EnemyType, int>();
    public int KillsInARow;

    private EnemyType _currentEnemy;

    private void Reset()
    {
        Score = new Dictionary<EnemyType, int>();
    }

    public void UpdateKills(EnemyType enemy)
    {
        if (enemy == _currentEnemy)
            KillsInARow++;
        else
        {
            _currentEnemy = enemy;
            KillsInARow = 1;
        }

        UpdateScore(enemy);
    }

    private void UpdateScore(EnemyType enemy)
    {
        if(Score.TryGetValue(enemy, out int scoreValue))
        {
            Score[enemy] = scoreValue + 1;
        }
        else
        {
            Score.Add(enemy, 1);
        }
    }
}
