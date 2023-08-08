using Microsoft.Xna.Framework.Graphics;
using LDtk;
using LDtk.Renderer;
using LDtkTypes;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MyGame;

public class WorldManager : IWorldManager
{
    private LDtkWorld _world;
    private LDtkRenderer _renderer;

    private ITextureManager textureManager;
    private IEntityManager entityManager;

    public WorldManager(ITextureManager textureManager, IEntityManager entityManager)
    {
        this.textureManager = textureManager;
        this.entityManager = entityManager;
    }


    public void LoadWorld(string ldtkFileName, SpriteBatch spriteBatch)
    {
        _world = textureManager.GetWorldFile(ldtkFileName).LoadWorld(Worlds.World.Iid);
        
        _renderer = new LDtkRenderer(spriteBatch);

        foreach (LDtkLevel level in _world.Levels)
        {
            _renderer.PrerenderLevel(level);
            LayerInstance[] grid =  level.LayerInstances;
            
            foreach (var tile  in grid[0].GridTiles)
            {
                entityManager.AddEntity(new Tile(new(tile.Px.X, tile.Px.Y + 34), 32, entityManager));
            }
            
        }
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (LDtkLevel level in _world.Levels)
        {
            _renderer.RenderPrerenderedLevel(level);
        }
    }

    public LDtkLevel GetLevel()
    {
        return _world.LoadLevel("Level_0");
    }
}