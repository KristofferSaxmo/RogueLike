using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Managers;
using RogueLike.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RogueLike.Sprites
{
    public class Sprite : Component, ICloneable
    {
        #region Fields

        protected Dictionary<string, Animation> _animations;
        protected Animation _animation;
        protected AnimationManager _animationManager;
        protected Shadow _shadow;
        protected Texture2D Texture;
        protected Rectangle _hurtbox;
        protected Rectangle _rectangle;
        protected int _scale = 3;
        protected Vector2 _origin;
        protected Vector2 _position;

        #endregion

        #region Properties

        public List<Sprite> Children { get; set; }
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }
        public Vector2 Origin
        {
            get { return _origin; }
            set
            {
                _origin = value;

                if (_animationManager != null)
                    _animationManager.Origin = _origin;
            }
        }
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int LayerOrigin { get; set; }
        public Rectangle LayerOriginTestRectangle // Shows the Y position of LayerOrigin. Only for testing
        {
            get
            {
                return new Rectangle((int)Position.X - 100, (int)Position.Y + LayerOrigin, 200, 1);
            }
        }
        public float Layer
        {
            get
            {
                return MathHelper.Clamp((100000 + Position.Y + LayerOrigin) / 10000000, 0.0f, 1.0f);
            }
        }
        public Color Color { get; set; }
        public bool IsRemoved { get; protected set; }
        public int Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        public Rectangle Rectangle
        {
            get
            {
                if (Texture != null)
                {
                    if (Texture.Width > 5 && Texture.Height > 5)
                    {
                        return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width * Scale, Texture.Height * Scale);
                    }
                }

                if (_animationManager != null)
                {
                    if (_animation != null)
                        return new Rectangle((int)Position.X, (int)Position.Y, _animation.FrameWidth * Scale, _animation.FrameHeight * Scale);

                    var animation = _animations.FirstOrDefault().Value;

                    return new Rectangle((int)Position.X, (int)Position.Y, animation.FrameWidth * Scale, animation.FrameHeight * Scale);
                }

                    return new Rectangle(_rectangle.X - (int)Origin.X * Scale, (int)_rectangle.Y - (int)Origin.Y * Scale, _rectangle.Width, _rectangle.Height);
            }
            set => _rectangle = value;
        }

        public Rectangle Hurtbox
        {
            get { return new Rectangle(_hurtbox.X - (int)Origin.X * Scale, _hurtbox.Y - (int)Origin.Y * Scale, _hurtbox.Width, _hurtbox.Height); }
        }
        public Sprite Parent;

        #endregion

        #region Methods

        public Sprite(Texture2D texture)
        {
            Texture = texture;

            Children = new List<Sprite>();

            Color = Color.White;

            if (texture != null)
                Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
        } // Sprite
        public Sprite(Texture2D texture, Texture2D shadowTexture)
        {
            Texture = texture;

            Children = new List<Sprite>
            {
                (_shadow = new Shadow(shadowTexture)
                {
                    Parent = this
                })
            };

            Color = Color.White;

            if (texture != null)
                Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
        } // Sprite with shadow
        public Sprite(Animation animation)
        {
            Texture = null;

            Children = new List<Sprite>();

            _animation = animation;

            _animationManager = new AnimationManager(animation, Scale) { Color = Color.White };

            Origin = new Vector2(animation.FrameWidth / 2, animation.FrameHeight / 2);
        } // Animated sprite
        public Sprite(Dictionary<string, Animation> animations)
        {
            Texture = null;

            Children = new List<Sprite>();

            _animations = animations;

            var animation = _animations.FirstOrDefault().Value;

            _animationManager = new AnimationManager(animation, Scale) { Color = Color.White };

            Origin = new Vector2(animation.FrameWidth / 2, animation.FrameHeight / 2);
        } // Animated sprite dictionary
        public Sprite(Dictionary<string, Animation> animations, Texture2D shadowTexture)
        {
            Texture = null;

            Children = new List<Sprite>
            {
                (_shadow = new Shadow(shadowTexture)
                {
                    Parent = this
                })
            };

            _animations = animations;

            var animation = _animations.FirstOrDefault().Value;

            _animationManager = new AnimationManager(animation, Scale) { Color = Color.White };

            Origin = new Vector2(animation.FrameWidth / 2, animation.FrameHeight / 2);
        } // Animated sprite dictionary with shadow
        public Sprite()
        {
            Children = new List<Sprite>();
        }

        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                spriteBatch.Draw(Texture, Rectangle, null, Color, 0f, Origin, SpriteEffects.None, Layer);
            }

            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
        }

        public object Clone()
        {
            var sprite = this.MemberwiseClone() as Sprite;

            if (_animations != null)
            {
                sprite._animations = this._animations.ToDictionary(c => c.Key, v => v.Value.Clone() as Animation);
                sprite._animationManager = sprite._animationManager.Clone() as AnimationManager;
            }

            return sprite;
        }

        #region Collision

        public bool Intersects(Sprite sprite)
        {
            if (Velocity.X > 0 && IsTouchingLeft(sprite))
            {
                Velocity = new Vector2(sprite.Hurtbox.Left - Hurtbox.Right, Velocity.Y);
                return true;
            }

            if (Velocity.X < 0 & IsTouchingRight(sprite))
            {
                Velocity = new Vector2(sprite.Hurtbox.Right - Hurtbox.Left, Velocity.Y);
                return true;
            }

            if (Velocity.Y > 0 && IsTouchingTop(sprite))
            {
                Velocity = new Vector2(Velocity.X, sprite.Hurtbox.Top - Hurtbox.Bottom);
                return true;
            }
            if (Velocity.Y < 0 & IsTouchingBottom(sprite))
            {
                Velocity = new Vector2(Velocity.X, sprite.Hurtbox.Bottom - Hurtbox.Top);
                return true;
            }

            return Hurtbox.Intersects(sprite.Hurtbox);
        }

        private bool IsTouchingLeft(Sprite sprite)
        {
            return Hurtbox.Right + Velocity.X > sprite.Hurtbox.Left &&
              Hurtbox.Left < sprite.Hurtbox.Left &&
              Hurtbox.Bottom > sprite.Hurtbox.Top &&
              Hurtbox.Top < sprite.Hurtbox.Bottom;
        }

        private bool IsTouchingRight(Sprite sprite)
        {
            return Hurtbox.Left + Velocity.X < sprite.Hurtbox.Right &&
              Hurtbox.Right > sprite.Hurtbox.Right &&
              Hurtbox.Bottom > sprite.Hurtbox.Top &&
              Hurtbox.Top < sprite.Hurtbox.Bottom;
        }

        private bool IsTouchingTop(Sprite sprite)
        {
            return Hurtbox.Bottom + Velocity.Y > sprite.Hurtbox.Top &&
              Hurtbox.Top < sprite.Hurtbox.Top &&
              Hurtbox.Right > sprite.Hurtbox.Left &&
              Hurtbox.Left < sprite.Hurtbox.Right;
        }

        private bool IsTouchingBottom(Sprite sprite)
        {
            return Hurtbox.Top + Velocity.Y < sprite.Hurtbox.Bottom &&
              Hurtbox.Bottom > sprite.Hurtbox.Bottom &&
              Hurtbox.Right > sprite.Hurtbox.Left &&
              Hurtbox.Left < sprite.Hurtbox.Right;
        }

        #endregion

        #endregion
    }
}