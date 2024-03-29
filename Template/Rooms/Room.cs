﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Managers;
using RogueLike.Models;
using RogueLike.Sprites;
using RogueLike.Sprites.RoomSprites;
using System.Collections.Generic;

namespace RogueLike.Rooms
{
    public class Room : Sprite
    {
        private readonly Dictionary<string, Texture2D> _textures;
        private readonly EnemyManager _enemyManager;

        public static Color Grass = new Color(36, 73, 67);
        public Vector2 RoomSize { get; }
        public Rectangle GrassRec { get; }
        public Rectangle GrassRec2 { get; }
        public int TileSize { get; }
        public bool IsWater { get; }
        public Rectangle Area { get; }

        private readonly int[,] _map;
        /// <summary>
        /// 0 = Empty
        /// 1 = Wall
        /// 2 = Possible Door
        /// </summary>

        public Room(Dictionary<string, Texture2D> textures, Vector2 position, Vector2 roomSize, EnemyManager enemyManager)
        {
            _enemyManager = enemyManager;
            _textures = textures;
            Texture = _textures["LeftWall"]; // Room måste ha en textur även om den inte ritas
            Position = position;
            _map = new int[(int)roomSize.X, (int)roomSize.Y];
            RoomSize = roomSize;
            TileSize = 96;
            Area = new Rectangle((int)position.X, (int)position.Y, (int)roomSize.X * TileSize, (int)roomSize.Y * TileSize);

            if (Game1.Random.Next(2) == 0) // 50% chance for water
                IsWater = true;
            else
                IsWater = false;

            GrassRec = new Rectangle((int)Position.X,
                                     (int)Position.Y - 20,
                                     (_map.GetLength(0) - 1) * TileSize,
                                     _map.GetLength(1) * TileSize + 1);

            GrassRec2 = new Rectangle((int)Position.X - 10 * TileSize,
                                      (int)Position.Y + (int)roomSize.Y * TileSize - 70,
                                      ((int)roomSize.X - 1) * TileSize + 22 * TileSize,
                                      1000);

            CreateRoom(roomSize);

            FillRoom(roomSize);
        }

        private void CreateRoom(Vector2 roomSize)
        {
            for (int i = 0; i < (int)roomSize.X; i++) // Tile X
            {
                for (int j = 0; j < (int)roomSize.Y; j++) // Tile Y
                {
                    if (i == 0 || j == 0 || i == (int)roomSize.X - 1 || j == (int)roomSize.Y - 1)
                        _map[i, j] = 1;
                }
            }
        }

        private void FillRoom(Vector2 roomSize)
        {
            if (IsWater)
            {
                CreateWater(roomSize);
            }

            for (int x = 0; x < (int)roomSize.X; x++) // Tile X
            {
                for (int y = 0; y < (int)roomSize.Y; y++) // Tile Y
                {
                    if (_map[x, y] == 1) // If it's a wall
                    {
                        CreateWalls(roomSize, x, y);
                    }

                    else if (_map[x, y] == 0)
                    {
                        CreateOther(x, y);
                    }
                }
            }

            CreateTreeTops(roomSize);
        }

        private void CreateWater(Vector2 roomSize)
        {
            for (int x = -10; x < roomSize.X + 10; x++)
            {
                for (int y = (int)roomSize.Y; y < (int)roomSize.Y + 5; y++)
                {
                    Children.Add(new AnimatedDefaultSprite(new Dictionary<string, Animation>() { { "Animation", new Animation(_textures["Water-Sheet"], 32, 0.1f) } })
                    {
                        Position = new Vector2(GetRandomXPos(x, 0, 0), GetRandomYPos(y, 144, 144)),
                        Parent = this
                    });
                }
                // Water
                Children.Add(new WaterEdge(new Dictionary<string, Animation>() { { "Animation", new Animation(_textures["WaterEdge-Sheet"], 32, 0.1f) } })
                {
                    Position = new Vector2(GetRandomXPos(x, 0, 0), GetRandomYPos((int)roomSize.Y - 1, 144, 144)),
                    Parent = this
                });
                // Left water walls
                if (x < roomSize.X / 2 && x < 0)
                {
                    Children.Add(new Wall(_textures["LeftWall"])
                    {
                        Position = new Vector2(GetRandomXPos(x, 0, 0), Position.Y + ((int)roomSize.Y - 1) * TileSize - 5),
                        Parent = this
                    });
                    Children.Add(new Wall(_textures["LeftWall"])
                    {
                        Position = new Vector2(GetRandomXPos(x, 48, 48), Position.Y + ((int)roomSize.Y - 1) * TileSize - 25),
                        Parent = this
                    });
                }
                // Right water walls
                else if (x >= roomSize.X - 1)
                {
                    Children.Add(new Wall(_textures["RightWall"])
                    {
                        Position = new Vector2(GetRandomXPos(x, 0, 0), Position.Y + ((int)roomSize.Y - 1) * TileSize - 5),
                        Parent = this
                    });
                    Children.Add(new Wall(_textures["RightWall"])
                    {
                        Position = new Vector2(GetRandomXPos(x, 48, 48), Position.Y + ((int)roomSize.Y - 1) * TileSize - 25),
                        Parent = this
                    });
                }
            }
        }

        private void CreateWalls(Vector2 roomSize, int x, int y)
        {
            // Left Walls
            if (x == 0)
            {
                Children.Add(new Wall(_textures["LeftWall"])
                {
                    Position = new Vector2(GetRandomXPos(x, 0, 0), GetRandomYPos(y, 0, 0)),
                    Parent = this
                });
                Children.Add(new Wall(_textures["LeftWall"])
                {
                    Position = new Vector2(GetRandomXPos(x, 30, 30), Position.Y + y * TileSize - 48),
                    Parent = this
                });
            }
            // Right Walls
            else if (x == roomSize.X - 1)
            {
                Children.Add(new Wall(_textures["RightWall"])
                {
                    Position = new Vector2(GetRandomXPos(x, 30, 30), GetRandomYPos(y, 0, 0)),
                    Parent = this
                });
                Children.Add(new Wall(_textures["RightWall"])
                {
                    Position = new Vector2(GetRandomXPos(x, 0, 0), Position.Y + y * TileSize - 48),
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
                        Position = new Vector2(GetRandomXPos(x, 0, 0), GetRandomYPos(y - 1, 50, 50)),
                        Parent = this
                    });
                    Children.Add(new Wall(_textures["LeftWall"])
                    {
                        Position = new Vector2(GetRandomXPos(x, 48, 48), GetRandomYPos(y - 1, 30, 30)),
                        Parent = this
                    });
                }
                else
                {
                    Children.Add(new Wall(_textures["RightWall"])
                    {
                        Position = new Vector2(GetRandomXPos(x, 0, 0), GetRandomYPos(y - 1, 50, 50)),
                        Parent = this
                    });
                    Children.Add(new Wall(_textures["RightWall"])
                    {
                        Position = new Vector2(GetRandomXPos(x, 48, 48), GetRandomYPos(y - 1, 30, 30)),
                        Parent = this
                    });
                }
            }
            // Bottom Walls
            else if (y == roomSize.Y - 1 && !IsWater)
            {
                if (x < roomSize.X / 2)
                {
                    Children.Add(new Wall(_textures["LeftWall"])
                    {
                        Position = new Vector2(GetRandomXPos(x, 0, 0), GetRandomYPos(y, 48, 48)),
                        Parent = this,
                        Color = Color.Black
                    });
                    Children.Add(new Wall(_textures["LeftWall"])
                    {
                        Position = new Vector2(GetRandomXPos(x, 48, 48), GetRandomYPos(y, 18, 18)),
                        Parent = this,
                        Color = Color.Black
                    });
                }
                else
                {
                    Children.Add(new Wall(_textures["RightWall"])
                    {
                        Position = new Vector2(GetRandomXPos(x, 0, 0), GetRandomYPos(y, 48, 48)),
                        Parent = this,
                        Color = Color.Black
                    });
                    Children.Add(new Wall(_textures["RightWall"])
                    {
                        Position = new Vector2(GetRandomXPos(x, 48, 48), GetRandomYPos(y, 18, 18)),
                        Parent = this,
                        Color = Color.Black
                    });
                }
            }
        }

        private void CreateOther(int x, int y)
        {
            if (Game1.Random.Next(100) < 1) // 1%
                Children.Add(_enemyManager.CreateEnemy(GetRandomPosition(x, y, 50), this));

            if (Game1.Random.Next(100) < 20) // 20%
                Children.Add(new Tree(_textures["Tree"], _textures["TreeShadow"])
                {
                    Position = GetRandomPosition(x, y, 50),
                    Parent = this
                });

            if (Game1.Random.Next(100) < 15) // 15%
                Children.Add(new DefaultSprite(_textures["Plant1"])
                {
                    LayerOrigin = 17,
                    Position = GetRandomPosition(x, y, 50),
                    Parent = this
                });

            if (Game1.Random.Next(100) < 30) // 30%
                Children.Add(new DefaultSprite(_textures["Plant2"])
                {
                    Position = GetRandomPosition(x, y, 50),
                    Parent = this
                });

            if (Game1.Random.Next(100) < 15) // 15%
                Children.Add(new PlantAnimation(new Dictionary<string, Animation>() { { "Animation", new Animation(_textures["Plant-Sheet"], 4, 0.5f) } })
                {
                    Position = GetRandomPosition(x, y, 50),
                    Parent = this
                });

            if (Game1.Random.Next(100) < 15) // 15%
                Children.Add(new Rock1(_textures["Rock1"], _textures["Rock1Shadow"])
                {
                    Position = GetRandomPosition(x, y, 50),
                    Parent = this
                });

            if (Game1.Random.Next(100) < 30) // 30%
                Children.Add(new DefaultSprite(_textures["Rock2"])
                {
                    Position = GetRandomPosition(x, y, 50),
                    Parent = this
                });

            if (Game1.Random.Next(100) < 10) // 10%
                Children.Add(new DefaultSprite(_textures["Mushroom"])
                {
                    Position = GetRandomPosition(x, y, 50),
                    Parent = this
                });

            if (Game1.Random.Next(100) < 2) // 2%
                Children.Add(new DefaultSprite(_textures["Mud"])
                {
                    Position = GetRandomPosition(x, y, 50),
                    Parent = this
                });
        }

        private void CreateTreeTops(Vector2 roomSize)
        {
            if (IsWater)
            {
                for (int x = -10; x < (int)roomSize.X + 10; x++) // Tile X
                {
                    for (int y = -10; y < (int)roomSize.Y + 10; y++) // Tile Y
                    {
                        if (((x < 0 || x > (int)roomSize.X - 1) && y < (int)roomSize.Y - 2) ||
                            (x >= 0 && x < (int)roomSize.X) && y < -1)
                        {
                            if (Game1.Random.Next(100) <= 50)
                            {
                                GetRandomTreeTops(x, y);
                            }
                        }
                    }
                }
                return;
            }

            for (int x = -10; x < (int)roomSize.X + 10; x++) // Tile X
            {
                for (int y = -10; y < (int)roomSize.Y + 10; y++) // Tile Y
                {
                    if (x < 0 || x > (int)roomSize.X - 1 ||
                    y < -1 || y > (int)roomSize.Y - 1)
                    {
                        if (Game1.Random.Next(100) <= 50)
                        {
                            GetRandomTreeTops(x, y);
                        }
                    }
                }
            }
        }

        private void GetRandomTreeTops(int x, int y)
        {
            int random = Game1.Random.Next(3);

            switch (random)
            {
                case 0:
                    Children.Add(new DefaultSprite(_textures["TreeTop1"])
                    {
                        Position = GetRandomPosition(x, y, 48),
                        Parent = this,
                        LayerOrigin = 50
                    });
                    break;
                case 1:
                    Children.Add(new DefaultSprite(_textures["TreeTop2"])
                    {
                        Position = GetRandomPosition(x, y, 48),
                        Parent = this,
                        LayerOrigin = 50
                    });
                    break;
                case 2:
                    Children.Add(new DefaultSprite(_textures["TreeTop3"])
                    {
                        Position = GetRandomPosition(x, y, 48),
                        Parent = this,
                        LayerOrigin = 50
                    });
                    break;
            }
        }

        private Vector2 GetRandomPosition(int x, int y, int maxRandom)
        {
            return new Vector2(Position.X + x * TileSize + Game1.Random.Next(maxRandom), Position.Y + y * TileSize + Game1.Random.Next(maxRandom));
        }
        private float GetRandomXPos(int x, int minRandom, int maxRandom)
        {
            return Position.X + x * TileSize + Game1.Random.Next(minRandom, maxRandom);
        }
        private float GetRandomYPos(int y, int minRandom, int maxRandom)
        {
            return Position.Y + y * TileSize + Game1.Random.Next(minRandom, maxRandom);
        }
    }
}