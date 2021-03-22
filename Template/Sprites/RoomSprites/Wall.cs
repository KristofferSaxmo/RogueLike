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
    public class Wall : Sprite, IHurtbox
    {
        public Wall(Texture2D texture) : base(texture)
        {
            LayerOrigin = 58;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void UpdateHurtbox()
        {
            _hurtbox = new Rectangle(Rectangle.X + 8 * Scale, Rectangle.Y + 45 * Scale, 12 * Scale, 15 * Scale);
        }
        public void OnCollide(Hitbox hitbox)
        {

        }
    }
}
