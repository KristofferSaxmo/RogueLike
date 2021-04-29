using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Sprites.GUISprites
{
    class DamageIndicator : Sprite
    {
        int _previousPlayerHealth = 0;
        Color colorCopy;

        public DamageIndicator(Texture2D texture) : base(texture)
        {

        }

        public void Update(GameTime gameTime, int playerHealth)
        {
            if (colorCopy == Color.Transparent)
            {
                colorCopy = Color;
                Color = Color.Transparent;
            }

            if (playerHealth < _previousPlayerHealth)
            {
                Color = colorCopy;
                _previousPlayerHealth = playerHealth;
                return;
            }

            _previousPlayerHealth = playerHealth;

            if (Color.A == 0) return;

            //Color = new Color(Color.R, Color.G, Color.B, Color.A * 0.95f); // Slowly reduce alpha

            Color *= 0.985f;

            if (Color.A < 1)
                Color = new Color(Color.R, Color.G, Color.B, 0f);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
