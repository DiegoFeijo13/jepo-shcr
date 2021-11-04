using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinTextManager : MonoBehaviour
{
    public Inventory PlayerInventory;
    public TextMeshProUGUI CoinDisplay;
    
    private void Start()
    {
        UpdateCoinCount();
    }

    private void Update()
    {
        UpdateCoinCount();
    }

    public void UpdateCoinCount()
    {
        CoinDisplay.text = "" + PlayerInventory.Coins;
    }
}
