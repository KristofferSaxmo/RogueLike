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
        private List<Texture2D> _textures;

        private Vector2 _position;

        private Vector2 _roomSize;

        public RoomManager(ContentManager content)
        {
            _textures = new List<Texture2D>()
            {
                content.Load<Texture2D>("tree_wall"),
                content.Load<Texture2D>("pine_tree"),
                content.Load<Texture2D>("stump"),
                content.Load<Texture2D>("plant1"),
                content.Load<Texture2D>("plant2"),
                content.Load<Texture2D>("plant-sheet"),
                content.Load<Texture2D>("rock1"),
                content.Load<Texture2D>("rock2"),
                content.Load<Texture2D>("tree_top1"),
                content.Load<Texture2D>("tree_top2"),
                content.Load<Texture2D>("tree_top3"),
                content.Load<Texture2D>("mud"),
                content.Load<Texture2D>("mushroom"),
                content.Load<Texture2D>("water_ledge"),
                content.Load<Texture2D>("telepad_base-sheet"),
                content.Load<Texture2D>("telepad_crystal-sheet"),
            };
        }

        public Room CreateRoom()
        {
            _position = new Vector2(500, 200);

            _roomSize = new Vector2(20, 20);

            return new Room(_textures, _position, _roomSize);
        }
    }
}
