using System.Diagnostics;
using Microsoft.Xna.Framework;
namespace MyGame;
public class Rigidbody
{
    private Entity owner;
    private Collider collider;
    public bool UseGravity {get; set;}


    public Rigidbody(Entity owner)
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

        if(collider.IsTrigger){
            return;
        }

        if(e.Side == CollisionSide.Top)
        {
            //Debug.WriteLine("Top");
        }

        if(e.Side == CollisionSide.Bottom)
        {
            owner.position.Y = e.Entity.collider.GetBoundingBox().Top - owner.collider.GetBoundingBox().Height;
        }

        if(e.Side == CollisionSide.Left)
        {
            //owner.isLeft = true;
            owner.position.X = e.Entity.collider.GetBoundingBox().Left + owner.collider.GetBoundingBox().Width;
            //e.Entity.position.X = owner.collider.GetBoundingBox().Left - e.Entity.collider.GetBoundingBox().Width;
        }

        if(e.Side == CollisionSide.Right)
        {
            //owner.isLeft = false;
            owner.position.X = e.Entity.collider.GetBoundingBox().Left - owner.collider.GetBoundingBox().Width;
            //e.Entity.position.X = owner.collider.GetBoundingBox().Right + 1;
        }


    }
    
}