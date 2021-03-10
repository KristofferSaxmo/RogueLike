using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Models;

namespace RogueLike.Sprites
{
    class Player : Sprite, ICollidable
    {
        private KeyboardState _currentKey;

        private KeyboardState _previousKey;
        public bool IsDead
        {
            get { return Health <= 0; }
        }
        public Input Input { get; set; }
        public Player(Texture2D texture) : base(texture)
        {
            
        }
        public void Move()
        {
            Velocity = Vector2.Zero;

            if (_currentKey.IsKeyDown(Input.Left))
                Velocity = new Vector2(Velocity.X - Speed, Velocity.Y);

            else if (_currentKey.IsKeyDown(Input.Right))
                Velocity = new Vector2(Velocity.X + Speed, Velocity.Y);

            if (_currentKey.IsKeyDown(Input.Up))
                Velocity = new Vector2(Velocity.X, Velocity.Y - Speed);

            if (_currentKey.IsKeyDown(Input.Down))
                Velocity = new Vector2(Velocity.X, Velocity.Y + Speed);

            Position += Velocity;
        }
        public override void Update(GameTime gameTime)
        {
            if (IsDead)
                return;

            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            Move();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsDead)
                return;
            
            base.Draw(gameTime, spriteBatch);
        }
        public void OnCollide(Sprite sprite)
        {
            if (IsDead)
                return;
        }
    }
}
