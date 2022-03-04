using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueLike
{
    class Letter
    {
        private Texture2D _texture;
        private Vector2 _origin;
        private Vector2 _position;
        private Rectangle _sourceRectangle;

        public char Character { get; }
        public Rectangle Rectangle { get; }
        public int Scale { get; }

        public Letter(Texture2D texture, Vector2 position, Rectangle sourceRectangle, int scale, char character)
        {
            Character = character;
            _texture = texture;
            _position = position;
            _origin = new Vector2(position.X + (sourceRectangle.X * 2) + (4 * scale), position.Y + (sourceRectangle.Y * 2) + (4 * scale));
            _sourceRectangle = sourceRectangle;
            Scale = scale;


            if (character == ' ')
            {
                Rectangle = new Rectangle(0, 0, 4, 8);
            }
            else
            {
                // Create a new texture of the desired size
                Texture2D croppedTexture = Game1.Bit8Texture;

                // Copy the data from the cropped region into a buffer, then into the new texture
                Color[] data = new Color[sourceRectangle.Width * sourceRectangle.Height];
                texture.GetData(0, sourceRectangle, data, 0, sourceRectangle.Width * sourceRectangle.Height);
                croppedTexture.SetData(data);

                // Create smallest possible rectangle
                Rectangle = Util.GetSmallestRectangleFromTexture(croppedTexture);
            }

            Rectangle = new Rectangle(Rectangle.X * scale + (int)position.X, Rectangle.Y * scale + (int)position.Y, Rectangle.Width * scale, Rectangle.Height * scale);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            //spriteBatch.Draw(Game1.DefaultTex, Rectangle, Color.Blue);
            spriteBatch.Draw(_texture, _position, _sourceRectangle, color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
