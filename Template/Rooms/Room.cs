﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Sprites;
using RogueLike.Sprites.RoomSprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Rooms
{
    public class Room : Sprite
    {
        private List<Sprite> _sprites = new List<Sprite>();

        private int[,] _map = new int[,]
            {
                {1, 1, 1, 1, 2, 1, 1, 1, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 1 },
                {2, 0, 0, 0, 0, 0, 0, 0, 2 },
                {1, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 1, 1, 1, 2, 1, 1, 1, 1 },
            };
        /// <summary>
        /// 0 = Empty
        /// 1 = Wall
        /// 2 = Possible Door
        /// </summary>

        private Vector2 _position;

        private List<Texture2D> _textures;
        /// <summary>
        /// _textures[0] - tree_wall
        /// _textures[1] - pine_tree
        /// _textures[2] - stump
        /// _textures[3] - plant1
        /// _textures[4] - plant2
        /// _textures[5] - plant-sheet
        /// _textures[6] - rock1
        /// _textures[7] - rock2
        /// _textures[8] - tree_top1
        /// _textures[9] - tree_top2
        /// _textures[10] - tree_top3
        /// _textures[11] - mud
        /// _textures[12] - mushroom
        /// _textures[13] - water_ledge
        /// _textures[14] - telepad_base-sheet
        /// _textures[15] - telepad_crystal-sheet
        /// </summary>

        public Room(List<Texture2D> textures, Vector2 position)
        {
            _textures = textures;
            _position = position;

            for (int i = 0; i < _map.GetLength(0); i++) // For each row
            {
                for (int j = 0; j < _map.GetLength(1); j++) // For each column
                {
                    if ((int)_map.GetValue(i, j) == 1) // If it's a wall
                    {
                        _sprites.Add(new Wall(_textures[0]) // Add wall
                        {
                            Position = new Vector2(_position.X + i * 60, _position.Y + j * 60),
                            Layer = 0.1f,
                        });
                    }
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites)
            {
                sprite.Update(gameTime);
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var sprite in _sprites)
            {
                sprite.Draw(gameTime, spriteBatch);
            }
        }
    }
}