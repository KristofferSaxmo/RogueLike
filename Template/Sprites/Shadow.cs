using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueLike.Sprites
{
    public class Shadow : Sprite
    {
        public Shadow(Texture2D texture) : base(texture)
        {

        }
        public override void Update(GameTime gameTime)
        {
            Position = Parent.Position;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, null, Color, 0f, Origin, SpriteEffects.None, 0f);
        }
    }
}
