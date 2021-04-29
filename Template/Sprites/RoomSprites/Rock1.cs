using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Interfaces;

namespace RogueLike.Sprites.RoomSprites
{
    public class Rock1 : Sprite, IDamageable
    {
        public Rock1(Texture2D texture, Texture2D shadowTexture) : base(texture, shadowTexture)
        {
            LayerOrigin = 2;
        }

        public void OnCollide(Hitbox hitbox)
        {

        }

        public void UpdateHurtbox()
        {
            _hurtbox = new Rectangle(Rectangle.X + 2 * Scale, Rectangle.Y + 7 * Scale, 14 * Scale, 3 * Scale);
        }
    }
}
