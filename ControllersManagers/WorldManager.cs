using Microsoft.Xna.Framework.Graphics;
using LDtk;
using LDtk.Renderer;
using LDtkTypes;

namespace MyGame;

public class WorldManager : IWorldManager
{
    private LDtkWorld _world;
    private LDtkRenderer _renderer;

    private ITextureManager textureManager;

    public WorldManager(ITextureManager textureManager)
    {
        this.textureManager = textureManager;
    }

    public void LoadWorld(string ldtkFileName, SpriteBatch spriteBatch)
    {
        _world = textureManager.GetWorldFile(ldtkFileName).LoadWorld(Worlds.World.Iid);
        
        _renderer = new LDtkRenderer(spriteBatch);

        foreach (LDtkLevel level in _world.Levels)
        {
            _renderer.PrerenderLevel(level);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (LDtkLevel level in _world.Levels)
        {
            _renderer.RenderPrerenderedLevel(level);
        }
    }
}