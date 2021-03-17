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

        public float Rotation { get; set; }

        public int Scale { get; set; }

        public AnimationManager(Animation animation, int scale)
        {
            _animation = animation;
            Scale = scale;
            _animation.Size = new Point(_animation.Texture.Width * Scale / _animation.FrameCount, _animation.Texture.Height * Scale);
        }

        public void Play(Animation animation)
        {
            if (_animation == animation)
                return;

            _animation = animation;

            _animation.Size = new Point(_animation.Texture.Width * Scale / _animation.FrameCount, _animation.Texture.Height * Scale);

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

            int width = _animation.Texture.Width / _animation.FrameCount;
            int height = _animation.Texture.Height;
            int row = _animation.CurrentFrame / _animation.FrameCount;
            int column = _animation.CurrentFrame % _animation.FrameCount;

            Rectangle sourceRectangle = new Rectangle(
                width * column,
                height * row,
                width,
                height);

            Rectangle destinationRectangle = new Rectangle(Position.ToPoint(), _animation.Size);

            spriteBatch.Draw(_animation.Texture, destinationRectangle, sourceRectangle, Color.White, Rotation, Origin, SpriteEffects.None, Layer);
        }

        public object Clone()
        {
            var animationManager = this.MemberwiseClone() as AnimationManager;

            animationManager._animation = animationManager._animation.Clone() as Animation;

            return animationManager;
        }
    }
}
