using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RogueLike.Input;
using System;

namespace RogueLike.Controls
{
    public class Button : Component
    {
        #region Fields

        private bool _isHovering;

        #endregion

        #region Properties
        public EventHandler Click;
        public Text Text { get; private set; }
        public bool Clicked { get; private set; }
        public Vector2 Position => Text.Position;
        public Rectangle Rectangle => Text.Rectangle;

        #endregion

        #region Methods

        public Button(Text text)
        {
            Text = text;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Text.Color = Color.White;

            if (_isHovering)
                Text.Color = Color.DarkTurquoise;

            Text.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            FlatMouse mouse = FlatMouse.Instance;
            Text.Update(gameTime);

            var mouseRectangle = new Rectangle((int)mouse.GetRelativePosition().X, (int)mouse.GetRelativePosition().Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (mouse.IsLeftButtonClicked())
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        #endregion
    }
}
