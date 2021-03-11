using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace RogueLike.Sprites
{
    public class Sprite : Component
    {
        #region Fields
        protected Texture2D _texture;
        protected Rectangle _hitbox;
        protected int _scale;
        #endregion

        #region Properties
        public List<Sprite> Children { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Velocity { get; set; }
        public float Rotation { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public float Layer { get; set; }
        public bool IsRemoved { get; set; }
        public int Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        public Rectangle Hitbox
        {
            get { return _hitbox; }
        }
        public Rectangle Rectangle
        {
            get
            {
                if (_texture != null)
                {
                    return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, _texture.Width * Scale, _texture.Height * Scale);
                }

                throw new Exception("Unknown sprite");
            }
        }
        public Rectangle CollisionArea
        {
            get
            {
                return new Rectangle(Hitbox.X, Hitbox.Y, MathHelper.Max(Hitbox.Width, Hitbox.Height), MathHelper.Max(Hitbox.Width, Hitbox.Height));
            }
        }
        public Sprite Parent;
        #endregion

        #region Methods
        public Sprite(Texture2D texture)
        {
            _texture = texture;

            Children = new List<Sprite>();

            if (texture != null)
                Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }
        public Sprite()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, Origin, 1f, SpriteEffects.None, Layer);
        }

        #region Collision
        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Hitbox.Right + this.Velocity.X > sprite.Hitbox.Left &&
              this.Hitbox.Left < sprite.Hitbox.Left &&
              this.Hitbox.Bottom > sprite.Hitbox.Top &&
              this.Hitbox.Top < sprite.Hitbox.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Hitbox.Left + this.Velocity.X < sprite.Hitbox.Right &&
              this.Hitbox.Right > sprite.Hitbox.Right &&
              this.Hitbox.Bottom > sprite.Hitbox.Top &&
              this.Hitbox.Top < sprite.Hitbox.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Hitbox.Bottom + this.Velocity.Y > sprite.Hitbox.Top &&
              this.Hitbox.Top < sprite.Hitbox.Top &&
              this.Hitbox.Right > sprite.Hitbox.Left &&
              this.Hitbox.Left < sprite.Hitbox.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Hitbox.Top + this.Velocity.Y < sprite.Hitbox.Bottom &&
              this.Hitbox.Bottom > sprite.Hitbox.Bottom &&
              this.Hitbox.Right > sprite.Hitbox.Left &&
              this.Hitbox.Left < sprite.Hitbox.Right;
        }
        #endregion

        #endregion
    }
}