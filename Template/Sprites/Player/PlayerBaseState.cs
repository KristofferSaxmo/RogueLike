using RogueLike.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites.Player
{
    public abstract class PlayerBaseState
    {
        //public PlayerBaseState(PlayerStateManager player)
        //{
        //    PlayerStateManager p = player;
        //}

        public abstract void EnterState(PlayerStateManager player);

        public abstract void UpdateState(PlayerStateManager player);
    }
}
