using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public Transform TextPopup;

    private static GameAssets _instance;
    public static GameAssets Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            }

            return _instance;
        }
    }

    
}
