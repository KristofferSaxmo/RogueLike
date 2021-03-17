using Microsoft.Xna.Framework;
using RogueLike.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    public class Quadtree
    {
        private int _maxObjects = 10;
        private int _maxLevels = 5;

        private int _level;
        private List<Sprite> _sprites;
        private Rectangle _bounds;
        private Quadtree[] _nodes;

        public Quadtree(int pLevel, Rectangle pBounds)
        {
            _level = pLevel;
            _bounds = pBounds;
            _sprites = new List<Sprite>();
            _nodes = new Quadtree[4];
        }

        public void Clear() // Clears the quadtree
        {
            _sprites.Clear();

            for (int i = 0; i < _nodes.Length; i++)
            {
                if (_nodes[i] != null)
                {
                    _nodes[i].Clear();
                    _nodes[i] = null;
                }
            }
        }

        public void Split() // Splits the node into 4 subnodes
        {
            int subWidth = _bounds.Width / 2;
            int subHeight = _bounds.Height / 2;
            int x = _bounds.X;
            int y = _bounds.Y;

            _nodes[0] = new Quadtree(_level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            _nodes[1] = new Quadtree(_level + 1, new Rectangle(x, y, subWidth, subHeight));
            _nodes[2] = new Quadtree(_level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            _nodes[3] = new Quadtree(_level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        /*
        * Determine which node the object belongs to. -1 means
        * object cannot completely fit within a child node and is part
        * of the parent node
        */
        private int GetIndex(Sprite sprite)
        {
            int index = -1;
            double verticalMidpoint = _bounds.X + (_bounds.Width / 2);
            double horizontalMidpoint = _bounds.X + (_bounds.Height / 2);

            // Object can completely fit within the top quadrants
            bool topQuadrant = (sprite.Hitbox.Y < horizontalMidpoint && sprite.Hitbox.Y + sprite.Hitbox.Height < horizontalMidpoint);
            // Object can completely fit within the bottom quadrants
            bool bottomQuadrant = (sprite.Hitbox.Y > horizontalMidpoint);

            // Object can completely fit within the left quadrants
            if (sprite.Hitbox.X < verticalMidpoint && sprite.Hitbox.X + sprite.Hitbox.Width < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            // Object can completely fit within the right quadrants
            else if (sprite.Hitbox.X > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }

        /*
        * Insert the object into the quadtree. If the node
        * exceeds the capacity, it will split and add all
        * objects to their corresponding nodes.
        */
        public void Insert(Sprite sprite)
        {
            if (_nodes[0] != null)
            {
                int index = GetIndex(sprite);

                if (index != -1)
                {
                    _nodes[index].Insert(sprite);

                    return;
                }
            }

            _sprites.Add(sprite);

            if (_sprites.Count > _maxObjects && _level < _maxLevels)
            {
                if (_nodes[0] == null)
                {
                    Split();
                }

                int i = 0;
                while (i < _sprites.Count)
                {
                    int index = GetIndex(_sprites[i]);
                    if (index != -1)
                    {
                        _nodes[index].Insert(_sprites[i]);
                        _sprites.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        /*
        * Return all objects that could collide with the given object
        */
        public List<Sprite> Retrieve(List<Sprite> returnObjects, Sprite sprite)
        {
            int index = GetIndex(sprite);
            if (index != -1 && _nodes[0] != null)
            {
                _nodes[index].Retrieve(returnObjects, sprite);
            }

            returnObjects.AddRange(_sprites);

            return returnObjects;
        }
    }
}
