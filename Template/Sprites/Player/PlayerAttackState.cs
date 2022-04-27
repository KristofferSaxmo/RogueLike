using Microsoft.Xna.Framework;
using RogueLike.Input;
using RogueLike.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites.Player
{
    enum Attacks
    {
        None,
        Attack1,
        Attack2,
        Attack3
    }
    public class PlayerAttackState : PlayerBaseState
    {
        //public PlayerAttackState(PlayerStateManager player) : base(player)
        //{
        //
        //}


        private int _attackCooldown;
        private Attacks _lastAttack;
        private Vector2 _direction;

        public override void EnterState(PlayerStateManager player)
        {
            player.Velocity = Vector2.Zero;
            _lastAttack = Attacks.None;
        }

        public override void UpdateState(PlayerStateManager player)
        {
            FlatMouse mouse = FlatMouse.Instance;



            // If mouse is left of player
            //if (mouse.GetRelativePosition().X < player.Rectangle.X)
            //{
            //    AttackLeft();
            //    return;
            //}
            _attackCooldown++;
            if (player.IsAttacking())
                return;
                
            if (_lastAttack == Attacks.None)
            {
                string key = player.IsFacingLeft ? "AttackLeft1" : "AttackRight1";
                player.SetAnimation(key);
                _lastAttack = Attacks.Attack1;
                player.Velocity = _direction * 5;
            }
            else if (_lastAttack == Attacks.Attack1)
            {
                string key = player.IsFacingLeft ? "AttackLeft2" : "AttackRight2";
                player.SetAnimation(key);
                _lastAttack = Attacks.Attack2;
                player.Velocity = _direction * 5;
            }
            else if (_lastAttack == Attacks.Attack2)
            {
                string key = player.IsFacingLeft ? "AttackLeft3" : "AttackRight3";
                player.SetAnimation(key);
                _lastAttack = Attacks.Attack3;
                player.Velocity = _direction * 15;
            }
            else if (_lastAttack == Attacks.Attack3)
            {
                FlatKeyboard keyboard = FlatKeyboard.Instance;
                if (keyboard.IsKeyDown(player.Input.Left)
                    || keyboard.IsKeyDown(player.Input.Up)
                    || keyboard.IsKeyDown(player.Input.Right)
                    || keyboard.IsKeyDown(player.Input.Down))
                {
                    player.SwitchState(player.MoveState);
                }
                player.SwitchState(player.IdleState);
            }
            player.Velocity = new Vector2(player.Velocity.X * 0.9f, player.Velocity.Y * 0.9f); // Friction when attacking
        }
    }
}
