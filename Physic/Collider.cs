using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public enum CollisionSide
{
    None,
    Top,
    Bottom,
    Left,
    Right
}

public class Collider : ICollider
{
    public event EventHandler<CollisionEventArgs> IntersectsEvent;

    private Rectangle boundingBox;
    private GameObject owner;
    private int width;
    private int height;

    private List<GameObject> entities = new();

    public bool MathBounding;
    Texture2D whiteRectangle;


    public bool IsTrigger {get; set;}

    public Collider(GameObject owner, int width, int height)
    {
        this.owner = owner;

        this.width = width;
        this.height = height;

        boundingBox = new Rectangle((int)owner.position.X, (int)owner.position.Y, width, height);

        entities = owner.gameObjectManager.GetEntities();
        IsTrigger = false;

        
    }

    public void Update()
    {
        if (MathBounding)
        {
            return;
        }

        if (owner.type == GameObjectType.Tiled)
        {
            boundingBox.X = (int)owner.position.X;
            boundingBox.Y = (int)owner.position.Y;
        }
        else
        {
            boundingBox.X = (int)owner.position.X;
            boundingBox.Y = (int)owner.position.Y;
        }
        

        Intersects();
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        if (whiteRectangle == null){
            whiteRectangle = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
        }

        spriteBatch.Draw(whiteRectangle, new Rectangle(boundingBox.X, boundingBox.Y, width, 1), Color.Red);
        spriteBatch.Draw(whiteRectangle, new Rectangle(boundingBox.X, boundingBox.Y + height, width, 1), Color.Red);

        spriteBatch.Draw(whiteRectangle, new Rectangle(boundingBox.X, boundingBox.Y, 1, height), Color.Red);
        spriteBatch.Draw(whiteRectangle, new Rectangle(boundingBox.X + width, boundingBox.Y, 1, height), Color.Red);

    }

    private void Intersects(){

        if (entities.Count == 0){
            return;
        }

        for (int i = 0; i < entities.Count; i++)
        {
            if (entities[i] == owner)
            {
                continue;
            }

            if (entities[i].type == GameObjectType.Tiled && owner.type == GameObjectType.Tiled){
                continue;
            }

            if (!Intersects(entities[i].collider))
            {
                continue;
            }

            CollisionSide collisionSide = CollisionSide.None;

            if (boundingBox.Bottom > entities[i].collider.boundingBox.Top && boundingBox.Top < entities[i].collider.boundingBox.Top)
            {
                collisionSide = CollisionSide.Bottom;
            }
            else if (boundingBox.Top < entities[i].collider.boundingBox.Bottom && boundingBox.Bottom > entities[i].collider.boundingBox.Bottom)
            {
                collisionSide = CollisionSide.Top;
            }
            else if (boundingBox.Right > entities[i].collider.boundingBox.Left && boundingBox.Left < entities[i].collider.boundingBox.Left)
            {
                collisionSide = CollisionSide.Right;
            }
            else if (boundingBox.Left < entities[i].collider.boundingBox.Right && boundingBox.Right > entities[i].collider.boundingBox.Right)
            {
                collisionSide = CollisionSide.Left;
            }

            IntersectsEvent?.Invoke(this, new(entities[i], collisionSide));
        }
    }

    private bool Intersects(Collider otherCollider)
    {
        return boundingBox.Intersects(otherCollider.boundingBox);
    }

    public Rectangle GetBoundingBox(){
        return boundingBox;
    }

    public void UpdateBoundingBox()
    {
        boundingBox = new Rectangle((int)owner.position.X, (int)owner.position.Y, owner.texture.Width, owner.texture.Height);
        width = owner.texture.Width;
        height = owner.texture.Height;
    }

    public override string ToString()
    {
        return $"boundingBox.X={boundingBox.X};boundingBox.Y={boundingBox.Y};";
    }

}
