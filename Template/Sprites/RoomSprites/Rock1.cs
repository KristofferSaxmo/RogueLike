using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites.RoomSprites
{
    public class Rock1 : Sprite
    {
        public Rock1(Texture2D texture, Texture2D shadowTexture) : base(texture, shadowTexture)
        {
            LayerOrigin = -15;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
