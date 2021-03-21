using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Models;
using RogueLike.Rooms;
using RogueLike.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Managers
{
    public class EnemyManager
    {
        private Texture2D _ghostShadow;
        private Dictionary<string, Animation> _ghostAnimations;
        private Enemy _enemyPrefab;
        
        public EnemyManager(ContentManager content)
        {
            _ghostShadow = content.Load<Texture2D>("enemies/ghost/ghost-shadow");
            
            _ghostAnimations = new Dictionary<string, Animation>()
            {
                { "GhostLeft", new Animation(content.Load<Texture2D>("enemies/ghost/ghost_left"), 5, 0.15f) },
                { "GhostRight", new Animation(content.Load<Texture2D>("enemies/ghost/ghost_right"), 5, 0.15f) },
                { "GhostAttackLeft", new Animation(content.Load<Texture2D>("enemies/ghost/ghost_attack_left"), 9, 0.15f) { IsLooping = false } },
                { "GhostAttackRight", new Animation(content.Load<Texture2D>("enemies/ghost/ghost_attack_right"), 9, 0.15f) { IsLooping = false } },
                { "GhostDeathLeft", new Animation(content.Load<Texture2D>("enemies/ghost/ghost_death_left"), 11, 0.15f) { IsLooping = false } },
                { "GhostDeathRight", new Animation(content.Load<Texture2D>("enemies/ghost/ghost_death_right"), 11, 0.15f) { IsLooping = false } },
                { "GhostLightning", new Animation(content.Load<Texture2D>("enemies/ghost/ghost_lightning"), 10, 0.10f) { IsLooping = false } }
            };

            _enemyPrefab = new Enemy(_ghostAnimations, _ghostShadow)
            {
                Health = 2,
                Speed = 3
            };
        }
        public Enemy CreateEnemy(Vector2 position, Room room)
        {
            Enemy enemy = _enemyPrefab.Clone() as Enemy;

            enemy.Position = position;
            enemy.Parent = room;

            return enemy;
        }
    }
}
