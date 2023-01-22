using Assets.Scripts.Mechanics.RDG;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    [SerializeField] private Tilemap floorMap;
    [SerializeField] private Tilemap wallMap;

    private List<Vector3Int> negativePos;
    private DungeonConfig _dngConfig;
    private RoomExits _roomExits;
    private bool isDone;

    public void FillRoom(RoomExits roomExits, DungeonConfig dngConfig)
    {
        _dngConfig = dngConfig;
        _roomExits = roomExits;
        FillFloor();
        FillWalls();

        isDone = true;
    }

    public List<Vector3> GetExitPositions(int stepSize)
    {
        if (!isDone)
            return new List<Vector3>();

        var positions = new List<Vector3>();

        if (_roomExits.HasTopExit())
        {
            var pos = new Vector3(this.transform.position.x, this.transform.position.y + stepSize);
            if(RoomManager.GetRoom(pos) == null)
                positions.Add(pos);
        }

        if (_roomExits.HasRightExit())
        {
            var pos = new Vector3(this.transform.position.x + stepSize, this.transform.position.y);
            if (RoomManager.GetRoom(pos) == null)
                positions.Add(pos);
        }

        if (_roomExits.HasBottomExit())
        {
            var pos = new Vector3(this.transform.position.x, this.transform.position.y - stepSize);
            if (RoomManager.GetRoom(pos) == null)
                positions.Add(pos);
        }

        if (_roomExits.HasLeftExit())
        {
            var pos = new Vector3(this.transform.position.x - stepSize, this.transform.position.y);
            if (RoomManager.GetRoom(pos) == null)
                positions.Add(pos);
        }

        return positions;
    }

    public RoomExits GetRoomExits() => _roomExits;

    private void FillFloor()
    {
        int minX = -_dngConfig.RoomRadius;
        int maxX = _dngConfig.RoomRadius;
        int minY = -_dngConfig.RoomRadius;
        int maxY = _dngConfig.RoomRadius + 1;
        for (int tilePosX = minX; tilePosX <= maxX; tilePosX++)
        {
            for (int tilePosY = minY; tilePosY < maxY; tilePosY++)
            {
                int tileIndex = Random.Range(0, _dngConfig.FloorTiles.Length);
                var tile = _dngConfig.FloorTiles[tileIndex];
                Vector3Int tilePos = new Vector3Int(tilePosX, tilePosY, 0);

                floorMap.SetTile(tilePos, tile);

            }
        }
    }

    private void FillWalls()
    {
        int leftWallX = -_dngConfig.RoomRadius;
        int rightWallX = _dngConfig.RoomRadius;
        int topWallY = _dngConfig.RoomRadius;
        int bottomWallY = -_dngConfig.RoomRadius;

        GetNegativePositions(topWallY, rightWallX, bottomWallY, leftWallX);

        //Fill left and right wall
        for (int tileY = topWallY; tileY >= bottomWallY; tileY--)
        {
            Vector3Int tileLeftPos = new Vector3Int(leftWallX, tileY, 0);
            if(!IsPositionNegative(tileLeftPos))
                wallMap.SetTile(tileLeftPos, _dngConfig.LeftWallTile);

            Vector3Int tileRightPos = new Vector3Int(rightWallX, tileY, 0);
            if (!IsPositionNegative(tileRightPos))
                wallMap.SetTile(tileRightPos, _dngConfig.RightWallTile);
        }

        //Fill top and bottom wall
        for (int tileX = leftWallX; tileX <= rightWallX; tileX++)
        {
            Vector3Int tileTopPos = new Vector3Int(tileX, topWallY, 0);
            if (!IsPositionNegative(tileTopPos))
                wallMap.SetTile(tileTopPos, _dngConfig.TopWallTile);

            Vector3Int tileBottomPos = new Vector3Int(tileX, bottomWallY, 0);
            if (!IsPositionNegative(tileBottomPos))
                wallMap.SetTile(tileBottomPos, _dngConfig.BotWallTile);
        }
    }

    private void GetNegativePositions(int topWallY, int rightWallX, int bottomWallY, int leftWallX)
    {
        negativePos = new List<Vector3Int>();

        if (_roomExits.HasTopExit())
        {
            negativePos.Add(new Vector3Int(-1, topWallY, 0));
            negativePos.Add(new Vector3Int(0, topWallY, 0));
            negativePos.Add(new Vector3Int(+1, topWallY, 0));
        }

        if (_roomExits.HasRightExit())
        {
            negativePos.Add(new Vector3Int(rightWallX, -1, 0));
            negativePos.Add(new Vector3Int(rightWallX, 0, 0));
            negativePos.Add(new Vector3Int(rightWallX, 1, 0));
        }

        if (_roomExits.HasBottomExit())
        {
            negativePos.Add(new Vector3Int(-1, bottomWallY, 0));
            negativePos.Add(new Vector3Int(0, bottomWallY, 0));
            negativePos.Add(new Vector3Int(+1, bottomWallY, 0));
        }

        if (_roomExits.HasLeftExit())
        {
            negativePos.Add(new Vector3Int(leftWallX, -1, 0));
            negativePos.Add(new Vector3Int(leftWallX, 0, 0));
            negativePos.Add(new Vector3Int(leftWallX, 1, 0));
        }
    }

    private bool IsPositionNegative(Vector3Int pos)
    {
        if (negativePos.Count == 0)
            return false;

        foreach (var nPos in negativePos)
        {
            if (nPos == pos)
                return true;
        }

        return false;
    }

}
