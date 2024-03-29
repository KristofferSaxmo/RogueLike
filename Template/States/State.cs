﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RogueLike.States
{
    public abstract class State
    {
        protected Game1 _game;

        protected ContentManager _content;

        public State(Game1 game, ContentManager content)
        {
            _game = game;

            _content = content;
        }
        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void UpdateCamera(Camera camera);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    }
}
