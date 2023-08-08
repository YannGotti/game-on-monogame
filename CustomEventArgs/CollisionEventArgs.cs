using System;
namespace MyGame;

public class CollisionEventArgs : EventArgs
{
    public Entity Entity { get; }
    public CollisionSide Side { get; }

    public CollisionEventArgs(Entity entity, CollisionSide side)
    {
        Entity = entity;
        Side = side;
    }
}