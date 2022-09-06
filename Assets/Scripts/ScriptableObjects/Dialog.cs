using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialog : ScriptableObject
{
    public bool IsActive;
    public string CurrentText;
    
    public void Close()
    {
        IsActive = false;
        CurrentText = string.Empty;
    }

    public void ShowText(string text)
    {
        IsActive = true;
        CurrentText = text;
    }
}
