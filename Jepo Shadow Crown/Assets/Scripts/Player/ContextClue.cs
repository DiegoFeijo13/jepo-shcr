using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    public GameObject PlayerContextClue;
    private bool _contextActive = false;

    public void ChangeContext()
    {
        _contextActive = !_contextActive;
        PlayerContextClue.SetActive(_contextActive);
    }
}
