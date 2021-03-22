using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites.RoomSprites
{
    public class Tree : Sprite, IHurtbox
    {
        public Tree(Texture2D texture, Texture2D shadowTexture) : base(texture, shadowTexture)
        {
            LayerOrigin = 71;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void UpdateHurtbox()
        {
            _hurtbox = new Rectangle(Rectangle.X + 7 * Scale, Rectangle.Y + 53 * Scale, 15 * Scale, 7 * Scale);
        }

        public void OnCollide(Hitbox hitbox)
        {

        }
    }
}
