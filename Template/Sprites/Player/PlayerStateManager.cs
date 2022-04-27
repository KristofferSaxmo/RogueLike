using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RogueLike.Interfaces;
using RogueLike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites.Player
{
    public class PlayerStateManager : Sprite, IDamageable, IDamaging
    {
        private int _collisionCooldown;

        public int AttackCooldown { get; set; }

        public bool IsFacingLeft { get; set; }

        public bool IsDead => Health <= 0;

        public KeyInput Input { get; set; }


        private PlayerBaseState _currentState;

        public PlayerIdleState IdleState = new PlayerIdleState();
        public PlayerMoveState MoveState = new PlayerMoveState();
        public PlayerAttackState AttackState = new PlayerAttackState();

        public PlayerStateManager(Dictionary<string, Animation> animations, Vector2 position) : base(animations, position)
        {
            LayerOrigin = 57;
            _currentState = IdleState;
            _currentState.EnterState(this);
        }

        public void SwitchState(PlayerBaseState state)
        {
            _currentState = state;
            _currentState.EnterState(this);
        }

        public override void Update(GameTime gameTime)
        {
            AttackCooldown++;
            _currentState.UpdateState(this);
            UpdateRectangle();
            _animationManager.Update(gameTime, Layer);
        }

        public void SetAnimation(string key)
        {
            _animationManager.Play(_animations[key]);
        }

        public void UpdateHurtbox()
        {
            _hurtbox = new Rectangle(Rectangle.X + 29 * Scale, Rectangle.Y + 33 * Scale, 12 * Scale, 4 * Scale);
        }

        public void OnCollide(Hitbox hitbox)
        {

        }

        public void UpdateHitbox()
        {

        }

        public bool IsAttacking()
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
