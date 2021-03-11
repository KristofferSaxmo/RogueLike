using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Sprites;

namespace RogueLike
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Sprite target)
        {
            var position = Matrix.CreateTranslation(
              -target.Position.X - (target.Rectangle.Width / 2),
              -target.Position.Y - (target.Rectangle.Height / 2),
              0);

            var offset = Matrix.CreateTranslation(
                Game1.ScreenWidth / 2,
                Game1.ScreenHeight / 2,
                0);

            Transform = position * offset;
        }
        public Vector2 GetWorldPosition(Vector2 screenPosition)
        {
            Matrix inverseTransform = Matrix.Invert(Transform);
            return Vector2.Transform(new Vector2(screenPosition.X, screenPosition.Y), inverseTransform);
        }
    }
}
