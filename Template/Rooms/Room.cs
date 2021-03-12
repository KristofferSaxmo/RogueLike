using Microsoft.Xna.Framework;
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

        private Random random = new Random();

        private Color Grass = new Color(36, 73, 67);

        private int[,] _map;
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

        public Room(List<Texture2D> textures, Vector2 position, Vector2 roomSize)
        {
            _map = new int[(int)roomSize.X, (int)roomSize.Y];
            _textures = textures;
            _position = position;

            for (int i = 0; i < (int)roomSize.X; i++) // Tile X
            {
                for (int j = 0; j < (int)roomSize.Y; j++) // Tile Y
                {
                    if (i == 0 || j == 0 || i == (int)roomSize.X - 1 || j == (int)roomSize.Y - 1)
                        _map[i, j] = 1;
                }
            }





            for (int i = 0; i < _map.GetLength(0); i++) // Tile X
            {
                for (int j = 0; j < _map.GetLength(1); j++) // Tile Y
                {
                    if ((int)_map.GetValue(i, j) == 1) // If it's a wall
                    {
                        _sprites.Add(new Wall(_textures[0]) // Add wall
                        {
                            Position = RandomPosition(i, j, 0),
                            Layer = 0.1f,
                        });
                    }

                    if ((int)_map.GetValue(i, j) == 2) // If it's water
                    {
                        _sprites.Add(new Wall(_textures[13]) // Add water
                        {
                            Position = RandomPosition(i, j, 0),
                            Layer = 0.1f,
                        });
                    }

                    else if ((int) _map.GetValue(i, j) == 0) // If it's an empty space
                    {
                        switch (random.Next(20))
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                                break; // Empty 20%

                            case 4:
                            case 5:
                            case 6:
                            case 7:
                                _sprites.Add(new Tree(_textures[1])
                                {
                                    Position = RandomPosition(i, j, 50),
                                    Layer = 0.1f,
                                });
                                break; // Tree 20%

                            case 8:
                            case 9:
                            case 10:
                            case 11:
                                _sprites.Add(new Plant1(_textures[3])
                                {
                                    Position = RandomPosition(i, j, 50),
                                    Layer = 0.1f,
                                });
                                break; // Plant1 20%

                            case 12:
                            case 13:
                            case 14:
                            case 15:
                                _sprites.Add(new Rock1(_textures[6])
                                {
                                    Position = RandomPosition(i, j, 50),
                                    Layer = 0.1f,
                                });
                                break; // Rock1 20%

                            case 16:
                            case 17:
                            case 18:
                            case 19:
                                _sprites.Add(new Plant2(_textures[4])
                                {
                                    Position = RandomPosition(i, j, 50),
                                    Layer = 0.1f,
                                });
                                break; // Plant2 20%

                        }
                    }
                }
            }
        }
        public Vector2 RandomPosition(int i, int j, int maxRandom)
        {
            return new Vector2(_position.X + i * 96 + random.Next(maxRandom), _position.Y + j * 96 + random.Next(maxRandom));
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