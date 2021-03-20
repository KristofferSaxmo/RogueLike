using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLike.Models;

namespace RogueLike.Managers
{
    public class AnimationManager : ICloneable
    {
        private Animation _animation;

        private float _timer;

        public Animation CurrentAnimation
        {
            get
            {
                return _animation;
            }
        }

        public float Layer { get; set; }

        public Vector2 Origin { get; set; }

        public Vector2 Position { get; set; }

        public int Scale { get; set; }

        public AnimationManager(Animation animation, int scale)
        {
            _animation = animation;
            Scale = scale;
        }

        public void Play(Animation animation)
        {
            if (_animation == animation)
                return;

            _animation = animation;

            _animation.CurrentFrame = 0;

            _timer = 0;
        }

        public void Stop()
        {
            _timer = 0f;

            _animation.CurrentFrame = 0;

            if (!_animation.IsLooping)
                _animation = null;
        }

        public void Update(GameTime gameTime, float layer)
        {
            Layer = layer;

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;

                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                {
                    if (_animation.IsLooping)
                        _animation.CurrentFrame = 0;
                    else
                        _animation = null;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_animation == null)
                return;

            spriteBatch.Draw(
              _animation.Texture,
              Position,
              new Rectangle(
                _animation.CurrentFrame * _animation.FrameWidth,
                0,
                _animation.FrameWidth,
                _animation.FrameHeight
                ),
              Color.White,
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

            animationManager._animation = animationManager._animation.Clone() as Animation;

            return animationManager;
        }
    }
}
