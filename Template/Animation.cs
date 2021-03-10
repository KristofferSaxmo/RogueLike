using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueLike
{
    class Animation
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int FrameTime { get; set; }
        public Point Size { get; set; }

        private int currentFrame;
        private int totalFrames;
        private int frameCountdown;

        public Animation(Texture2D texture, int frameTime, int rows, int columns, int scale)
        {
            Texture = texture;
            FrameTime = frameTime;
            Rows = rows;
            Columns = columns;
            Size = new Point(texture.Width * scale * rows / 4, texture.Height * scale * columns / 4);
            frameCountdown = frameTime;
            currentFrame = 0;
            totalFrames = rows * columns;
        }

        public void Reset()
        {
            frameCountdown = FrameTime;
            currentFrame = 0;
        }

        public void Update()
        {
            if (frameCountdown == 0)
            {
                currentFrame++;
                frameCountdown = FrameTime;
            }

            frameCountdown--;

            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle(position.ToPoint(), Size);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
