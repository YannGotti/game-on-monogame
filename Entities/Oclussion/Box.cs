using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;
public class Box : GameObject
{
    private Rigidbody rigidbody;

    public Box(Vector2 startPos, Texture2D Texture, IGameObjectManager gameObjectManager)
    {
        this.position = startPos;
        this.gameObjectManager = gameObjectManager;
        this.texture = Texture;
        this.type = GameObjectType.Box;
        this.collider = new(this, texture.Width, texture.Height);
        this.massGravity = 50;
        rigidbody = new(this);
    }


    public override void Update(GameTime gameTime){
        IsMoving();
        
        rigidbody.Update(gameTime);
    }
    
}