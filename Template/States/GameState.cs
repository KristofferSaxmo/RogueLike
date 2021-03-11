﻿using Microsoft.Xna.Framework;
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
        private Camera _camera;

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
                    Position = new Vector2(900, 600),
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
        public override void UpdateCamera(Camera camera)
        {
            foreach(Player player in _players)
            {
                camera.Follow(player);
            }
            _camera = camera;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, _camera.Transform);

            foreach (var sprite in _sprites)
            {
                sprite.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
