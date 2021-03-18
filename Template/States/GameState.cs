using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RogueLike.Managers;
using RogueLike.Models;
using RogueLike.Rooms;
using RogueLike.Sprites;
using RogueLike.Sprites.RoomSprites;
using System.Collections.Generic;
using System.Linq;

namespace RogueLike.States
{
    public class GameState : State
    {
        private Quadtree _quad;
        private SpriteFont _font;
        private RoomManager _roomManager;
        private GUIManager _guiManager;
        private List<Sprite> _sprites;
        private IEnumerable<Sprite> _onScreenSprites;
        private Rectangle _screenRectangle;
        private List<Sprite> _returnSprites = new List<Sprite>();
        private List<Player> _players;
        private Camera _camera;

        public GameState(Game1 game, ContentManager content, Texture2D defaultTex) : base(game, content, defaultTex)
        {

        }
        public override void LoadContent()
        {
            _roomManager = new RoomManager(_content);
            _guiManager = new GUIManager(_content);

            _sprites = new List<Sprite>()
            {
                _roomManager.CreateRoom(
                    new Vector2(0, 0),
                    new Vector2(130, 130)),

                new Player(new Dictionary<string, Animation>()
                {
                    { "WalkLeft", new Animation(_content.Load<Texture2D>("player/player_run_left"), 4, 0.1f) },
                    { "WalkRight", new Animation(_content.Load<Texture2D>("player/player_run_right"), 4, 0.1f) },
                    { "IdleLeft", new Animation(_content.Load<Texture2D>("player/player_idle_left"), 5, 0.2f) },
                    { "IdleRight", new Animation(_content.Load<Texture2D>("player/player_idle_right"), 5, 0.2f) },
                    { "AttackRight1", new Animation(_content.Load<Texture2D>("player/player_attack_right1"), 6, 0.04f) { IsLooping = false } },
                    { "AttackRight2", new Animation(_content.Load<Texture2D>("player/player_attack_right2"), 6, 0.04f) { IsLooping = false } },
                    { "AttackRight3", new Animation(_content.Load<Texture2D>("player/player_attack_right3"), 5, 0.1f) { IsLooping = false } },
                    { "AttackLeft1", new Animation(_content.Load<Texture2D>("player/player_attack_left1"), 6, 0.04f) { IsLooping = false } },
                    { "AttackLeft2", new Animation(_content.Load<Texture2D>("player/player_attack_left2"), 6, 0.04f) { IsLooping = false } },
                    { "AttackLeft3", new Animation(_content.Load<Texture2D>("player/player_attack_left3"), 5, 0.1f) { IsLooping = false } }
                })
                {
                    Health = 6,
                    Position = new Vector2(_roomManager.CurrentRoom.Area.X + _roomManager.CurrentRoom.Area.Center.X, _roomManager.CurrentRoom.Area.Y + _roomManager.CurrentRoom.Area.Center.Y),
                    Speed = 7,
                    Input = new Input()
                    {
                        Left = Keys.A,
                        Right = Keys.D,
                        Up = Keys.W,
                        Down = Keys.S
                    },
                },
            };
            _quad = new Quadtree(0, _roomManager.CurrentRoom.Area);
            _players = _sprites.Where(c => c is Player).Select(c => (Player)c).ToList();

            AddChildren();
        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_game, _content, _defaultTex));

            if(_players[0].IsDead)
                _game.ChangeState(new MenuState(_game, _content, _defaultTex));

            _screenRectangle = new Rectangle((int)_players[0].Position.X - Game1.ScreenWidth / 2 - 100,
                                             (int)_players[0].Position.Y - Game1.ScreenHeight / 2 - 100,
                                             Game1.ScreenWidth + 200,
                                             Game1.ScreenHeight + 200);

            foreach (var sprite in _sprites)
                sprite.Update(gameTime);

            _onScreenSprites = _sprites.Where(sprite => sprite.Rectangle.Intersects(_screenRectangle));

            DetectCollisions();

            RemoveSprites();

            _guiManager.Update(gameTime);
        }
        public void DetectCollisions()
        {
            var collidableSprites = _onScreenSprites.Where(c => c is ICollidable);

            foreach (var sprite in collidableSprites)
            {
                ((ICollidable)sprite).UpdateHitbox();
            }

            _quad.Clear();
            foreach (var sprite in collidableSprites)
            {
                _quad.Insert(sprite);
            }

            foreach (var spriteA in collidableSprites)
            {
                _returnSprites.Clear();
                _quad.Retrieve(_returnSprites, spriteA);

                foreach (var spriteB in _returnSprites)
                {
                    if (spriteA == spriteB)
                        continue;

                    if (spriteA.Parent == spriteB.Parent)
                        continue;

                    if (spriteA.Layer == spriteB.Layer)
                        spriteA.Position = new Vector2(spriteA.Position.X, spriteA.Position.Y);

                    if (spriteA.Intersects(spriteB))
                        ((ICollidable)spriteA).OnCollide(spriteB);
                }
                spriteA.Position += spriteA.Velocity;
            }
        }
        public void AddChildren()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];
                foreach (var child in sprite.Children)
                    _sprites.Add(child);

                sprite.Children = new List<Sprite>();
            }
        }
        public void RemoveSprites()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }
        public override void UpdateCamera(Camera camera)
        {
            camera.Follow(_players[0]);
            _camera = camera;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, Camera.Transform);

            spriteBatch.Draw(_defaultTex, _roomManager.CurrentRoom.GrassRec, Room.Grass);
            if (_roomManager.CurrentRoom.IsWater)
            {
                spriteBatch.Draw(_defaultTex, _roomManager.CurrentRoom.GrassRec2, Room.Grass);
            }

            foreach (var sprite in _onScreenSprites)
            {
                sprite.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);

            _guiManager.Draw(gameTime, spriteBatch, _players[0].Health);

            spriteBatch.End();

        }
    }
}
