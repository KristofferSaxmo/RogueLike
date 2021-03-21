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
    public class Rock1 : Sprite, IHurtbox
    {
        public Rock1(Texture2D texture, Texture2D shadowTexture) : base(texture, shadowTexture)
        {
            LayerOrigin = 2;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void OnCollide(Sprite sprite)
        {
            
        }

        public void UpdateHurtbox()
        {
            _hurtbox = new Rectangle(Rectangle.X + 2 * Scale, Rectangle.Y + 7 * Scale, 14 * Scale, 3 * Scale);
        }
    }
}
