using System;
namespace MyGame;

public class CollisionEventArgs : EventArgs
{
    public GameObject Entity { get; }
    public CollisionSide Side { get; }

    public CollisionEventArgs(GameObject entity, CollisionSide side)
    {
        Entity = entity;
        Side = side;
    }
}