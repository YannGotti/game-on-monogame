using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public interface IGameObjectManager
{
    void AddEntity(GameObject entity);
    void RemoveEntity(GameObject entity);
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, Rectangle dBorder);

    List<GameObject> GetEntities();
}