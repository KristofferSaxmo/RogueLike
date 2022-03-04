using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RogueLike
{
    public abstract class Component
    {
        public bool IsRemoved { get; protected set; }
        public List<Component> Children { get; set; }
        public Component Parent;
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
