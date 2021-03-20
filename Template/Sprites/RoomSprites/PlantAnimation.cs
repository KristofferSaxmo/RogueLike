using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Models;
using RogueLike.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites.RoomSprites
{
    public class PlantAnimation : Sprite
    {
        public PlantAnimation(Dictionary<string, Animation> animations) : base(animations)
        {
            LayerOrigin = 17;
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
