using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RogueLike.Interfaces;
using RogueLike.Models;
using System.Collections.Generic;

namespace RogueLike.Sprites
{
    enum Attacks
    {
        None,
        Attack1,
        Attack2,
        Attack3
    }
    public class Player : Sprite, IDamageable, IDamaging
    {
        private Attacks _lastAttack = Attacks.None;

        private KeyboardState _currentKey;

        private MouseState _currentMouse;

        private Vector2 _direction;

        private int _attackCooldown;

        private int _collisionCooldown;

        private bool _isFacingLeft;

        public bool IsDead => Health <= 0;

        public Input Input { get; set; }

        public Player(Dictionary<string, Animation> animations) : base(animations)
        {
            LayerOrigin = 57;
        }

        private void Attack()
        {
            if (_lastAttack == Attacks.None) // Lose all movement speed before attacking
                Velocity = Vector2.Zero;

            _direction = Camera.GetWorldPosition(new Vector2(_currentMouse.X, _currentMouse.Y)) - Position;
            _direction.Normalize();

            // If mouse is left of player
            if (Camera.GetWorldPosition(new Vector2(_currentMouse.X, _currentMouse.Y)).X < Rectangle.X)
            {
                AttackLeft();
                _isFacingLeft = true;
                return;
            }

            // If mouse is right of player
            AttackRight();
            _isFacingLeft = false;
        }

        private void AttackLeft()
        {
            if (!IsAttacking() && (_lastAttack == Attacks.None || _lastAttack == Attacks.Attack3))
            {
                _animationManager.Play(_animations["AttackLeft1"]);
                _attackCooldown = 0;
                _lastAttack = Attacks.Attack1;
                Velocity = _direction * 5;
            }
            else if (!IsAttacking() && _lastAttack == Attacks.Attack1)
            {
                _animationManager.Play(_animations["AttackLeft2"]);
                _attackCooldown = 0;
                _lastAttack = Attacks.Attack2;
                Velocity = _direction * 5;
            }
            else if (!IsAttacking() && _lastAttack == Attacks.Attack2)
            {
                _animationManager.Play(_animations["AttackLeft3"]);
                _attackCooldown = 0;
                _lastAttack = Attacks.Attack3;
                Velocity = _direction * 15;
            }
        }

        private void AttackRight()
        {
            if (!IsAttacking() && (_lastAttack == Attacks.None || _lastAttack == Attacks.Attack3))
            {
                _animationManager.Play(_animations["AttackRight1"]);
                _attackCooldown = 0;
                _lastAttack = Attacks.Attack1;
                Velocity = _direction * 5;
            }
            else if (!IsAttacking() && _lastAttack == Attacks.Attack1)
            {
                _animationManager.Play(_animations["AttackRight2"]);
                _attackCooldown = 0;
                _lastAttack = Attacks.Attack2;
                Velocity = _direction * 5;
            }
            else if (!IsAttacking() && _lastAttack == Attacks.Attack2)
            {
                _animationManager.Play(_animations["AttackRight3"]);
                _attackCooldown = 0;
                _lastAttack = Attacks.Attack3;
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
                _lastAttack = Attacks.None;

            if (_collisionCooldown > 0)
                _collisionCooldown--;
        }

        public void UpdateHurtbox()
        {
            _hurtbox = new Rectangle(Rectangle.X + 29 * Scale, Rectangle.Y + 33 * Scale, 12 * Scale, 4 * Scale);
        }

        public void UpdateHitbox()
        {
            if (!IsAttacking()) return;


            if (_isFacingLeft)
            {
                if (_animationManager.CurrentAnimation == _animations["AttackLeft1"] && _animationManager.CurrentAnimation.CurrentFrame == 3)
                    Children.Add(new Hitbox(new Rectangle(Rectangle.X,
                        Rectangle.Y + 3 * Scale,
                        28 * Scale,
                        34 * Scale), this));

                else if (_animationManager.CurrentAnimation == _animations["AttackLeft2"] && _animationManager.CurrentAnimation.CurrentFrame == 3)
                    Children.Add(new Hitbox(new Rectangle(Rectangle.X + 5 * Scale,
                        Rectangle.Y,
                        49 * Scale,
                        36 * Scale), this));

                else if (_animationManager.CurrentAnimation == _animations["AttackLeft3"])
                    Children.Add(new Hitbox(new Rectangle(Rectangle.X,
                        Rectangle.Y + 19 * Scale,
                        18 * Scale,
                        4 * Scale), this));

                return;
            }

            if (_animationManager.CurrentAnimation == _animations["AttackRight1"] && _animationManager.CurrentAnimation.CurrentFrame == 3)
                Children.Add(new Hitbox(new Rectangle(Rectangle.X + 41 * Scale,
                    Rectangle.Y + 3 * Scale,
                    28 * Scale,
                    34 * Scale), this));

            else if (_animationManager.CurrentAnimation == _animations["AttackRight2"] && _animationManager.CurrentAnimation.CurrentFrame == 3)
                Children.Add(new Hitbox(new Rectangle(Rectangle.X + 14 * Scale,
                    Rectangle.Y,
                    49 * Scale,
                    36 * Scale), this));

            else if (_animationManager.CurrentAnimation == _animations["AttackRight3"])
                Children.Add(new Hitbox(new Rectangle(Rectangle.X + 51 * Scale,
                    Rectangle.Y + 19 * Scale,
                    18 * Scale,
                    4 * Scale), this));
        }

        public void OnCollide(Hitbox hitbox)
        {
            if (_collisionCooldown != 0)
                return;

            if (hitbox.Parent is Lightning)
            {
                Health--;
                _collisionCooldown = 60;
            }
        }

        private bool IsAttacking()
        {
            return _animationManager.CurrentAnimation == _animations["AttackLeft1"] ||
                   _animationManager.CurrentAnimation == _animations["AttackLeft2"] ||
                   _animationManager.CurrentAnimation == _animations["AttackLeft3"] ||
                   _animationManager.CurrentAnimation == _animations["AttackRight1"] ||
                   _animationManager.CurrentAnimation == _animations["AttackRight2"] ||
                   _animationManager.CurrentAnimation == _animations["AttackRight3"];
        }
    }
}
