using Microsoft.Xna.Framework;
using RogueLike.Input;

namespace RogueLike.Sprites.Player
{
    public class PlayerMoveState : PlayerBaseState
    {
        public override void EnterState(PlayerStateManager player)
        {

        }

        public override void UpdateState(PlayerStateManager player)
        {
            FlatKeyboard keyboard = FlatKeyboard.Instance;
            FlatMouse mouse = FlatMouse.Instance;

            player.Velocity = Vector2.Zero;

            if (keyboard.IsKeyDown(player.Input.Left))
                player.Velocity = new Vector2(player.Velocity.X - player.Speed, player.Velocity.Y);

            if (keyboard.IsKeyDown(player.Input.Right))
                player.Velocity = new Vector2(player.Velocity.X + player.Speed, player.Velocity.Y);

            if (keyboard.IsKeyDown(player.Input.Up))
                player.Velocity = new Vector2(player.Velocity.X, player.Velocity.Y - player.Speed);

            if (keyboard.IsKeyDown(player.Input.Down))
                player.Velocity = new Vector2(player.Velocity.X, player.Velocity.Y + player.Speed);

            if (player.Velocity.X < 0)
            {
                player.IsFacingLeft = true;
                player.SetAnimation("WalkLeft");
            }
            else if (player.Velocity.X > 0)
            {
                player.IsFacingLeft = false;
                player.SetAnimation("WalkRight");
            }
            else if (player.IsFacingLeft)
                player.SetAnimation("WalkLeft");
            else
                player.SetAnimation("WalkRight");
                

            if (mouse.IsLeftButtonDown())
                player.SwitchState(player.AttackState);

            else if (player.Velocity == Vector2.Zero)
            {
                player.SwitchState(player.IdleState);
            }
        }
    }
}
