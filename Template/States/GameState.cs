using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RogueLike.Managers;
using RogueLike.Models;
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
        private Camera _camera;

        public GameState(Game1 game, ContentManager content) : base(game, content)
        {

        }
        public override void LoadContent()
        {
            _sprites = new List<Sprite>()
            {
                new Player(new Dictionary<string, Animation>()
                {
                    { "WalkLeft", new Animation(_content.Load<Texture2D>("player/player_walk_left"), 4) },
                    { "WalkRight", new Animation(_content.Load<Texture2D>("player/player_walk_right"), 4) },
                    { "IdleLeft", new Animation(_content.Load<Texture2D>("player/player_idle_left"), 1) },
                    { "IdleRight", new Animation(_content.Load<Texture2D>("player/player_idle_right"), 1) }
                })
                {
                    Health = 3,
                    Position = new Vector2(900, 200),
                    Speed = 3,
                    Input = new Input()
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

            _sprites.Add(_roomManager.CreateRoom()); // Test
        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_game, _content));

            foreach (var sprite in _sprites)
                sprite.Update(gameTime);

            DetectCollisions();

            AddChildren();

            RemoveSprites();
        }
        public void DetectCollisions()
        {
            var collidableSprites = _sprites.Where(c => c is ICollidable);

            foreach (var sprite in collidableSprites)
            {
                ((ICollidable)sprite).UpdateHitbox();
            }

            foreach (var spriteA in collidableSprites)
            {
                foreach (var spriteB in collidableSprites)
                {
                    // Don't do anything if they're the same sprite!
                    if (spriteA == spriteB)
                        continue;

                    if (!spriteA.CollisionArea.Intersects(spriteB.CollisionArea))
                        continue;

                    if (spriteA.Intersects(spriteB))
                        ((ICollidable)spriteA).OnCollide(spriteB);
                }
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
