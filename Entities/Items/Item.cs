using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;
public class Item : Entity
{
    public Item(Texture2D Texture, IEntityManager entityManager)
    {
        this.entityManager = entityManager;
        this.texture = Texture;
        this.type = EntityType.Item;
        this.collider = new(this, Texture.Width, Texture.Height);
    }

    public void SetPosition(Vector2 Position){
        this.position = Position;
    }

    public override void CollisionOccurred(Vector2 posUser){
        this.position = posUser;
        Debug.WriteLine("Подбор");
    }

}
