using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public DroppableItem Item;
    public int ItemChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    public DroppableItem LootItem()
    {
        int cumProb = 0;
        int currentProb = Random.Range(0, 100);
        for (int i = 0; i < loots.Length; i++)
        {
            cumProb += loots[i].ItemChance;
            if (currentProb <= cumProb)
            {
                return loots[i].Item;
            }
        }
        return null;
    }
}
