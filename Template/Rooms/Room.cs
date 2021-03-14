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
        private Random _random = new Random();

        private Color _grass = new Color(36, 73, 67);

        private int[,] _map;

        private int _tileSize;
        /// <summary>
        /// 0 = Empty
        /// 1 = Wall
        /// 2 = Possible Door
        /// </summary>

        private Dictionary<string, Texture2D> _textures;

        public Room(Dictionary<string, Texture2D> defaultTex, Texture2D texture, Vector2 position, Vector2 roomSize, int tileSize)
        {
            _textures = defaultTex;
            _texture = texture;
            Position = position;
            _map = new int[(int)roomSize.X, (int)roomSize.Y];
            _tileSize = tileSize;

            #region Create Room
            for (int i = 0; i < (int)roomSize.X; i++) // Tile X
            {
                for (int j = 0; j < (int)roomSize.Y; j++) // Tile Y
                {
                    if (i == 0 || j == 0 || i == (int)roomSize.X - 1 || j == (int)roomSize.Y - 1)
                        _map[i, j] = 1;
                }
            }
            #endregion

            #region Fill Room
            for (int x = 0; x < (int)roomSize.X; x++) // Tile X
            {
                for (int y = 0; y < (int)roomSize.Y; y++) // Tile Y
                {
                    if (_map[x, y] == 1) // If it's a wall
                    {
                        if (x == 0)
                        {
                            Children.Add(new Wall(_textures["LeftWall"])
                            {
                                Position = new Vector2(RandomXPos(x, 0, 0), RandomYPos(y, 0, 0)),
                                Parent = this
                            });
                        }
                        else if (x == roomSize.Y - 1)
                        {
                            Children.Add(new Wall(_textures["RightWall"])
                            {
                                Position = new Vector2(RandomXPos(x, 0, 0), RandomYPos(y, 0, 0)),
                                Parent = this
                            });
                        }
                        if (y == 0 && x != roomSize.X - 1)
                        {
                            Children.Add(new Wall(_textures["RightWall"])
                            {
                                Position = new Vector2(RandomXPos(x, 0, 32), RandomYPos(y - 1, 20, 100)),
                                Parent = this
                            });
                            Children.Add(new Wall(_textures["RightWall"])
                            {
                                Position = new Vector2(RandomXPos(x, 32, 64), RandomYPos(y - 1, 20, 100)),
                                Parent = this
                            });
                            Children.Add(new Wall(_textures["RightWall"])
                            {
                                Position = new Vector2(RandomXPos(x, 64, 92), RandomYPos(y - 1, 20, 100)),
                                Parent = this
                            });
                        }
                        else if (y == roomSize.X - 1)
                        {
                            Children.Add(new WaterEdge(_textures["WaterEdge"])
                            {
                                Position = RandomPosition(x, y + 1, 0),
                                Parent = this
                            });
                        }
                    }

                    else if (_map[x, y] == 0) // If it's an empty space
                    {
                        switch (_random.Next(20))
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
                                Children.Add(new Tree(_textures["Tree"])
                                {
                                    Position = RandomPosition(x, y, 50),
                                    Parent = this
                                });
                                break; // Tree 20%

                            case 8:
                            case 9:
                            case 10:
                            case 11:
                                Children.Add(new Plant1(_textures["Plant1"])
                                {
                                    Position = RandomPosition(x, y, 50),
                                    Parent = this
                                });
                                break; // Plant1 20%

                            case 12:
                            case 13:
                            case 14:
                            case 15:
                                Children.Add(new Rock1(_textures["Rock1"])
                                {
                                    Position = RandomPosition(x, y, 50),
                                    Parent = this
                                });
                                break; // Rock1 20%

                            case 16:
                            case 17:
                            case 18:
                            case 19:
                                Children.Add(new Plant2(_textures["Plant2"])
                                {
                                    Position = RandomPosition(x, y, 50),
                                    Parent = this
                                });
                                break; // Plant2 20%

                        }
                    }
                }
            }
            #endregion
        }
        private Vector2 RandomPosition(int x, int y, int maxRandom)
        {
            return new Vector2(Position.X + x * _tileSize + _random.Next(maxRandom), Position.Y + y * _tileSize + _random.Next(maxRandom));
        }
        private float RandomXPos(int x, int minRandom, int maxRandom)
        {
            return Position.X + x * _tileSize + _random.Next(minRandom, maxRandom);
        }
        private float RandomYPos(int y, int minRandom, int maxRandom)
        {
            return Position.Y + y * _tileSize + _random.Next(minRandom, maxRandom);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle test = new Rectangle((int)Position.X, (int)Position.Y, (_map.GetLength(0) - 1) * _tileSize, _map.GetLength(1) * _tileSize);
            spriteBatch.Draw(_texture, test, _grass);
        }
    }
}