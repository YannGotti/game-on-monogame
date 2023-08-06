using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Player : Entity
{
    private Vector2 pushDirection;
    private IAnimationController animationController;

    public Player(Vector2 startPos, ITextureManager textureManager, IEntityManager entityManager)
    {
        this.position = startPos;
        this.speed = 200;
        this.textureManager = textureManager;
        this.entityManager = entityManager;
        this.texture = textureManager.GetAnimationFrames()["run"][0];
        this.pushDirection = Vector2.One;
        this.type = EntityType.Player;
        this.collider = new(this, texture.Width, texture.Height);
        
        animationController = new AnimationController(this);

        collider.IntersectsEvent += IntersectsEvent;
    }

    private float _offsetTime;
    public void Move(GameTime gameTime)
    {
        _offsetTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        var kstate = Keyboard.GetState();

        if(kstate.IsKeyDown(Keys.F))
        {
            if (_offsetTime > 0.1f){
                isFight = true;
            }

            _offsetTime = 0;
        }

        if (_offsetTime > 0.5f) isFight = false;


        if(kstate.IsKeyDown(Keys.LeftControl))
        {
            isCrouch = true;
        }

        if(kstate.IsKeyUp(Keys.LeftControl))
        {
            isCrouch = false;
        }

        if (isCrouch){
            speed = 100;
        }

        if (!isCrouch){
            speed = 200;
        }


        if(isFight) {
            return;
        }

        if (kstate.IsKeyDown(Keys.A))
        {
            position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            pushDirection.X = -5;
        }

        if(kstate.IsKeyDown(Keys.D))
        {
            position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            pushDirection.X = 5;
        }

        if(kstate.IsKeyDown(Keys.E))
        {
            if (_offsetTime > 0.1f) ItemThrown();
            _offsetTime = 0;
        }
        

    }

    public override void Update(GameTime gameTime){
        Move(gameTime);
        animationController.Update(gameTime);
        IsMoving();
    }


    public override void Draw(SpriteBatch spriteBatch, Rectangle dBorder){
        
        if(!InBorder(dBorder)){
            return;
        }
        
        animationController.Draw(spriteBatch, position);
    }

    private void ItemThrown()
    {
        Vector2 new_pos = position + new Vector2(100, 0);
        Item iron = new(textureManager.GetTexture("iron"), entityManager);
        iron.SetPosition(new_pos);
        entityManager.AddEntity(iron);
    }


    private void IntersectsEvent(object sender, Entity e)
    {
        if (e.type == EntityType.Block)
        {
            e.CollisionOccurred(pushDirection);
        }

        if (e.type == EntityType.Item)
        {
            Item item = (Item)e;
            item.CollisionOccurred(position);
            entityManager.RemoveEntity(item);
        }
    }
}