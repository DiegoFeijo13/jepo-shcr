using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemiesDatabase : ScriptableObject
{
    public List<GameObject> Enemies = new List<GameObject>();

    internal List<GameObject> GetEnemies(int level)
    {
        if (Enemies.Count == 0)
            return Enemies;

        var enemiesToReturn = new List<GameObject>();
        //TODO: Can be better. Maybe a level range?
        foreach (var e in Enemies)
        {
            if(e.GetComponent<EnemyBase>().Level <= level)
                enemiesToReturn.Add(e);
        }
        
        return enemiesToReturn;
    }
}
