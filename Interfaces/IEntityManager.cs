using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public interface IEntityManager
{
    void AddEntity(Entity entity);
    void RemoveEntity(Entity entity);
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, Rectangle dBorder);

    List<Entity> GetEntities();
}