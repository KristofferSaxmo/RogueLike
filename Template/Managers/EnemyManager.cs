using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Models;
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
        public EnemyManager(ContentManager content)
        {
            _ghostShadow = content.Load<Texture2D>("enemies/ghost/ghost-shadow");
            _ghostAnimations = new Dictionary<string, Animation>()
            {
                { "GhostLeft", new Animation(content.Load<Texture2D>("enemies/ghost/ghost_left"), 5, 0.2f) },
                { "GhostRight", new Animation(content.Load<Texture2D>("enemies/ghost/ghost_right"), 5, 0.2f) },
                { "GhostAttackLeft", new Animation(content.Load<Texture2D>("enemies/ghost/ghost_attack_left"), 9, 0.2f) { IsLooping = false } },
                { "GhostAttackRight", new Animation(content.Load<Texture2D>("enemies/ghost/ghost_attack_right"), 9, 0.2f) { IsLooping = false } },
                { "GhostDeathLeft", new Animation(content.Load<Texture2D>("enemies/ghost/ghost_death_left"), 11, 0.2f) { IsLooping = false } },
                { "GhostDeathRight", new Animation(content.Load<Texture2D>("enemies/ghost/ghost_death_right"), 11, 0.2f) { IsLooping = false } },
                { "GhostLightning", new Animation(content.Load<Texture2D>("enemies/ghost/ghost_lightning"), 10, 0.2f) { IsLooping = false } }
            };
        }
        public Enemy CreateEnemy(Vector2 position)
        {
            return new Enemy(_ghostAnimations, _ghostShadow)
            {
                Health = 6,
                Position = position,
                Speed = 7
            };
        }
    }
}
