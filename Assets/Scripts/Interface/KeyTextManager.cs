using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyTextManager : MonoBehaviour
{
    public Inventory PlayerInventory;
    public TextMeshProUGUI KeyDisplay;
    
    private void Start()
    {
        UpdateKeyCount();
    }

    void FixedUpdate()
    {
        UpdateKeyCount();
    }

    public void UpdateKeyCount()
    {
        KeyDisplay.text = "" + PlayerInventory.Keys;
    }
}
