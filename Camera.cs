using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;
public class Camera
{
    public Matrix Transform { get; private set; }
    public Vector2 Position { get; set; }
    public float Scale { get; set; }

    public Camera(Viewport viewport)
    {
        Scale = 1.0f;
    }

    public void Update(Vector2 playerPosition, Viewport viewport)
    {
        Position = new Vector2(playerPosition.X - viewport.Width / 2, playerPosition.Y - viewport.Height / 2);


        Transform = Matrix.CreateScale(Scale) * Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0));
    }

    public Rectangle GetCameraBounds(Viewport viewport)
    {
        Vector2 screenTopLeft = new(Position.X, Position.Y);

        Vector2 screenBottomRight = new(Position.X + viewport.Width, Position.Y + viewport.Height);

        Rectangle cameraBounds = new((int)screenTopLeft.X, (int)screenTopLeft.Y, (int)(screenBottomRight.X - screenTopLeft.X), (int)(screenBottomRight.Y - screenTopLeft.Y));

        return cameraBounds;
    }
}
