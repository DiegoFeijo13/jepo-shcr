using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class EnemiesDataBase : ScriptableObject
{
    public List<GameObject> Enemies = new List<GameObject>();

    internal List<GameObject> GetEnemies(int level)
    {
        if (Enemies.Count == 0)
            return Enemies;

        //TODO: Can be better. Maybe a level range?
        return Enemies.Where(x => (x.GetComponent<EnemyBase>()).Level <= level).ToList();
    }
}
