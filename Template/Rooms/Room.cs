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

        private Dictionary<string, Texture2D> _textures;

        public Room(Dictionary<string, Texture2D> textures, Vector2 position, Vector2 roomSize)
        {
            _textures = textures;
            Position = position;
            _map = new int[(int)roomSize.X, (int)roomSize.Y];

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
                        _sprites.Add(new Wall(_textures["Wall"]) // Add wall
                        {
                            Position = RandomPosition(i, j, 0),
                        });
                    }

                    if ((int)_map.GetValue(i, j) == 2) // If it's water
                    {
                        _sprites.Add(new Wall(_textures["WaterEdge"]) // Add water
                        {
                            Position = RandomPosition(i, j, 0),
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
                                _sprites.Add(new Tree(_textures["Tree"])
                                {
                                    Position = RandomPosition(i, j, 50),
                                });
                                break; // Tree 20%

                            case 8:
                            case 9:
                            case 10:
                            case 11:
                                _sprites.Add(new Plant1(_textures["Plant1"])
                                {
                                    Position = RandomPosition(i, j, 50),
                                });
                                break; // Plant1 20%

                            case 12:
                            case 13:
                            case 14:
                            case 15:
                                _sprites.Add(new Rock1(_textures["Rock1"])
                                {
                                    Position = RandomPosition(i, j, 50),
                                });
                                break; // Rock1 20%

                            case 16:
                            case 17:
                            case 18:
                            case 19:
                                _sprites.Add(new Plant2(_textures["Plant2"])
                                {
                                    Position = RandomPosition(i, j, 50),
                                });
                                break; // Plant2 20%

                        }
                    }
                }
            }
        }
        public Vector2 RandomPosition(int i, int j, int maxRandom)
        {
            return new Vector2(Position.X + i * 96 + random.Next(maxRandom), Position.Y + j * 96 + random.Next(maxRandom));
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