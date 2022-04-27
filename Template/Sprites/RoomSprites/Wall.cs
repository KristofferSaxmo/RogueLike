using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Interfaces;

namespace RogueLike.Sprites.RoomSprites
{
    public class Wall : Sprite, IDamageable
    {
        public Wall(Texture2D texture, Vector2 position) : base(texture, position)
        {
            LayerOrigin = 83;
            _hurtbox = new Rectangle(Rectangle.X + 8 * Scale, Rectangle.Y + 45 * Scale, 12 * Scale, 15 * Scale);
        }

        public void UpdateHurtbox()
        {
            
        }

        public void OnCollide(Hitbox hitbox)
        {

        }
    }
}
