using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Sprites;
using RogueLike.Sprites.GUISprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Managers
{
    public class GUIManager
    {
        private Hearts _hearts;
        public GUIManager(ContentManager content)
        {
            _hearts = new Hearts(content.Load<Texture2D>("gui/hearts"));
        }
        public void Update(GameTime gameTime)
        {
            
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, int playerHealth)
        {
            _hearts.Draw(spriteBatch, playerHealth);
        }
    }
}
