using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyGame;

public enum GameObjectType
{
    Player,
    Item,
    Block,
    Tiled,
    Box
}

public abstract class GameObject
{
    public float speed = 200;
    public Vector2 position;
    public Texture2D texture;

    public GameObjectType type;
    public Collider collider;

    public ITextureManager textureManager;
    public IGameObjectManager gameObjectManager;
    public SpriteBatch spriteBatch;

    public bool isMoving;
    public bool isFight;
    public bool isCrouch;

    public bool isLeft;

    public float massGravity;

    private Vector2 previousPosition = Vector2.Zero;

    public GameObject()
    {
    }

    public GameObject(Vector2 startPos)
    {
        this.position = startPos;
    }

    public GameObject(Vector2 startPos, ITextureManager textureManager, IGameObjectManager entityManager)
    {
        this.position = startPos;
        this.textureManager = textureManager;
        this.gameObjectManager = entityManager;

    }

    public void IsMoving(){

        if (position.X > previousPosition.X)
        {
            isMoving = true;
        }
        else if (position.X < previousPosition.X)
        {
            isMoving = true;
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

    public void DrawData(string data)
    {
        if (spriteBatch == null)
        {
            return;
        }

        if (textureManager == null)
        {
            return;
        }

        SpriteFont font = textureManager.GetFont("font");
        spriteBatch.DrawString(font, data, new Vector2(300, 300), Color.Red);
    }

    public virtual void Draw(SpriteBatch spriteBatch, Rectangle dBorder){
        
        if (spriteBatch == null) 
        {
            this.spriteBatch = spriteBatch;
        }

        if(collider.MathBounding = !InBorder(dBorder))
        {
            return;
        }

        //spriteBatch.Draw
        //(
        //    texture,
        //    position,
        //    null,
        //    Color.White, 0f,
        //    new Vector2(texture.Width / 2, texture.Height / 2),
        //    Vector2.One, SpriteEffects.None, 0f
        //);

        spriteBatch.Draw(texture, position, Color.White);

        collider.Draw(spriteBatch);


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

