using RogueLike.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites.Player
{
    public class PlayerIdleState : PlayerBaseState
    {
        //public PlayerIdleState(PlayerStateManager player)
        //{
        //
        //}

        public override void EnterState(PlayerStateManager player)
        {

        }

        public override void UpdateState(PlayerStateManager player)
        {
            FlatMouse mouse = FlatMouse.Instance;
            FlatKeyboard keyboard = FlatKeyboard.Instance;

            if (mouse.IsLeftButtonDown())
                player.SwitchState(player.AttackState);

            if (keyboard.IsKeyDown(player.Input.Left)
                || keyboard.IsKeyDown(player.Input.Up)
                || keyboard.IsKeyDown(player.Input.Right)
                || keyboard.IsKeyDown(player.Input.Down))
            {
                player.SwitchState(player.MoveState);
            }

            if (player.IsFacingLeft)
                player.SetAnimation("IdleLeft");
            else
                player.SetAnimation("IdleRight");
        }
    }
}
