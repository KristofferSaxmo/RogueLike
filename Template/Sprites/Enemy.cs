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
        private Vector2 _playerPos;
        private Vector2 _startingPos;
        private bool _isFacingLeft;
        private int _attackCooldown;
        public Rectangle AttackRectangle
        {
            get
            {
                return new Rectangle(Rectangle.X - 300,
                                     Rectangle.Y + Rectangle.Height / 2 - 300,
                                     600,
                                     600);
            }
        }
        public Rectangle FollowRectangle
        {
            get
            {
                return new Rectangle(Rectangle.X - 500,
                                     Rectangle.Y + Rectangle.Height / 2 - 500,
                                     1000,
                                     1000);
            }
        }
        public Enemy(Dictionary<string, Animation> animations, Texture2D shadowTexture) : base(animations, shadowTexture)
        {
            LayerOrigin = 35;
            _startingPos = Position;
        }

        private void Attack()
        {
            
        }

        private void FollowPlayer()
        {

        }

        private void Roam()
        {

        }

        private void ChangeAnimation()
        {

        }

        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            _animationManager.Update(gameTime, Layer);
            _playerPos = playerPos;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void UpdateHitbox()
        {
            _hitbox = new Rectangle(Rectangle.X + 9 * Scale, Rectangle.Y + 26 * Scale, 16 * Scale, 21 * Scale);
        }

        public void OnCollide(Sprite sprite)
        {
            
        }
    }
}
