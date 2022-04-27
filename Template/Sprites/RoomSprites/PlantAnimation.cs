﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Models;
using System.Collections.Generic;

namespace RogueLike.Sprites.RoomSprites
{
    public class PlantAnimation : Sprite
    {
        public PlantAnimation(Dictionary<string, Animation> animations, Vector2 position) : base(animations, position)
        {
            LayerOrigin = 17;
        }
        public override void Update(GameTime gameTime)
        {
            _animationManager.Update(gameTime, Layer);
        }
    }
}
