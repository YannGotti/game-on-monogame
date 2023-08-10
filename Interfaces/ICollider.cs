using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyGame;

public interface ICollider
{
    void Update();
    Rectangle GetBoundingBox();

    void UpdateBoundingBox();
}