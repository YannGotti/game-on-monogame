using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyGame;


public class Tile : GameObject
{
    public Tile(Vector2 position, int sizeTile, IGameObjectManager gameObjectManager)
    {
        this.gameObjectManager = gameObjectManager;

        this.type = GameObjectType.Tiled;
        this.position = position;
        this.collider = new(this, sizeTile, sizeTile);
    }

    public  override void Update(GameTime gameTime)
    {

    }

    public override void Draw(SpriteBatch spriteBatch, Rectangle dBorder)
    {
        if(collider.MathBounding = !InBorder(dBorder)){
            return;
        }

        collider.Draw(spriteBatch);
    }
}