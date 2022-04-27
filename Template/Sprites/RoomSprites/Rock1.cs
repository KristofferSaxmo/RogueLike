using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Interfaces;

namespace RogueLike.Sprites.RoomSprites
{
    public class Rock1 : Sprite, IDamageable
    {
        public Rock1(Texture2D texture, Texture2D shadowTexture, Vector2 position) : base(texture, shadowTexture, position)
        {
            LayerOrigin = 2;
            _hurtbox = new Rectangle(Rectangle.X + 2 * Scale, Rectangle.Y + 7 * Scale, 14 * Scale, 3 * Scale);
        }

        public void OnCollide(Hitbox hitbox)
        {

        }

        public void UpdateHurtbox()
        {
            
        }
    }
}
