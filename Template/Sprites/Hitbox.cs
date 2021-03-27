using Microsoft.Xna.Framework;

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
