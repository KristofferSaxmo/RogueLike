using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RogueLike.Managers;
using RogueLike.Models;
using RogueLike.Rooms;
using RogueLike.Sprites.Shadows;
using System.Collections.Generic;
using System.Linq;

namespace RogueLike.Sprites
{
    public class Player : Sprite, ICollidable
    {
        private KeyboardState _currentKey;

        private KeyboardState _previousKey;

        private Vector2 _previousPosition;

        private bool isFacingLeft = true;

        public bool IsDead
        {
            get { return Health <= 0; }
        }
        public Input Input { get; set; }
        public Player(Dictionary<string, Animation> animations) : base(animations)
        {
            LayerOrigin = 28;
        }
        public void Move()
        {
            Velocity = Vector2.Zero;

            if (_currentKey.IsKeyDown(Input.Left))
                Velocity = new Vector2(Velocity.X - Speed, Velocity.Y);

            if (_currentKey.IsKeyDown(Input.Right))
                Velocity = new Vector2(Velocity.X + Speed, Velocity.Y);

            if (_currentKey.IsKeyDown(Input.Up))
                Velocity = new Vector2(Velocity.X, Velocity.Y - Speed);

            if (_currentKey.IsKeyDown(Input.Down))
                Velocity = new Vector2(Velocity.X, Velocity.Y + Speed);
        }
        public void ChangeAnimation()
        {
            if (Velocity.X < 0)
            {
                _animationManager.Play(_animations["WalkLeft"]);
                isFacingLeft = true;
            }

            else if (Velocity.X > 0)
            {
                _animationManager.Play(_animations["WalkRight"]);
                isFacingLeft = false;
            }

            else if (Velocity.Y != 0 && isFacingLeft)
                _animationManager.Play(_animations["WalkLeft"]);

            else if (Velocity.Y != 0 && !isFacingLeft)
                _animationManager.Play(_animations["WalkRight"]);

            else if (isFacingLeft)
                _animationManager.Play(_animations["IdleLeft"]);

            else if (!isFacingLeft)
                _animationManager.Play(_animations["IdleRight"]);
        }
        public override void Update(GameTime gameTime)
        {
            if (IsDead)
                return;

            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            _previousPosition = Position;

            Move();

            ChangeAnimation();

            _animationManager.Update(gameTime, Layer);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsDead)
                return;

            base.Draw(gameTime, spriteBatch);
        }
        public void UpdateHitbox()
        {
            _hitbox = new Rectangle(Rectangle.X + 2 * Scale, Rectangle.Y + 25 * Scale, 7 * Scale, 3 * Scale);
        }
        public void OnCollide(Sprite sprite)
        {
            if (IsDead)
                return;

            if (sprite.Parent is Room)
                Position = _previousPosition;
        }
    }
}
