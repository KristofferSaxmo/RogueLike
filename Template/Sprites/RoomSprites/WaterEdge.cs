using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites.RoomSprites
{
    public class WaterEdge : Sprite, ICollidable
    {
        public WaterEdge(Texture2D texture) : base(texture)
        {
            LayerOrigin = 0;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
        public void UpdateHitbox()
        {
            _hitbox = Rectangle;
        }
        public void OnCollide(Sprite sprite)
        {
            
        }
    }
}
