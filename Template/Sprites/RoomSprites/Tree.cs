using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Interfaces;

namespace RogueLike.Sprites.RoomSprites
{
    public class Tree : Sprite, IDamageable
    {
        public Tree(Texture2D texture, Texture2D shadowTexture, Vector2 position) : base(texture, shadowTexture, position)
        {
            LayerOrigin = 71;
            _hurtbox = new Rectangle(Rectangle.X + 7 * Scale, Rectangle.Y + 53 * Scale, 15 * Scale, 7 * Scale);
        }

        public void UpdateHurtbox()
        {
            UpdateRectangle();
        }

        public void OnCollide(Hitbox hitbox)
        {

        }
    }
}
