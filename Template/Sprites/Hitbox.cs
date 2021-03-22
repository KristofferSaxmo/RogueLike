using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites
{
    public class Hitbox : Sprite
    {
        public Hitbox(Rectangle rectangle, Sprite sprite)
        {
            _rectangle = rectangle;
            Origin = sprite.Origin;
            Parent = sprite;
            IsRemoved = true;
        }
    }
}
