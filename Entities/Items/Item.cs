using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;
public class Item : GameObject
{
    private Rigidbody rigidbody;

    public Item(Texture2D Texture, IGameObjectManager gameObjectManager)
    {
        this.gameObjectManager = gameObjectManager;
        this.texture = Texture;
        this.type = GameObjectType.Item;
        this.collider = new(this, texture.Width, texture.Height);
        this.massGravity = 20;
        rigidbody = new(this);

        this.collider.IsTrigger = true;
        rigidbody.UseGravity = false;
    }

    public void SetPosition(Vector2 pos, bool isLeft)
    {
        position.Y = pos.Y - 10;

        if(isLeft)
        {
            position.X = pos.X - 60;
        }

        if(!isLeft)
        {
            position.X = pos.X + 60;
        }
    }

    public override void CollisionOccurred(Vector2 posUser){
        this.position = posUser;
    }

    public override void Update(GameTime gameTime){
        IsMoving();

        //rigidbody.Update(gameTime);
    }

}
