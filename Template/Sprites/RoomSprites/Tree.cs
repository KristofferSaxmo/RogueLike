﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites.RoomSprites
{
    public class Tree : Sprite, ICollidable
    {
        public Tree(Texture2D texture) : base(texture)
        {
            LayerOrigin = 57;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
        public void UpdateHitbox()
        {
            _hitbox = new Rectangle(Rectangle.X + 9 * Scale, Rectangle.Y + 53 * Scale, 11 * Scale, 7 * Scale);
        }
        public void OnCollide(Sprite sprite)
        {
            throw new NotImplementedException();
        }
    }
}
