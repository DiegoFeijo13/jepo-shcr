using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonLevelTextManager : MonoBehaviour
{
    public DungeonLevelControl LevelControl;
    public TextMeshProUGUI LevelDisplay;

    private void Start()
    {
        UpdateLevelText();
    }

    private void Update()
    {
        UpdateLevelText();
    }

    public void UpdateLevelText()
    {
        LevelDisplay.text = "Level " + LevelControl.CurrentLevel;
    }
}
