using Microsoft.Xna.Framework.Graphics;

namespace RogueLike.Sprites
{
    public class DefaultSprite : Sprite
    {
        public DefaultSprite(Texture2D texture) : base(texture)
        {
            LayerOrigin = -1000;
        }
    }
}
