using System.Diagnostics;
using Microsoft.Xna.Framework;
namespace MyGame;
public class Rigidbody
{
    private GameObject owner;
    private Collider collider;
    public bool UseGravity {get; set;}
    
    public Rigidbody(GameObject owner)
    {
        this.owner = owner;
        UseGravity = true;

        if (this.owner.collider != null)
        {
            this.collider = this.owner.collider;
        }
        
        Debug.WriteIf(this.owner.collider == null, "Отсутствует ссылка на коллайдер объекта!");
        collider.IntersectsEvent += CollisionOccurred;
    }

    public void Update(GameTime gameTime)
    {
        if (collider.MathBounding)
        {
            return;
        }

        if (!UseGravity)
        {
            return;
        }

        owner.position.Y += 9.8f * owner.massGravity * (float)gameTime.ElapsedGameTime.TotalSeconds;

    }

    private void CollisionOccurred(object sender, CollisionEventArgs e){

        owner.DrawData(e.Side.ToString());
    
        if (collider.IsTrigger || e.Side == CollisionSide.None)
        {
            return;
        }

        if (e.Side == CollisionSide.Bottom)
        {
            owner.position.Y = e.Entity.position.Y - (owner.type == GameObjectType.Player ? owner.texture.Height / 2 : owner.texture.Height);
        }
        else if (e.Side == CollisionSide.Right)
        {
            e.Entity.position.X = owner.collider.GetBoundingBox().Right;
        }
        else if (e.Side == CollisionSide.Left)
        {
            e.Entity.position.X = owner.collider.GetBoundingBox().Left - e.Entity.collider.GetBoundingBox().Width;
        }

        collider.UpdateBoundingBox();

    }
    
}