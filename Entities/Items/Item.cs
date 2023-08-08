using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;
public class Item : Entity
{
    private Rigidbody rigidbody;
    public Item(Texture2D Texture, IEntityManager entityManager)
    {
        this.entityManager = entityManager;
        this.texture = Texture;
        this.type = EntityType.Item;
        this.collider = new(this, Texture.Width, Texture.Height);
        this.massGravity = 20;
        rigidbody = new(this);
    }

    public void SetPosition(Vector2 Position, bool isLeft){
        this.position = Position;
    }

    public override void CollisionOccurred(Vector2 posUser){
        this.position = posUser;
    }

    public override void Update(GameTime gameTime){
        IsMoving();

        rigidbody.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch, Rectangle dBorder){



        if(collider.MathBounding = !InBorder(dBorder))
        {
            return;
        }

        spriteBatch.Draw
        (
            texture,
            position,
            null,
            Color.White, 0f,
            new Vector2(texture.Width / 2, texture.Height / 2),
            Vector2.One, SpriteEffects.None, 0f
        );

    }

}
