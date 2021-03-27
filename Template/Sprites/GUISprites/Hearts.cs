using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites.GUISprites
{
    public class Hearts : Sprite
    {
        private readonly Rectangle _fullHeart, _halfHeart, _emptyHeart;
        private readonly Rectangle _heart1, _heart2, _heart3;
        private const int HeartCount = 3;

        public Hearts(Texture2D texture) : base(texture)
        {
            Scale = 5;
            Position = new Vector2(20, 20);

            int width = Texture.Width / HeartCount;
            int height = Texture.Height;

            _fullHeart = new Rectangle(
                width * (0 % HeartCount),
                height * (0 / HeartCount),
                width,
                height);

            _halfHeart = new Rectangle(
                width * (1 % HeartCount),
                height * (1 / HeartCount),
                width,
                height);

            _emptyHeart = new Rectangle(
                width * (2 % HeartCount),
                height * (2 / HeartCount),
                width,
                height);

            _heart1 = new Rectangle(
                (int)Position.X + (texture.Width * Scale / HeartCount * 0 + Scale * 0),
                (int)Position.Y,
                texture.Width * Scale / HeartCount,
                texture.Height * Scale);

            _heart2 = new Rectangle(
                (int)Position.X + (texture.Width * Scale / HeartCount * 1 + Scale * 1),
                (int)Position.Y,
                texture.Width * Scale / HeartCount,
                texture.Height * Scale);

            _heart3 = new Rectangle(
                (int)Position.X + (texture.Width * Scale / HeartCount * 2 + Scale * 2),
                (int)Position.Y,
                texture.Width * Scale / HeartCount,
                texture.Height * Scale);
        }
        public void Draw(SpriteBatch spriteBatch, int playerHealth)
        {
            switch (playerHealth)
            {
                case 0:
                    spriteBatch.Draw(Texture, _heart1, _emptyHeart, Color);
                    spriteBatch.Draw(Texture, _heart2, _emptyHeart, Color);
                    spriteBatch.Draw(Texture, _heart3, _emptyHeart, Color);
                    break;
                case 1:
                    spriteBatch.Draw(Texture, _heart1, _halfHeart, Color);
                    spriteBatch.Draw(Texture, _heart2, _emptyHeart, Color);
                    spriteBatch.Draw(Texture, _heart3, _emptyHeart, Color);
                    break;
                case 2:
                    spriteBatch.Draw(Texture, _heart1, _fullHeart, Color);
                    spriteBatch.Draw(Texture, _heart2, _emptyHeart, Color);
                    spriteBatch.Draw(Texture, _heart3, _emptyHeart, Color);
                    break;
                case 3:
                    spriteBatch.Draw(Texture, _heart1, _fullHeart, Color);
                    spriteBatch.Draw(Texture, _heart2, _halfHeart, Color);
                    spriteBatch.Draw(Texture, _heart3, _emptyHeart, Color);
                    break;
                case 4:
                    spriteBatch.Draw(Texture, _heart1, _fullHeart, Color);
                    spriteBatch.Draw(Texture, _heart2, _fullHeart, Color);
                    spriteBatch.Draw(Texture, _heart3, _emptyHeart, Color);
                    break;
                case 5:
                    spriteBatch.Draw(Texture, _heart1, _fullHeart, Color);
                    spriteBatch.Draw(Texture, _heart2, _fullHeart, Color);
                    spriteBatch.Draw(Texture, _heart3, _halfHeart, Color);
                    break;
                case 6:
                    spriteBatch.Draw(Texture, _heart1, _fullHeart, Color);
                    spriteBatch.Draw(Texture, _heart2, _fullHeart, Color);
                    spriteBatch.Draw(Texture, _heart3, _fullHeart, Color);
                    break;
            }
        }
    }
}
