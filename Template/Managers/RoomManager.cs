using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Managers
{
    public class RoomManager
    {
        private Dictionary<string, Texture2D> _textures;

        public Room CurrentRoom { get; set; }

        public RoomManager(ContentManager content)
        {
            _textures = new Dictionary<string, Texture2D>()
            {
                { "LeftWall", content.Load<Texture2D>("room/tree_wall_left") },
                { "RightWall", content.Load<Texture2D>("room/tree_wall_right") },
                { "Tree", content.Load<Texture2D>("room/pine_tree") },
                { "TreeShadow", content.Load<Texture2D>("room/pine_tree-shadow") },
                { "Stump", content.Load<Texture2D>("room/stump") },
                { "StumpShadow", content.Load<Texture2D>("room/stump-shadow") },
                { "Plant1", content.Load<Texture2D>("room/plant1") },
                { "Plant2", content.Load<Texture2D>("room/plant2") },
                { "Plant-Sheet", content.Load<Texture2D>("room/plant-sheet") },
                { "Rock1", content.Load<Texture2D>("room/rock1") },
                { "Rock1Shadow", content.Load<Texture2D>("room/rock1-shadow") },
                { "Rock2", content.Load<Texture2D>("room/rock2") },
                { "Path1", content.Load<Texture2D>("room/path1") },
                { "Path2", content.Load<Texture2D>("room/path2") },
                { "Path3", content.Load<Texture2D>("room/path3") },
                { "TreeTop1", content.Load<Texture2D>("room/tree_top1") },
                { "TreeTop2", content.Load<Texture2D>("room/tree_top2") },
                { "TreeTop3", content.Load<Texture2D>("room/tree_top3") },
                { "Mud", content.Load<Texture2D>("room/mud") },
                { "Mushroom", content.Load<Texture2D>("room/mushroom") },
                { "WaterEdge", content.Load<Texture2D>("room/water_edge") },
                { "WaterEdge-Sheet", content.Load<Texture2D>("room/water_edge-sheet") },
                { "TelepadBaseSheet", content.Load<Texture2D>("room/telepad_base-sheet") },
                { "TelepadCrystalSheet", content.Load<Texture2D>("room/telepad_crystal-sheet") }
            };
        }

        public Room CreateRoom(Texture2D defaultTex, Vector2 position, Vector2 roomSize)
        {
            CurrentRoom = new Room(_textures, defaultTex, position, roomSize, 96);
            return CurrentRoom;
        }
    }
}
