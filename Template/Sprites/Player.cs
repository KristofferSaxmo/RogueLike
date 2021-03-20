using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RogueLike.Managers;
using RogueLike.Models;
using RogueLike.Rooms;
using RogueLike.Sprites.RoomSprites;
using System.Collections.Generic;
using System.Linq;

namespace RogueLike.Sprites
{
    public class Player : Sprite, ICollidable
    {
        private KeyboardState _currentKey;

        private MouseState _currentMouse;

        private Vector2 _direction;

        private int _attackCooldown;

        private int _collisionCooldown;

        private int _lastAttack = 0;

        private bool _isFacingLeft;
        public bool IsDead
        {
            get { return Health <= 0; }
        }
        public Input Input { get; set; }
        public Player(Dictionary<string, Animation> animations) : base(animations)
        {
            LayerOrigin = 57;
        }
        private void Attack()
        {
            if (_lastAttack == 0) // Lose all movement speed before attacking
                Velocity = Vector2.Zero;

            _direction = Camera.GetWorldPosition(new Vector2(_currentMouse.X, _currentMouse.Y)) - Position;
            _direction.Normalize();

            // If mouse is left of player
            if (Camera.GetWorldPosition(new Vector2(_currentMouse.X, _currentMouse.Y)).X < Rectangle.X)
            {
                AttackLeft();
                return;
            }

            // If mouse is right of player
            AttackRight();
        }

        private void AttackLeft()
        {
            if (!IsAttacking() && (_lastAttack == 0 || _lastAttack == 3))
            {
                _animationManager.Play(_animations["AttackLeft1"]);
                _attackCooldown = 0;
                _lastAttack = 1;
                Velocity = _direction * 5;
            }
            else if (!IsAttacking() && _lastAttack == 1)
            {
                _animationManager.Play(_animations["AttackLeft2"]);
                _attackCooldown = 0;
                _lastAttack = 2;
                Velocity = _direction * 5;
            }
            else if (!IsAttacking() && _lastAttack == 2)
            {
                _animationManager.Play(_animations["AttackLeft3"]);
                _attackCooldown = 0;
                _lastAttack = 3;
                Velocity = _direction * 15;
            }
        }

        private void AttackRight()
        {
            if (!IsAttacking() && (_lastAttack == 0 || _lastAttack == 3))
            {
                _animationManager.Play(_animations["AttackRight1"]);
                _attackCooldown = 0;
                _lastAttack = 1;
                Velocity = _direction * 5;
            }
            else if (!IsAttacking() && _lastAttack == 1)
            {
                _animationManager.Play(_animations["AttackRight2"]);
                _attackCooldown = 0;
                _lastAttack = 2;
                Velocity = _direction * 5;
            }
            else if (!IsAttacking() && _lastAttack == 2)
            {
                _animationManager.Play(_animations["AttackRight3"]);
                _attackCooldown = 0;
                _lastAttack = 3;
                Velocity = _direction * 15;
            }
        }

        private void Move()
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

        private void ChangeAnimation()
        {
            if (Velocity.X < 0)
            {
                _animationManager.Play(_animations["WalkLeft"]);
                _isFacingLeft = true;
            }
            else if (Velocity.X > 0)
            {
                _animationManager.Play(_animations["WalkRight"]);
                _isFacingLeft = false;
            }
            else if (Velocity.Y != 0 && _isFacingLeft)
                _animationManager.Play(_animations["WalkLeft"]);

            else if (Velocity.Y != 0 && !_isFacingLeft)
                _animationManager.Play(_animations["WalkRight"]);

            else if (_isFacingLeft && !IsAttacking())
                _animationManager.Play(_animations["IdleLeft"]);

            else if (!_isFacingLeft && !IsAttacking())
                _animationManager.Play(_animations["IdleRight"]);
        }

        public override void Update(GameTime gameTime)
        {
            _animationManager.Update(gameTime, Layer);

            _currentKey = Keyboard.GetState();
            _currentMouse = Mouse.GetState();

            if (_currentMouse.LeftButton == ButtonState.Pressed)
                Attack();

            if (IsAttacking())
            {
                Velocity = new Vector2(Velocity.X * 0.9f, Velocity.Y * 0.9f); // Friction when attacking
            }

            else
            {
                Move();
                ChangeAnimation();
            }

            if (_attackCooldown < 30)
                _attackCooldown++;
            else
                _lastAttack = 0;

            if (_collisionCooldown > 0)
                _collisionCooldown--;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void UpdateHitbox()
        {
            _hitbox = new Rectangle(Rectangle.X + 29 * Scale, Rectangle.Y + 33 * Scale, 12 * Scale, 4 * Scale);
        }

        public void OnCollide(Sprite sprite)
        {

            if (_collisionCooldown != 0)
                return;

            if (sprite is Enemy)
            {
                Health--;
                _collisionCooldown = 60;
            }
        }

        private bool IsAttacking()
        {
            if (_animationManager.CurrentAnimation == _animations["AttackLeft1"] ||
                _animationManager.CurrentAnimation == _animations["AttackLeft2"] ||
                _animationManager.CurrentAnimation == _animations["AttackLeft3"] ||
                _animationManager.CurrentAnimation == _animations["AttackRight1"] ||
                _animationManager.CurrentAnimation == _animations["AttackRight2"] ||
                _animationManager.CurrentAnimation == _animations["AttackRight3"])
                return true;

            return false;
        }
    }
}
