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
        public GuiManager(ContentManager content, Texture2D defaultTex)
        {
            _playerHearts = new Hearts(content.Load<Texture2D>("gui/hearts"));
            _playerDamageIndicator = new DamageIndicator(defaultTex)
            {
                Color = new Color(150, 0, 0, 150),
                Position = Vector2.Zero,
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