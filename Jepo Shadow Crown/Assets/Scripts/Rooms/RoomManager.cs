using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomData
{
    public string Name;
    public int X;
    public int Y;    
}

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    string _currentMap = "Level1";
    bool _isLoadingRoom = false;

    RoomData _currentLoadingRoom;
    Queue<RoomData> _roomQueue = new Queue<RoomData>();
    List<RoomParent> _loadedRooms = new List<RoomParent>();

    private void Awake()
    {
        Instance = this;    
    }

    void Start()
    {
        LoadRoom("Start", 0, 0);        
    }

    private void Update()
    {
        UpdateRoomQueue();
    }

    private void UpdateRoomQueue()
    {
        if (_isLoadingRoom)
            return;

        if (_roomQueue.Count == 0)
            return;

        _currentLoadingRoom = _roomQueue.Dequeue();
        _isLoadingRoom = true;
        StartCoroutine(LoadRoomCo(_currentLoadingRoom));
    }

    void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y))
            return;

        RoomData roomData = new RoomData
        {
            Name = name,
            X = x,
            Y = y
        };

        _roomQueue.Enqueue(roomData);
    }

    IEnumerator LoadRoomCo(RoomData room)
    {
        string sceneName = _currentMap + room.Name;        
        AsyncOperation loadScene =  SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!loadScene.isDone)
        {
            yield return null;
        }
    }

    public void RegisterRoom(RoomParent roomParent)
    {
        roomParent.transform.position = new Vector3(
            _currentLoadingRoom.X * roomParent.Width,
            _currentLoadingRoom.Y * roomParent.Height,
            0);

        roomParent.X = _currentLoadingRoom.X;
        roomParent.Y = _currentLoadingRoom.Y;
        roomParent.name = _currentMap + "-" + _currentLoadingRoom.Name + " " + roomParent.X + ", " + roomParent.Y;
        roomParent.transform.parent = transform;

        _isLoadingRoom = false;

        if (_loadedRooms.Count == 0)
        {
            GameCamera.Instance.CurrentRoom = roomParent;
        }

        _loadedRooms.Add(roomParent);
    }

    bool DoesRoomExist(int x, int y)
    {
        return _loadedRooms.Any(room => room.X == x && room.Y == y);
    }

    public void OnPlayerEnterRoom(RoomParent roomParent)
    {
        GameCamera.Instance.CurrentRoom = roomParent;

        roomParent.ActivateRoom();
        LoadAllExits(roomParent);
        

    }

    private void LoadAllExits(RoomParent roomParent)
    {        
        if(!string.IsNullOrEmpty(roomParent.UpExit))
            LoadRoom(roomParent.UpExit, roomParent.X, roomParent.Y + 1);
        if(!string.IsNullOrEmpty(roomParent.DownExit))
            LoadRoom(roomParent.DownExit, roomParent.X, roomParent.Y - 1);
        if (!string.IsNullOrEmpty(roomParent.LeftExit))
            LoadRoom(roomParent.LeftExit, roomParent.X - 1, roomParent.Y);
        if (!string.IsNullOrEmpty(roomParent.RightExit))
            LoadRoom(roomParent.RightExit, roomParent.X + 1, roomParent.Y);                       
    }

    
}
