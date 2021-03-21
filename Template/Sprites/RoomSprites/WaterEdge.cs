using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Interfaces;
using RogueLike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites.RoomSprites
{
    public class WaterEdge : Sprite, IHurtbox
    {
        public WaterEdge(Dictionary<string, Animation> animations) : base(animations)
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
        public void UpdateHurtbox()
        {
            _hurtbox = Rectangle;
        }
        public void OnCollide(Sprite sprite)
        {
            
        }
    }
}
