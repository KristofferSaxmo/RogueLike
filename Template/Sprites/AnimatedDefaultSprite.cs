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
    public class AnimatedDefaultSprite : Sprite
    {
        public AnimatedDefaultSprite(Dictionary<string, Animation> animations) : base(animations)
        {

        }
        public override void Update(GameTime gameTime)
        {
            _animationManager.Update(gameTime, Layer);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
