using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RogueLike.Interfaces;
using RogueLike.Managers;
using RogueLike.Models;
using RogueLike.Rooms;
using RogueLike.Sprites;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RogueLike.States
{
    public class GameState : State
    {
        #region Fields

        private KeyboardState _previousKeyboardState;
        private bool _showHurtboxes = false;
        private Quadtree _quad;
        private RoomManager _roomManager;
        private GuiManager _guiManager;
        private EnemyManager _enemyManager;
        private List<Sprite> _sprites;
        private IEnumerable<Sprite> _onScreenSprites;
        private Rectangle _screenRectangle;
        private readonly List<Sprite> _returnSprites = new List<Sprite>();
        private List<Player> _players;
        private int _score;
        private BinaryWriter _bw;
        private BinaryReader _br;

        #endregion

        public GameState(Game1 game, ContentManager content, Texture2D defaultTex) : base(game, content, defaultTex)
        {

        }

        public override void LoadContent()
        {
            if (File.Exists("Score.bin"))
            {
                _br = new BinaryReader(new FileStream("Score.bin", FileMode.OpenOrCreate, FileAccess.Read));
                if(_br.BaseStream.Length > 0)
                    _score = _br.ReadInt32();

                _br.Close();
            }
            _roomManager = new RoomManager(_content);
            _guiManager = new GuiManager(_content, _defaultTex);
            _enemyManager = new EnemyManager(_content);

            _sprites = new List<Sprite>()
            {
                _roomManager.CreateRoom(
                    new Vector2(0, 0),
                    new Vector2(20, 5), // Room size
                    _enemyManager),

                new Player(new Dictionary<string, Animation>()
                {
                    { "WalkLeft", new Animation(_content.Load<Texture2D>("player/player_run_left"), 4, 0.1f) },
                    { "WalkRight", new Animation(_content.Load<Texture2D>("player/player_run_right"), 4, 0.1f) },
                    { "IdleLeft", new Animation(_content.Load<Texture2D>("player/player_idle_left"), 5, 0.2f) },
                    { "IdleRight", new Animation(_content.Load<Texture2D>("player/player_idle_right"), 5, 0.2f) },
                    { "AttackLeft1", new Animation(_content.Load<Texture2D>("player/player_attack_left1"), 6, 0.04f) { IsLooping = false } },
                    { "AttackLeft2", new Animation(_content.Load<Texture2D>("player/player_attack_left2"), 6, 0.04f) { IsLooping = false } },
                    { "AttackLeft3", new Animation(_content.Load<Texture2D>("player/player_attack_left3"), 5, 0.1f) { IsLooping = false } },
                    { "AttackRight1", new Animation(_content.Load<Texture2D>("player/player_attack_right1"), 6, 0.04f) { IsLooping = false } },
                    { "AttackRight2", new Animation(_content.Load<Texture2D>("player/player_attack_right2"), 6, 0.04f) { IsLooping = false } },
                    { "AttackRight3", new Animation(_content.Load<Texture2D>("player/player_attack_right3"), 5, 0.1f) { IsLooping = false } }
                })
                {
                    Health = 6,
                    Position = new Vector2(_roomManager.CurrentRoom.Area.X + _roomManager.CurrentRoom.Area.Center.X, _roomManager.CurrentRoom.Area.Y + _roomManager.CurrentRoom.Area.Height - 300),
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
            _players = _sprites.OfType<Player>().ToList();
        }

        private void DetectCollisions()
        {

            var hurtboxSprites = _onScreenSprites.Where(c => c is IDamageable) as Sprite[] ?? _onScreenSprites.Where(c => c is IDamageable).ToArray();
            foreach (var sprite in hurtboxSprites)
            {
                ((IDamageable)sprite).UpdateHurtbox();
            }

            foreach (var sprite in _onScreenSprites.Where(c => c is IDamaging))
            {
                ((IDamaging)sprite).UpdateHitbox();
            }

            var hitboxes = _onScreenSprites.Where(c => c is Hitbox);

            UseQuadtree(hurtboxSprites, hitboxes);
        }

        private void UseQuadtree(Sprite[] hurtboxSprites, IEnumerable<Sprite> hitboxes)
        {
            _quad.Clear();

            foreach (var sprite in hurtboxSprites)
            {
                _quad.Insert(sprite);
            }

            foreach (var sprite in hitboxes)
            {
                _quad.Insert(sprite);
            }

            foreach (var spriteA in hurtboxSprites)
            {
                _returnSprites.Clear();

                _quad.Retrieve(_returnSprites, spriteA);

                foreach (var spriteB in _returnSprites)
                {
                    if (!(spriteB is IDamageable))
                        continue;

                    if (spriteA == spriteB)
                        continue;

                    if (spriteA.Parent is Room && spriteB.Parent is Room)
                    {
                        if (!(spriteA is Enemy && spriteB is Enemy)) // Enemies can collide with each other
                            continue;
                    }

                    spriteA.Intersects(spriteB);
                }

                foreach (var spriteC in _returnSprites)
                {
                    if (!(spriteC is Hitbox))
                        continue;

                    if (spriteA == spriteC)
                        continue;

                    if (spriteC.Rectangle.Intersects(spriteA.Hurtbox))
                    {
                        ((IDamageable)spriteA).OnCollide((Hitbox)spriteC);

                        if(spriteA is Hitbox && spriteC is Enemy)
                        {
                            _score++;
                            _bw = new BinaryWriter(new FileStream("Score.bin", FileMode.OpenOrCreate, FileAccess.Write));
                            _bw.Write(_score);
                            _bw.Close();
                        }
                    }
                }

                spriteA.Position += spriteA.Velocity;
            }
        }

        private void AddChildren()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];
                foreach (var child in sprite.Children)
                    _sprites.Add(child);

                sprite.Children = new List<Sprite>();
            }
        }

        private void RemoveSprites()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (!_sprites[i].IsRemoved) continue;

                _sprites.RemoveAt(i);
                i--;
            }
        }

        public override void UpdateCamera(Camera camera)
        {
            camera.Follow(_players[0]);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_game, _content, _defaultTex));

            if (_players[0].IsDead)
                _game.ChangeState(new MenuState(_game, _content, _defaultTex));

            _screenRectangle = new Rectangle((int)_players[0].Position.X - Game1.ScreenWidth / 2 - 100,
                                             (int)_players[0].Position.Y - Game1.ScreenHeight / 2 - 100,
                                             Game1.ScreenWidth + 200,
                                             Game1.ScreenHeight + 200);

            foreach (var sprite in _sprites)
                sprite.Update(gameTime);

            foreach (Enemy enemy in _sprites.Where(sprite => sprite is Enemy))
            {
                enemy.Update(gameTime, _players[0].Position);
            }

            _onScreenSprites = _sprites.Where(sprite => sprite.Rectangle.Intersects(_screenRectangle));

            AddChildren();

            DetectCollisions();

            RemoveSprites();

            _guiManager.Update(gameTime, _players[0].Health);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !_previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                _showHurtboxes = !_showHurtboxes;
            }

            _previousKeyboardState = Keyboard.GetState();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, Camera.Transform);

            spriteBatch.Draw(_defaultTex, _roomManager.CurrentRoom.GrassRec, Room.Grass);

            if (_roomManager.CurrentRoom.IsWater)
            {
                spriteBatch.Draw(_defaultTex, _roomManager.CurrentRoom.GrassRec2, Room.Grass);
            }

            //Shows Hurtboxes. Only for testing
            if (_showHurtboxes)
            {
                foreach (var sprite1 in _onScreenSprites.Where(c => c is IDamageable))
                {
                    var sprite = (IDamageable)sprite1;
                    spriteBatch.Draw(_defaultTex, ((Sprite)sprite).Hurtbox, Color.Blue);
                }
            }

            else
            {
                foreach (var sprite in _onScreenSprites)
                {
                    sprite.Draw(gameTime, spriteBatch);

                    // Shows the Y position of LayerOrigin. Only for testing
                    //spriteBatch.Draw(_defaultTex, sprite.LayerOriginTestRectangle, Color.Black); 
                }
            }

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);

            _guiManager.Draw(gameTime, spriteBatch, _players[0].Health);

            spriteBatch.End();

        }
    }
}
