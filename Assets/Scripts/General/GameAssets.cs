using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public Transform TextPopup;
    
    [Header("Enemies Stuff")]
    public EnemiesDatabase EnemiesDatabase;
    public EnemySpawnPoint EnemySpawnPoint;

    [Header("Objects Stuff")]
    public ObjectsDatabase ObjectsDatabase;

    private static GameAssets _instance;
    public static GameAssets Instance
    {
        get
        {
            if (_instance == null)
            {
                //_instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }


}
