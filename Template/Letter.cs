using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueLike
{
    class Letter
    {
        private Texture2D _texture;
        private Vector2 _origin;
        private Rectangle _sourceRectangle;

        public Vector2 Position { get; private set; }
        public char Character { get; }
        public Rectangle Rectangle { get; private set; }
        public int Scale { get; }

        public Letter(Texture2D texture, Vector2 position, Rectangle sourceRectangle, int scale, char character)
        {
            Character = character;
            _texture = texture;
            _sourceRectangle = sourceRectangle;
            Scale = scale;
            _origin = new Vector2(position.X + (_sourceRectangle.X * 2) + (4 * Scale), position.Y + (_sourceRectangle.Y * 2) + (4 * Scale));

            SetPosition(position);
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
            if (Character == ' ')
            {
                Rectangle = new Rectangle(0, 0, 4, 8);
            }
            else
            {
                // Create a new texture of the desired size
                Texture2D croppedTexture = Game1.Bit8Texture;

                // Copy the data from the cropped region into a buffer, then into the new texture
                Color[] data = new Color[_sourceRectangle.Width * _sourceRectangle.Height];
                _texture.GetData(0, _sourceRectangle, data, 0, _sourceRectangle.Width * _sourceRectangle.Height);
                croppedTexture.SetData(data);

                // Create smallest possible rectangle
                Rectangle = Util.GetSmallestRectangleFromTexture(croppedTexture);
            }

            Rectangle = new Rectangle(Rectangle.X * Scale + (int)position.X, Rectangle.Y * Scale + (int)position.Y, Rectangle.Width * Scale, Rectangle.Height * Scale);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            //spriteBatch.Draw(Game1.DefaultTex, Rectangle, Color.Blue);
            spriteBatch.Draw(_texture, Position, _sourceRectangle, color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
