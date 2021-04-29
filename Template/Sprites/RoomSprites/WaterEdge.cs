using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Interfaces;
using RogueLike.Models;
using System.Collections.Generic;

namespace RogueLike.Sprites.RoomSprites
{
    public class WaterEdge : Sprite, IDamageable
    {
        public WaterEdge(Dictionary<string, Animation> animations) : base(animations)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _animationManager.Update(gameTime, Layer);
        }

        public void UpdateHurtbox()
        {
            _hurtbox = Rectangle;
        }

        public void OnCollide(Hitbox hitbox)
        {

        }
    }
}
