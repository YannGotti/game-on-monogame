using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
namespace MyGame;
public class Collider : ICollider
{
    public event EventHandler<Entity> IntersectsEvent;

    private Rectangle boundingBox;
    private Entity owner;

    private List<Entity> entities = new();

    public Collider(Entity owner, int width, int height)
    {
        this.owner = owner;
        boundingBox = new Rectangle((int)owner.position.X, (int)owner.position.Y, width, height);

        entities = owner.entityManager.GetEntities();
    }

    public void Update()
    {
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

            if (!Intersects(entities[i].collider) || entities[i].type == EntityType.Player)
            {
                continue;
            }

            IntersectsEvent?.Invoke(this, entities[i]);
        }
    }

    private bool Intersects(Collider otherCollider)
    {
        return boundingBox.Intersects(otherCollider.boundingBox);
    }

    public override string ToString()
    {
        return $"boundingBox.X={boundingBox.X};boundingBox.Y={boundingBox.Y};";
    }

}
