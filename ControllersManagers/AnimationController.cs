using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyGame;
public class AnimationController : IAnimationController
{
    public Texture2D[] frames;
    private int currentFrame;
    private float frameTime;
    private float timeElapsed;
    private GameObject owner;
    private Dictionary<string, Texture2D[]> animationFrames;

    public AnimationController(GameObject owner)
    {
        this.frameTime = 0.1f;
        currentFrame = 0;
        timeElapsed = 0;

        this.owner = owner;
        this.animationFrames = owner.textureManager.GetAnimationFrames();
    }


    public void Update(GameTime gameTime)
    {
        Controller();

        timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (timeElapsed > frameTime)
        {
            currentFrame = (currentFrame + 1) % frames.Length;
            timeElapsed -= frameTime;
        }
    }

    private void Controller(){
        if (animationFrames == null){
            return;
        }


        if (owner.isMoving && animationFrames["run"] != null) {
            frames = animationFrames["run"];
        }

        if (!owner.isMoving && animationFrames["idle"] != null) {
            frames = animationFrames["idle"];
        }

        if (owner.isFight && animationFrames["attack_1"] != null) {
            frames = animationFrames["attack_1"];
        }

        if (owner.isCrouch && owner.isMoving && animationFrames["crouch"] != null){
            frames = animationFrames["crouch"];
        }

        if (owner.isCrouch && !owner.isMoving && animationFrames["crouch"] != null){
            frames = new Texture2D[1]{ animationFrames["crouch"][0] };
        }


        if (!owner.isCrouch && owner.type == GameObjectType.Player && animationFrames["push"] != null)
        {
            Player player = (Player)owner;

            if (player.isPush)
            {
                frames = animationFrames["push"];
                return;
            }

        }

        owner.texture = frames[0];     
        owner.collider.UpdateBoundingBox();   
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {

        try
        {
            if (owner.isLeft){
                spriteBatch.Draw
                (
                    frames[currentFrame],
                    position,
                    null,
                    Color.White, 0f,
                    new Vector2(frames[currentFrame].Width / 2, frames[currentFrame].Height / 2),
                    Vector2.One, SpriteEffects.FlipHorizontally, 0f
                );
            }

            if (!owner.isLeft){

                spriteBatch.Draw
                (
                    frames[currentFrame],
                    position,
                    null,
                    Color.White, 0f,
                    new Vector2(frames[currentFrame].Width / 2, frames[currentFrame].Height / 2),
                    Vector2.One, SpriteEffects.None, 0f
                );
            }
        }
        catch (System.Exception)
        {
            currentFrame = 0;
        }
        
    }
}
