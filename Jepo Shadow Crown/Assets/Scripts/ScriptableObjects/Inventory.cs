using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item CurrentItem;
    public List<Item> Items = new List<Item>();
    public int Keys;
    public int Coins;

    public void OnEnable()
    {
        
    }

    public bool CheckForItem(Item item)
    {
        return Items.Contains(item);
    }

    public void AddItem(Item itemToAdd)
    {        
        if (itemToAdd.IsKey)
        {
            Keys++;
        }
        else
        {
            if (!Items.Contains(itemToAdd))
            {
                Items.Add(itemToAdd);
            }
        }
    }
}
