using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyGame;

public class GameObjectManager : IGameObjectManager
{
    private List<GameObject> entities = new List<GameObject>();

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

    public List<GameObject> GetEntities()
    {
        return entities;
    }

    public void AddEntity(GameObject entity) => entities.Add(entity);

    public void RemoveEntity(GameObject entity) => entities.Remove(entity);
}