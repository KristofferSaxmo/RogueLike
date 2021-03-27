using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Sprites.GUISprites;

namespace RogueLike.Managers
{
    public class GuiManager
    {
        private readonly Hearts _hearts;
        public GuiManager(ContentManager content)
        {
            _hearts = new Hearts(content.Load<Texture2D>("gui/hearts"));
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch, int playerHealth)
        {
            _hearts.Draw(spriteBatch, playerHealth);
        }
    }
}