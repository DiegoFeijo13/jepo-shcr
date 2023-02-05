using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Mechanics.RDG
{
    internal static class RoomManager
    {
        private static int _roomMask = LayerMask.GetMask("Room");

        internal static List<Room> Rooms { get; private set; }
        internal static Room GetRoom(Vector3 position)
        {
            var roomDetection = Physics2D.OverlapCircle(position, 1, _roomMask);
            if (roomDetection == null)
                return null;

            return roomDetection.gameObject.GetComponent<Room>();
        }

        internal static RoomExits GetRoomExits(Vector3 currentPosition, int stepSize, bool isDeadEnd)
        {
            bool needsTopExit = false;
            Vector2 position = new(currentPosition.x, currentPosition.y + stepSize);
            var room = RoomManager.GetRoom(position);
            needsTopExit = room != null && room.GetRoomExits().HasBottomExit();

            bool needsRightExit = false;
            position = new(currentPosition.x + stepSize, currentPosition.y);
            room = RoomManager.GetRoom(position);
            needsRightExit = room != null && room.GetRoomExits().HasLeftExit();

            bool needsBottomExit = false;
            position = new(currentPosition.x, currentPosition.y - stepSize);
            room = RoomManager.GetRoom(position);
            needsBottomExit = room != null && room.GetRoomExits().HasTopExit();

            bool needsLeftExit = false;
            position = new(currentPosition.x - stepSize, currentPosition.y);
            room = RoomManager.GetRoom(position);
            needsLeftExit = room != null && room.GetRoomExits().HasRightExit();

            var exits = RoomExitsExtension.GetPossibleExits(needsTopExit, needsRightExit, needsBottomExit, needsLeftExit, !isDeadEnd);

            if (isDeadEnd)
            {
                if (!needsTopExit)
                    exits.RemoveAll(e => e.HasTopExit());
                if (!needsRightExit)
                    exits.RemoveAll(e => e.HasRightExit());
                if (!needsBottomExit)
                    exits.RemoveAll(e => e.HasBottomExit());
                if (!needsLeftExit)
                    exits.RemoveAll(e => e.HasLeftExit());
            }

            if (exits.Count == 0)
                return RoomExits.None;

            return exits[Random.Range(0, exits.Count)];
        }
    }
}
