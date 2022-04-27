using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Interfaces;
using RogueLike.Models;
using RogueLike.Structs;
using RogueLike.States;
using System.Collections.Generic;
using System.IO;

namespace RogueLike.Sprites
{
    public class Enemy : Sprite, IDamageable
    {
        private BinaryWriter _bw;

        private Vector2 _playerPos, _playerVel;

        private Vector2 _startingPos;

        private Vector2 _direction;

        private bool _isFacingLeft;
        private bool _isRoaming = false;
        private bool _isDying;

        private int _attackCooldown;

        private int _collisionCooldown;

        private readonly Animation _lightningPrefab;

        public Circle AttackCircle => new Circle(new Vector2(Rectangle.X, Rectangle.Y + Rectangle.Height / 2 - 48), 300);

        public Circle FollowCircle => new Circle(new Vector2(Rectangle.X, Rectangle.Y + Rectangle.Height / 2 - 48), 600);

        public Enemy(Dictionary<string, Animation> animations, Texture2D shadowTexture, Vector2 position) : base(animations, shadowTexture, position)
        {
            LayerOrigin = 62;
            _startingPos = Position;
            _lightningPrefab = _animations["GhostLightning"];
        }

        private void Attack()
        {
            Velocity = Vector2.Zero;

            _animationManager.Play(_isFacingLeft ? _animations["GhostAttackLeft"] : _animations["GhostAttackRight"]);

            Animation lightning = _lightningPrefab.Clone() as Animation;

            Children.Add(new Lightning(lightning, new Vector2(_playerPos.X + (_playerVel.X * 32), _playerPos.Y + (_playerVel.Y * 32) - 99))
            {
                Parent = this
            });

            for (int i = 0; i < Game1.Random.Next(4, 8); i++)
            {
                Animation randomLightning = _lightningPrefab.Clone() as Animation;
                randomLightning.CurrentFrame -= i*2;
                Children.Add(new Lightning(randomLightning, new Vector2(_playerPos.X + (_playerVel.X * 32) + Game1.Random.Next(-100, 100), _playerPos.Y + (_playerVel.Y * 32) - 99 + Game1.Random.Next(-100, 100)))
                {
                    Parent = this
                });
            }

            _attackCooldown = 150;
        }

        private void FollowPlayer()
        {
            _direction = _playerPos - Position;
            _direction.Normalize();

            Velocity = _direction * Speed;
        }

        private void Roam()
        {
            Velocity = Vector2.Zero;
            if (_isRoaming)
                return;


        }

        private void ChangeAnimation()
        {
            if (Position.X > _playerPos.X)
            {
                _animationManager.Play(_animations["GhostLeft"]);
                _isFacingLeft = true;
            }
            else if (Position.X < _playerPos.X)
            {
                _animationManager.Play(_animations["GhostRight"]);
                _isFacingLeft = false;
            }
        }

        public void Update(GameTime gameTime, Vector2 playerPos, Vector2 playerVel)
        {
            if (_isDying && _animationManager.CurrentAnimation.CurrentFrame >= _animations["GhostDeathLeft"].FrameCount - 1)
                IsRemoved = true;

            _animationManager.Update(gameTime, Layer);

            if (_isDying)
            {
                Velocity = Vector2.Zero;
                return;
            }

            _playerPos = playerPos;
            _playerVel = playerVel;

            if (!InAnimation())
            {
                if (AttackCircle.Contains(_playerPos) && _attackCooldown == 0)
                    Attack();

                else if (FollowCircle.Contains(_playerPos))
                    FollowPlayer();

                else if (!_isRoaming)
                    Roam();
            }

            if (!InAnimation())
                ChangeAnimation();

            if (_attackCooldown > 0)
                _attackCooldown--;

            if (_collisionCooldown > 0)
                _collisionCooldown--;

            UpdateRectangle();
        }

        public void UpdateHurtbox()
        {
            if (!_isDying)
            {
                _hurtbox = new Rectangle(Rectangle.X + 9 * Scale, Rectangle.Y + 31 * Scale, 16 * Scale, 16 * Scale);
                return;
            }

            _hurtbox = Rectangle.Empty;
        }

        public void OnCollide(Hitbox hitbox)
        {
            _isDying = _animationManager.CurrentAnimation == _animations["GhostDeathLeft"] || _animationManager.CurrentAnimation == _animations["GhostDeathRight"];

            if (_isDying || _collisionCooldown != 0)
                return;

            if (hitbox.Parent is Player.Player)
            {
                Health--;
                _collisionCooldown = 10;
            }

            if (Health > 0) return;

            GameState.Score++;

            _bw = new BinaryWriter(new FileStream("Score.bin", FileMode.OpenOrCreate, FileAccess.Write));
            _bw.Write(GameState.Score);
            _bw.Close();
           

            Velocity = Vector2.Zero;

            if (_isFacingLeft)
            {
                _animationManager.Play(_animations["GhostDeathLeft"]);
                
                return;
            }

            _animationManager.Play(_animations["GhostDeathRight"]);
        }

        private bool InAnimation()
        {
            return _animationManager.CurrentAnimation == _animations["GhostAttackLeft"] ||
                   _animationManager.CurrentAnimation == _animations["GhostAttackRight"] ||
                   _animationManager.CurrentAnimation == _animations["GhostDeathLeft"] ||
                   _animationManager.CurrentAnimation == _animations["GhostDeathRight"];
        }
    }
}
