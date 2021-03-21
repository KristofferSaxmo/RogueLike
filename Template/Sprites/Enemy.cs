using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Interfaces;
using RogueLike.Models;
using RogueLike.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites
{
    public class Enemy : Sprite, IHurtbox
    {

        private Vector2 _playerPos;

        private Vector2 _startingPos;

        private bool _isFacingLeft, _isRoaming, _isDying;

        private int _attackCooldown;

        private Vector2 _direction;

        private Animation _lightningPrefaba;

        public Circle AttackCircle
        {
            get
            {
                return new Circle(new Vector2(Rectangle.X, Rectangle.Y + Rectangle.Height / 2 - 48), 300);
            }
        }

        public Circle FollowCircle
        {
            get
            {
                return new Circle(new Vector2(Rectangle.X, Rectangle.Y + Rectangle.Height / 2 - 48), 600);
            }
        }

        public Enemy(Dictionary<string, Animation> animations, Texture2D shadowTexture) : base(animations, shadowTexture)
        {
            LayerOrigin = 35;
            _startingPos = Position;
            _lightningPrefaba = _animations["GhostLightning"];
        }

        private void Attack()
        {
            Velocity = Vector2.Zero;

            if (_isFacingLeft)
                _animationManager.Play(_animations["GhostAttackLeft"]);
            else
                _animationManager.Play(_animations["GhostAttackRight"]);

            Animation lightning = _lightningPrefaba.Clone() as Animation;

            Children.Add(new Lightning(lightning)
            {
                Position = new Vector2(_playerPos.X, _playerPos.Y - 99),
                Parent = this,
            });

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

        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            if (_isDying && _animationManager.CurrentAnimation.CurrentFrame >= _animations["GhostDeathLeft"].FrameCount - 1)
                IsRemoved = true;

            _animationManager.Update(gameTime, Layer);

            if (_isDying)
                return;

            _playerPos = playerPos;

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
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void UpdateHurtbox()
        {
            if (!_isDying)
            {
                _hurtbox = new Rectangle(Rectangle.X + 9 * Scale, Rectangle.Y + 26 * Scale, 16 * Scale, 21 * Scale);
                return;
            }

            _hurtbox = Rectangle.Empty;    
        }

        public void OnCollide(Sprite sprite)
        {
            _isDying = _animationManager.CurrentAnimation == _animations["GhostDeathLeft"] || _animationManager.CurrentAnimation == _animations["GhostDeathRight"];

            if (_isDying)
                return;

            if (sprite is Player)
            {
                Health--;
                _animationManager.Color = new Color(255, 255, 255, 0);
            }

            if (Health <= 0)
            {
                if (_isFacingLeft)
                {
                    _animationManager.Play(_animations["GhostDeathLeft"]);
                    return;
                }

                _animationManager.Play(_animations["GhostDeathRight"]);

                Velocity = Vector2.Zero;
            }

        }
        private bool InAnimation()
        {
            if (_animationManager.CurrentAnimation == _animations["GhostAttackLeft"] ||
                _animationManager.CurrentAnimation == _animations["GhostAttackRight"] ||
                _animationManager.CurrentAnimation == _animations["GhostDeathLeft"] ||
                _animationManager.CurrentAnimation == _animations["GhostDeathRight"])
                return true;

            return false;
        }
    }
}
