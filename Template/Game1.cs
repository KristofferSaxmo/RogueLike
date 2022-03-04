using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.States;
using System;

namespace RogueLike
{
    public class Game1 : Game
    {
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        public static Random Random = new Random();
        public static Texture2D Bit8Texture;
        public static Texture2D DefaultTexture;

        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;

        private State _currentState;
        private State _nextState;

        private Camera _camera;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();

            _camera = new Camera();

            IsMouseVisible = true;

            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            DefaultTexture = new Texture2D(GraphicsDevice, 1, 1);
            DefaultTexture.SetData(new Color[1] { Color.White });

            Bit8Texture = new Texture2D(GraphicsDevice, 8, 8);

            _currentState = new MenuState(this, Content);
            _currentState.LoadContent();
            _nextState = null;
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                _currentState.LoadContent();

                _nextState = null;
            }

            _currentState.UpdateCamera(_camera);

            _currentState.Update(gameTime);

            base.Update(gameTime);
        }
        public void ChangeState(State state)
        {
            _nextState = state;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _currentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}
