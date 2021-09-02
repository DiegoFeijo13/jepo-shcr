using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventoryModel : MonoBehaviour
{
    Dictionary<ItemType, int> _inventory = new Dictionary<ItemType, int>();

    public void AddItem(ItemType itemType, int amount = 1)
    {
        if (_inventory.ContainsKey(itemType))
        {
            _inventory[itemType] += amount;
        }
        else
        {
            _inventory.Add(itemType, amount);
        }
    }
}
