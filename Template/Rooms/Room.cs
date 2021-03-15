using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Models;
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

        private Color _water = new Color(61, 61, 126);

        private Rectangle _grassRec, _grassRec2, _waterRec;

        private bool _isWater;

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

            if (_random.Next(2) == 0) // 50% chance for water
                _isWater = true;
            else
                _isWater = false;

            _grassRec = new Rectangle(
                (int)Position.X,
                (int)Position.Y - 20,
                (_map.GetLength(0) - 1) * _tileSize,
                _map.GetLength(1) * _tileSize + 1);

            _waterRec = new Rectangle(
                (int)Position.X - 10 * tileSize,
                (int)Position.Y + (int)roomSize.Y * tileSize,
                ((int)roomSize.X - 1) * tileSize + 22 * tileSize,
                1000);

            _grassRec2 = new Rectangle(
                    (int)Position.X - 10 * tileSize,
                    (int)Position.Y + (int)roomSize.Y * tileSize - 70,
                    ((int)roomSize.X - 1) * tileSize + 22 * tileSize,
                    1000);


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

            #region Water Room
            if (_isWater)
            {
                for (int x = -10; x < roomSize.X + 10; x++)
                {
                    // Water
                    Children.Add(new WaterEdge(_textures["WaterEdge"])
                    {
                        Position = new Vector2(RandomXPos(x, 0, 0), RandomYPos((int)roomSize.Y - 1, 144, 144)),
                        Parent = this
                    });
                    // Left water walls
                    if (x < roomSize.X / 2 && x < 0)
                    {
                        Children.Add(new Wall(_textures["LeftWall"])
                        {
                            Position = new Vector2(RandomXPos(x, 0, 0), Position.Y + ((int)roomSize.Y - 1) * _tileSize - 5),
                            Parent = this
                        });
                        Children.Add(new Wall(_textures["LeftWall"])
                        {
                            Position = new Vector2(RandomXPos(x, 48, 48), Position.Y + ((int)roomSize.Y - 1) * _tileSize - 25),
                            Parent = this
                        });
                    }
                    // Right water walls
                    else if (x >= roomSize.X - 1)
                    {
                        Children.Add(new Wall(_textures["RightWall"])
                        {
                            Position = new Vector2(RandomXPos(x, 0, 0), Position.Y + ((int)roomSize.Y - 1) * _tileSize - 5),
                            Parent = this
                        });
                        Children.Add(new Wall(_textures["RightWall"])
                        {
                            Position = new Vector2(RandomXPos(x, 48, 48), Position.Y + ((int)roomSize.Y - 1) * _tileSize - 25),
                            Parent = this
                        });
                    }
                }
            }
            #endregion

            for (int x = 0; x < (int)roomSize.X; x++) // Tile X
            {
                for (int y = 0; y < (int)roomSize.Y; y++) // Tile Y
                {
                    #region Create Walls
                    if (_map[x, y] == 1) // If it's a wall
                    {
                        // Left Walls
                        if (x == 0)
                        {
                            Children.Add(new Wall(_textures["LeftWall"])
                            {
                                Position = new Vector2(RandomXPos(x, 0, 0), RandomYPos(y, 0, 0)),
                                Parent = this
                            });
                            Children.Add(new Wall(_textures["LeftWall"])
                            {
                                Position = new Vector2(RandomXPos(x, 30, 30), Position.Y + y * _tileSize - 48),
                                Parent = this
                            });
                        }
                        // Right Walls
                        else if (x == roomSize.X - 1)
                        {
                            Children.Add(new Wall(_textures["RightWall"])
                            {
                                Position = new Vector2(RandomXPos(x, 30, 30), RandomYPos(y, 0, 0)),
                                Parent = this
                            });
                            Children.Add(new Wall(_textures["RightWall"])
                            {
                                Position = new Vector2(RandomXPos(x, 0, 0), Position.Y + y * _tileSize - 48),
                                Parent = this
                            });
                        }
                        // Top Walls
                        if (y == 0 && x != roomSize.X - 1)
                        {
                            if (x < roomSize.X / 2)
                            {
                                Children.Add(new Wall(_textures["LeftWall"])
                                {
                                    Position = new Vector2(RandomXPos(x, 0, 0), RandomYPos(y - 1, 50, 50)),
                                    Parent = this
                                });
                                Children.Add(new Wall(_textures["LeftWall"])
                                {
                                    Position = new Vector2(RandomXPos(x, 48, 48), RandomYPos(y - 1, 30, 30)),
                                    Parent = this
                                });
                            }
                            else
                            {
                                Children.Add(new Wall(_textures["RightWall"])
                                {
                                    Position = new Vector2(RandomXPos(x, 0, 0), RandomYPos(y - 1, 50, 50)),
                                    Parent = this
                                });
                                Children.Add(new Wall(_textures["RightWall"])
                                {
                                    Position = new Vector2(RandomXPos(x, 48, 48), RandomYPos(y - 1, 30, 30)),
                                    Parent = this
                                });
                            }
                        }
                        // Bottom Walls
                        else if (y == roomSize.Y - 1 && !_isWater)
                        {
                            if (x < roomSize.X / 2)
                            {
                                Children.Add(new Wall(_textures["LeftWall"])
                                {
                                    Position = new Vector2(RandomXPos(x, 0, 0), RandomYPos(y, 48, 48)),
                                    Parent = this,
                                    Color = Color.Black
                                });
                                Children.Add(new Wall(_textures["LeftWall"])
                                {
                                    Position = new Vector2(RandomXPos(x, 48, 48), RandomYPos(y, 18, 18)),
                                    Parent = this,
                                    Color = Color.Black
                                });
                            }
                            else
                            {
                                Children.Add(new Wall(_textures["RightWall"])
                                {
                                    Position = new Vector2(RandomXPos(x, 0, 0), RandomYPos(y, 48, 48)),
                                    Parent = this,
                                    Color = Color.Black
                                });
                                Children.Add(new Wall(_textures["RightWall"])
                                {
                                    Position = new Vector2(RandomXPos(x, 48, 48), RandomYPos(y, 18, 18)),
                                    Parent = this,
                                    Color = Color.Black
                                });
                            }
                        }
                    }
                    #endregion

                    #region Create Other
                    else if (_map[x, y] == 0)
                    {
                        if (_random.Next(100) <= 30) // 30%
                            Children.Add(new Tree(_textures["Tree"], _textures["TreeShadow"])
                            {
                                Position = RandomPosition(x, y, 50),
                                Parent = this
                            });

                        if (_random.Next(100) <= 10) // 10%
                            Children.Add(new Plant1(_textures["Plant1"])
                            {
                                Position = RandomPosition(x, y, 50),
                                Parent = this
                            });

                        if (_random.Next(100) <= 10) // 10%
                            Children.Add(new DefaultSprite(_textures["Plant2"])
                            {
                                Position = RandomPosition(x, y, 50),
                                Parent = this
                            });

                        if (_random.Next(100) <= 2) // 2%
                            Children.Add(new PlantAnimation(new Dictionary<string, Animation>() { { "Animation", new Animation(_textures["Plant-Sheet"], 4, 0.5f) } })
                            {
                                Position = RandomPosition(x, y, 50),
                                Parent = this
                            });

                        if (_random.Next(100) <= 10) // 10%
                            Children.Add(new Rock1(_textures["Rock1"], _textures["Rock1Shadow"])
                            {
                                Position = RandomPosition(x, y, 50),
                                Parent = this
                            });

                        if (_random.Next(100) <= 10) // 10%
                            Children.Add(new DefaultSprite(_textures["Rock2"])
                            {
                                Position = RandomPosition(x, y, 50),
                                Parent = this
                            });

                        if (_random.Next(100) <= 5) // 5%
                            Children.Add(new DefaultSprite(_textures["Mushroom"])
                            {
                                Position = RandomPosition(x, y, 50),
                                Parent = this
                            });

                        if (_random.Next(100) <= 1) // 1%
                            Children.Add(new DefaultSprite(_textures["Mud"])
                            {
                                Position = RandomPosition(x, y, 50),
                                Parent = this
                            });
                    }
                    #endregion
                }
            }

            #region Outside Tree Tops
            if (_isWater)
            {
                for (int x = -10; x < (int)roomSize.X + 10; x++) // Tile X
                {
                    for (int y = -10; y < (int)roomSize.Y + 10; y++) // Tile Y
                    {
                        if (((x < 0 || x > (int)roomSize.X - 1) && y < (int)roomSize.Y - 2) ||
                            (x >= 0 && x < (int)roomSize.X) && y < -1)
                        {
                            if (_random.Next(100) <= 50)
                            {
                                int random = _random.Next(3);

                                if (random == 0)
                                    Children.Add(new DefaultSprite(_textures["TreeTop1"])
                                    {
                                        Position = RandomPosition(x, y, 48),
                                        Parent = this
                                    });
                                else if (random == 1)
                                    Children.Add(new DefaultSprite(_textures["TreeTop2"])
                                    {
                                        Position = RandomPosition(x, y, 48),
                                        Parent = this
                                    });
                                else
                                    Children.Add(new DefaultSprite(_textures["TreeTop3"])
                                    {
                                        Position = RandomPosition(x, y, 48),
                                        Parent = this
                                    });
                            }
                        }
                    }
                }
            }
            else
            {
                for (int x = -10; x < (int)roomSize.X + 10; x++) // Tile X
                {
                    for (int y = -10; y < (int)roomSize.Y + 10; y++) // Tile Y
                    {
                        if (x < 0 || x > (int)roomSize.X - 1 ||
                        y < -1 || y > (int)roomSize.Y - 1)
                        {
                            if (_random.Next(100) <= 50)
                            {
                                int random = _random.Next(3);

                                if (random == 0)
                                    Children.Add(new DefaultSprite(_textures["TreeTop1"])
                                    {
                                        Position = RandomPosition(x, y, 48),
                                        Parent = this,
                                        LayerOrigin = 50
                                    });
                                else if (random == 1)
                                    Children.Add(new DefaultSprite(_textures["TreeTop2"])
                                    {
                                        Position = RandomPosition(x, y, 48),
                                        Parent = this,
                                        LayerOrigin = 50
                                    });
                                else
                                    Children.Add(new DefaultSprite(_textures["TreeTop3"])
                                    {
                                        Position = RandomPosition(x, y, 48),
                                        Parent = this,
                                        LayerOrigin = 50
                                    });
                            }
                        }
                    }
                }
            }
            #endregion

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
            spriteBatch.Draw(_texture, _grassRec, _grass);
            if (_isWater)
            {
                spriteBatch.Draw(_texture, _grassRec2, _grass);
                spriteBatch.Draw(_texture, _waterRec, _water);
            }
        }
    }
}