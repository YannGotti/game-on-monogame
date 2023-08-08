using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyGame;


public class Tile : Entity
{
    public Tile(Vector2 position, int sizeTile, IEntityManager entityManager)
    {
        this.entityManager = entityManager;

        this.type = EntityType.Tiled;
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
    }
}