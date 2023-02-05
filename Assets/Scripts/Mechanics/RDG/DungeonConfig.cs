using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Mechanics.RDG
{
    public class DungeonConfig
    {
        public int RoomRadius { get; set; }
        public Tile[] FloorTiles { get; set; }
        public Tile TopWallTile { get; set; }
        public Tile RightWallTile { get; set; }
        public Tile BotWallTile { get; set; }
        public Tile LeftWallTile { get; set; }

        public int StepSize() => (RoomRadius * 2) + 1;
    }
}
