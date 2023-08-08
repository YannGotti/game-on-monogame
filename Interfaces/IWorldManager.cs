using Microsoft.Xna.Framework.Graphics;
using LDtk;

namespace MyGame;

public interface IWorldManager
{
    void LoadWorld(string ldtkFileName, SpriteBatch spriteBatch);

    void Draw(SpriteBatch spriteBatch);
}