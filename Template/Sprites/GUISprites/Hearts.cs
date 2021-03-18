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
        private Rectangle _fullHeart, _halfHeart, _emptyHeart;
        private Rectangle _heart1, _heart2, _heart3;
        private int heartCount = 3;
        public Hearts(Texture2D texture) : base(texture)
        {
            Scale = 5;
            Position = new Vector2(20, 20);

            int width = _texture.Width / heartCount;
            int height = _texture.Height;
            int row = heartCount;
            int column = heartCount;

            _fullHeart = new Rectangle(
                width * (0 % column),
                height * (0 / row),
                width,
                height);

            _halfHeart = new Rectangle(
                width * (1 % column),
                height * (1 / row),
                width,
                height);

            _emptyHeart = new Rectangle(
                width * (2 % column),
                height * (2 / row),
                width,
                height);

            _heart1 = new Rectangle(
                (int)Position.X + (texture.Width * Scale / heartCount * 0),
                (int)Position.Y,
                texture.Width * Scale / heartCount,
                texture.Height * Scale);

            _heart2 = new Rectangle(
                (int)Position.X + (texture.Width * Scale / heartCount * 1),
                (int)Position.Y,
                texture.Width * Scale / heartCount,
                texture.Height * Scale);

            _heart3 = new Rectangle(
                (int)Position.X + (texture.Width * Scale / heartCount * 2),
                (int)Position.Y,
                texture.Width * Scale / heartCount,
                texture.Height * Scale);
        }
        public void Draw(SpriteBatch spriteBatch, int playerHealth)
        {
            switch (playerHealth)
            {
                case 0:
                    spriteBatch.Draw(_texture, _heart1, _emptyHeart, Color);
                    spriteBatch.Draw(_texture, _heart2, _emptyHeart, Color);
                    spriteBatch.Draw(_texture, _heart3, _emptyHeart, Color);
                    break;
                case 1:
                    spriteBatch.Draw(_texture, _heart1, _halfHeart, Color);
                    spriteBatch.Draw(_texture, _heart2, _emptyHeart, Color);
                    spriteBatch.Draw(_texture, _heart3, _emptyHeart, Color);
                    break;
                case 2:
                    spriteBatch.Draw(_texture, _heart1, _fullHeart, Color);
                    spriteBatch.Draw(_texture, _heart2, _emptyHeart, Color);
                    spriteBatch.Draw(_texture, _heart3, _emptyHeart, Color);
                    break;
                case 3:
                    spriteBatch.Draw(_texture, _heart1, _fullHeart, Color);
                    spriteBatch.Draw(_texture, _heart2, _halfHeart, Color);
                    spriteBatch.Draw(_texture, _heart3, _emptyHeart, Color);
                    break;
                case 4:
                    spriteBatch.Draw(_texture, _heart1, _fullHeart, Color);
                    spriteBatch.Draw(_texture, _heart2, _fullHeart, Color);
                    spriteBatch.Draw(_texture, _heart3, _emptyHeart, Color);
                    break;
                case 5:
                    spriteBatch.Draw(_texture, _heart1, _fullHeart, Color);
                    spriteBatch.Draw(_texture, _heart2, _fullHeart, Color);
                    spriteBatch.Draw(_texture, _heart3, _halfHeart, Color);
                    break;
                case 6:
                    spriteBatch.Draw(_texture, _heart1, _fullHeart, Color);
                    spriteBatch.Draw(_texture, _heart2, _fullHeart, Color);
                    spriteBatch.Draw(_texture, _heart3, _fullHeart, Color);
                    break;
            }
        }
    }
}
