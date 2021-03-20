using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites
{
    public class Lightning : Sprite, ICollidable
    {
        public Lightning(Animation animation) : base(animation)
        {
            LayerOrigin = 153;
        }
        
        public override void Update(GameTime gameTime)
        {
            if (_animationManager.CurrentAnimation == null)
            {
                IsRemoved = true;
                return;
            }

            _animationManager.Update(gameTime, Layer);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void UpdateHitbox()
        {
            if (_animationManager.CurrentAnimation == null)
                return;
                        
            if (_animationManager.CurrentAnimation.CurrentFrame >= 6)
                _hitbox = new Rectangle(Rectangle.X + 3 * Scale, Rectangle.Y + 99 * Scale, 23 * Scale, 13 * Scale);
        }

        public void OnCollide(Sprite sprite)
        {

        }
    }
}
