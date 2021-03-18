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
    public class Enemy : Sprite, ICollidable
    {
        public Enemy(Dictionary<string, Animation> animations) : base(animations)
        {

        }

        public void Attack()
        {

        }

        public override void Update(GameTime gameTime)
        {
            Attack();
        }

        private void FollowPlayer()
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void UpdateHitbox()
        {
            throw new NotImplementedException();
        }

        public void OnCollide(Sprite sprite)
        {
            throw new NotImplementedException();
        }
    }
}
