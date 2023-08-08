using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
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
    private Entity owner;

    private List<Entity> entities = new();

    public bool MathBounding;

    public bool IsTrigger {get; set;}

    public Collider(Entity owner, int width, int height)
    {
        this.owner = owner;
        boundingBox = new Rectangle((int)owner.position.X, (int)owner.position.Y, width, height);

        entities = owner.entityManager.GetEntities();
        IsTrigger = false;
    }

    public void Update()
    {
        if (MathBounding)
        {
            return;
        }

        boundingBox.X = (int)owner.position.X;
        boundingBox.Y = (int)owner.position.Y;

        Intersects();
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

            if (entities[i].type == EntityType.Tiled && owner.type == EntityType.Tiled){
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

    public override string ToString()
    {
        return $"boundingBox.X={boundingBox.X};boundingBox.Y={boundingBox.Y};";
    }

}
