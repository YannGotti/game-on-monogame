using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyGame;

public enum EntityType
{
    Player,
    Item,
    Block
}

public class Entity
{
    public float speed = 200;
    public Vector2 position;
    public Texture2D texture;

    public EntityType type;
    public Collider collider;

    public ITextureManager textureManager;
    public IEntityManager entityManager;


    public bool isMoving;
    public bool isFight;
    public bool isCrouch;

    public bool isLeft;

    private Vector2 previousPosition = Vector2.Zero;

    public Entity()
    {
    }

    public Entity(Vector2 startPos)
    {
        this.position = startPos;
    }

    public Entity(Vector2 startPos, ITextureManager textureManager, IEntityManager entityManager)
    {
        this.position = startPos;
        this.textureManager = textureManager;
        this.entityManager = entityManager;

    }

    public void IsMoving(){

        if (position.X > previousPosition.X)
        {
            isMoving = true;
            isLeft = false;
        }
        else if (position.X < previousPosition.X)
        {
            isMoving = true;
            isLeft = true;
        }
        else
        {
            isMoving = false;
        }

        previousPosition = position;
    }

    public virtual void Update(GameTime gameTime){
        IsMoving();
    }


    public virtual void Draw(SpriteBatch spriteBatch, Rectangle dBorder){
        
        if(!InBorder(dBorder)){
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

    public virtual void CollisionOccurred(Vector2 direction){

        position += direction;
    }

    public bool InBorder(Rectangle dBorder){

        if(position.X < dBorder.Left){
            return false;
        }

        if(position.X > dBorder.Right){
            return false;
        }

        if(position.Y < dBorder.Top){
            return false;
        }

        if(position.Y > dBorder.Bottom){
            return false;
        }

        return true;
    }

    
}

