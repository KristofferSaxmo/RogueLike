using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Interfaces;

namespace RogueLike.Sprites.RoomSprites
{
    public class Wall : Sprite, IHurtbox
    {
        public Wall(Texture2D texture) : base(texture)
        {
            LayerOrigin = 58;
        }

        public void UpdateHurtbox()
        {
            _hurtbox = new Rectangle(Rectangle.X + 8 * Scale, Rectangle.Y + 45 * Scale, 12 * Scale, 15 * Scale);
        }

        public void OnCollide(Hitbox hitbox)
        {

        }
    }
}
