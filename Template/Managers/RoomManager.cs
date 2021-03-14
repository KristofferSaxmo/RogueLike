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

        private Vector2 _position;

        private Vector2 _roomSize;

        public RoomManager(ContentManager content)
        {
            _textures = new Dictionary<string, Texture2D>()
            {
                { "LeftWall", content.Load<Texture2D>("room/tree_wall_left") },
                { "RightWall", content.Load<Texture2D>("room/tree_wall_right") },
                { "Tree", content.Load<Texture2D>("room/pine_tree") },
                { "Stump", content.Load<Texture2D>("room/stump") },
                { "Plant1", content.Load<Texture2D>("room/plant1") },
                { "Plant2", content.Load<Texture2D>("room/plant2") },
                { "Plant-Sheet", content.Load<Texture2D>("room/plant-sheet") },
                { "Rock1", content.Load<Texture2D>("room/rock1") },
                { "Rock2", content.Load<Texture2D>("room/rock2") },
                { "TreeTop1", content.Load<Texture2D>("room/tree_top1") },
                { "TreeTop2", content.Load<Texture2D>("room/tree_top2") },
                { "TreeTop3", content.Load<Texture2D>("room/tree_top3") },
                { "Mud", content.Load<Texture2D>("room/mud") },
                { "Mushroom", content.Load<Texture2D>("room/mushroom") },
                { "WaterEdge", content.Load<Texture2D>("room/water_edge") },
                { "TelepadBaseSheet", content.Load<Texture2D>("room/telepad_base-sheet") },
                { "TelepadCrystalSheet", content.Load<Texture2D>("room/telepad_crystal-sheet") }
            };
        }

        public Room CreateRoom(Texture2D defaultTex)
        {
            _position = new Vector2(200, -300);

            _roomSize = new Vector2(20, 20);

            return new Room(_textures, defaultTex, _position, _roomSize, 96);
        }
    }
}
