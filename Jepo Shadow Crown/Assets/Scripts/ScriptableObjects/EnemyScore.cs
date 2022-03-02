using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class EnemyScore : ScriptableObject
{
    public List<EnemyKillCount> Score = new List<EnemyKillCount>();
    public int KillsInARow;

    private Enemies _currentEnemy;

    public void UpdateKills(Enemies enemy)
    {
        if (enemy == _currentEnemy)
            KillsInARow++;
        else
        {
            _currentEnemy = enemy;
            KillsInARow = 1;
        }

        UpdateList(enemy);
    }

    private void UpdateList(Enemies enemy)
    {
        var killcount = Score.FirstOrDefault(x => x.Enemy == enemy);
        
        if (killcount != null)
            killcount.Count++;
        else
            Score.Add(new EnemyKillCount { Enemy = enemy, Count = 1 });
    }
}

[System.Serializable]
public class EnemyKillCount
{
    public Enemies Enemy;
    public int Count;
}
