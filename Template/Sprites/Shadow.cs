using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites
{
    public class Shadow : Sprite
    {
        public Shadow(Texture2D texture) : base(texture)
        {

        }
        public override void Update(GameTime gameTime)
        {
            Position = Parent.Position;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, null, Color, Rotation, Origin, SpriteEffects.None, 0f);
        }
    }
}
