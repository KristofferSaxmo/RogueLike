using Microsoft.Xna.Framework;
using RogueLike.Sprites;

namespace RogueLike
{
    public class Camera
    {
        public static Matrix Transform { get; private set; }

        public void SetCamera(Vector2 position)
        {
            Matrix positionMatrix = Matrix.CreateTranslation(
              -position.X,
              -position.Y,
              0);

            var offset = Matrix.CreateTranslation(
                Game1.ScreenWidth / 2,
                Game1.ScreenHeight / 2,
                0);

            Transform = positionMatrix * offset;
        }
        public static Vector2 GetRelativePosition(Vector2 screenPosition)
        {
            Matrix inverseTransform = Matrix.Invert(Transform);
            return Vector2.Transform(new Vector2(screenPosition.X, screenPosition.Y), inverseTransform);
        }
    }
}
