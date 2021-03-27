using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Models;
using System.Collections.Generic;

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
    }
}
