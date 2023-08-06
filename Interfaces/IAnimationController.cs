using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyGame;

public interface IAnimationController
{
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, Vector2 position);
}