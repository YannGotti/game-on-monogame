using Microsoft.Xna.Framework.Graphics;
using LDtk;
using LDtk.Renderer;
using LDtkTypes;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MyGame;

public class WorldManager : IWorldManager
{
    private LDtkWorld _world;
    private LDtkRenderer _renderer;

    private ITextureManager textureManager;
    private IGameObjectManager gameObjectManager;

    public WorldManager(ITextureManager textureManager, IGameObjectManager gameObjectManager)
    {
        this.textureManager = textureManager;
        this.gameObjectManager = gameObjectManager;
    }


    public void LoadWorld(string ldtkFileName, SpriteBatch spriteBatch)
    {
        _world = textureManager.GetWorldFile(ldtkFileName).LoadWorld(Worlds.World.Iid);
        
        _renderer = new LDtkRenderer(spriteBatch);

        foreach (LDtkLevel level in _world.Levels)
        {
            _renderer.PrerenderLevel(level);
            LayerInstance[] grid =  level.LayerInstances;
            List<TileInstance> tileList = new();

            for (int i = 0; i < grid[0].GridTiles.Length; i++)
            {
                if (tileList.Count == 0)
                {
                    tileList.Add(grid[0].GridTiles[i]);
                    continue;
                }

                if (grid[0].GridTiles[i].Px.Y == grid[0].GridTiles[i - 1].Px.Y)
                {
                    tileList.Add(grid[0].GridTiles[i]);
                }
                else
                {
                    gameObjectManager.AddEntity(
                        new Tile(
                            new
                            (
                                tileList[0].Px.X,
                                tileList[0].Px.Y),
                                (tileList[tileList.Count - 1].Px.X - tileList[0].Px.X) + 16,  16 * 2,
                                gameObjectManager
                            )
                        );
                    tileList = new();
                }
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