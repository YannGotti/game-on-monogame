using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyGame;

public class EntityManager : IEntityManager
{
    private List<Entity> entities = new List<Entity>();

    public void Update(GameTime gameTime)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            var entity = entities[i];
            entity.Update(gameTime);
            entity.collider.Update();
        }
    }

    public void Draw(SpriteBatch spriteBatch, Rectangle dBorder)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].Draw(spriteBatch, dBorder);
        }
    }

    public List<Entity> GetEntities()
    {
        return entities;
    }

    public void AddEntity(Entity entity) => entities.Add(entity);

    public void RemoveEntity(Entity entity) => entities.Remove(entity);
}