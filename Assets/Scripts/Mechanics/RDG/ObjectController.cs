using System.Collections.Generic;
using UnityEngine;


public class ObjectController : MonoBehaviour
{
    public static void SpawnWallObject(Vector3 roomPosition, int roomRadius)
    {
        if (GameAssets.Instance.ObjectsDatabase == null || 
            GameAssets.Instance.ObjectsDatabase.WallObjects == null ||
            GameAssets.Instance.ObjectsDatabase.WallObjects.Length == 0)
            return;

        var objectsDb = GameAssets.Instance.ObjectsDatabase.WallObjects;
        int currentLvl = GameManager.Instance.CurrentLevel();

        //Where to spawn?
        int yPos = (int)roomPosition.y + roomRadius;

        int startXPos = (int)roomPosition.x - roomRadius + 1;
        int endXPos = (int)roomPosition.x + roomRadius - 1;
        int[] negativeXPositions = { (int)roomPosition.x - 1, (int)roomPosition.x, (int)roomPosition.x + 1 };
        for(int i = startXPos; i <= endXPos; i++)
        {
            if (i == negativeXPositions[0] ||
                i == negativeXPositions[1] ||
                i == negativeXPositions[2])
                continue;

            int chance = 35 - currentLvl;
            if(chance < 5)
                chance = 5;

            if (Random.Range(0, 100) >= chance)
                continue;

            var objectToSpawn = objectsDb[Random.Range(0, objectsDb.Length)];
            var position = new Vector3(i+.5f, yPos+.5f);
            Instantiate(objectToSpawn, position, Quaternion.identity);
        }
        
        
    }
}

