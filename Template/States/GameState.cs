using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RogueLike.Managers;
using RogueLike.Sprites;
using System.Collections.Generic;
using System.Linq;

namespace RogueLike.States
{
    public class GameState : State
    {
        private SpriteFont _font;
        private RoomManager _roomManager;
        private List<Sprite> _sprites;
        private List<Player> _players;
        private bool test = true;

        public GameState(Game1 game, ContentManager content) : base(game, content)
        {

        }
        public override void LoadContent()
        {
            Texture2D playerSheet = _content.Load<Texture2D>("playerSheet");

            _sprites = new List<Sprite>()
            {
                new Player(playerSheet)
                {
                    Layer = 0.5f,
                    Health = 3,
                    Position = Vector2.Zero,
                    Scale = 3,
                    Speed = 3,
                    Input = new Models.Input()
                    {
                        Left = Keys.A,
                        Right = Keys.D,
                        Up = Keys.W,
                        Down = Keys.S,
                    },
                },
            };

            _players = _sprites.Where(c => c is Player).Select(c => (Player)c).ToList();

            _roomManager = new RoomManager(_content);
        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_game, _content));

            foreach (var sprite in _sprites)
                sprite.Update(gameTime);

            var collidableSprites = _sprites.Where(c => c is ICollidable);

            if (test)
            {
                _sprites.Add(_roomManager.CreateRoom());
                test = false;
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, null);

            foreach (var sprite in _sprites)
            {
                sprite.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
