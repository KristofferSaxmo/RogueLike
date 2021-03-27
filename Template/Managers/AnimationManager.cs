using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Models;
using System;

namespace RogueLike.Managers
{
    public class AnimationManager : ICloneable
    {
        private float _timer;

        public Animation CurrentAnimation { get; private set; }

        public Color Color { get; set; }

        public float Layer { get; set; }

        public Vector2 Origin { get; set; }

        public Vector2 Position { get; set; }

        public int Scale { get; set; }

        public AnimationManager(Animation animation, int scale)
        {
            CurrentAnimation = animation;
            Scale = scale;
        }

        public void Play(Animation animation)
        {
            if (CurrentAnimation == animation)
                return;

            CurrentAnimation = animation;

            CurrentAnimation.CurrentFrame = 0;

            _timer = 0;
        }

        public void Stop()
        {
            _timer = 0f;

            CurrentAnimation.CurrentFrame = 0;

            if (!CurrentAnimation.IsLooping)
                CurrentAnimation = null;
        }

        public void Update(GameTime gameTime, float layer)
        {
            if (CurrentAnimation == null)
                return;

            Layer = layer;

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!(_timer > CurrentAnimation.FrameSpeed)) return;
            _timer = 0f;

            CurrentAnimation.CurrentFrame++;

            if (CurrentAnimation.CurrentFrame < CurrentAnimation.FrameCount) return;

            if (CurrentAnimation.IsLooping)
            {
                CurrentAnimation.CurrentFrame = 0;
                return;
            }

            CurrentAnimation = null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentAnimation == null)
                return;

            spriteBatch.Draw(
              CurrentAnimation.Texture,
              Position,
              new Rectangle(
                CurrentAnimation.CurrentFrame * CurrentAnimation.FrameWidth,
                0,
                CurrentAnimation.FrameWidth,
                CurrentAnimation.FrameHeight
                ),
              Color,
              0f,
              Origin,
              Scale,
              SpriteEffects.None,
              Layer
              );
        }

        public object Clone()
        {
            var animationManager = this.MemberwiseClone() as AnimationManager;

            animationManager.CurrentAnimation = animationManager.CurrentAnimation.Clone() as Animation;

            return animationManager;
        }
    }
}
