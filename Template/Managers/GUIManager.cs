using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Sprites.GUISprites;

namespace RogueLike.Managers
{
    public class GuiManager
    {
        private readonly Hearts _playerHearts;
        private readonly DamageIndicator _playerDamageIndicator;
        public GuiManager(ContentManager content)
        {
            _playerHearts = new Hearts(content.Load<Texture2D>("gui/hearts"), new Vector2(20, 20));
            _playerDamageIndicator = new DamageIndicator(Game1.DefaultTexture, Vector2.Zero)
            {
                Color = new Color(100, 0, 0, 100),
                Rectangle = new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight)
            };
        }
        public void Update(GameTime gameTime, int playerHealth)
        {
            _playerDamageIndicator.Update(gameTime, playerHealth);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, int playerHealth)
        {
            _playerDamageIndicator.Draw(gameTime, spriteBatch);
            _playerHearts.Draw(spriteBatch, playerHealth);

        }
    }
}