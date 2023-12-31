﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

using LDtk;
using LDtk.Renderer;
using LDtkTypes;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Camera _camera;

    private Player _player;

    private ITextureManager _textureManager;

    private IGameObjectManager _gameObjectManager;

    private IWorldManager _worldManager;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.ApplyChanges();

        _textureManager = new TextureManager(Content);
        _gameObjectManager = new GameObjectManager();
        _worldManager = new WorldManager(_textureManager, _gameObjectManager);

        _camera = new Camera(GraphicsDevice.Viewport);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _worldManager.LoadWorld("World", _spriteBatch);


        _textureManager.LoadTexture("iron", "iron");
        _textureManager.LoadTexture("box", "Objects\\box");
        _textureManager.LoadFont("font", "font");


        _player = new(new Vector2(500, 100), _textureManager, _gameObjectManager);
        _gameObjectManager.AddEntity(_player);
        
        _gameObjectManager.AddEntity
        (
            new Box
            (
                new Vector2(300, 0), _textureManager.GetTexture("box"), _gameObjectManager
            )
        );

        _gameObjectManager.AddEntity(new Box(new Vector2(300, 50), _textureManager.GetTexture("box"), _gameObjectManager));

        _gameObjectManager.AddEntity
        (
            new Box
            (
                new Vector2(600, 0), _textureManager.GetTexture("box"), _gameObjectManager
            )
        );
        
        
        _gameObjectManager.AddEntity(_player);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _gameObjectManager.Update(gameTime);

        _camera.Update(_player.position, GraphicsDevice.Viewport);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(transformMatrix: _camera.Transform);

        Rectangle dBorder = _camera.GetCameraBounds(GraphicsDevice.Viewport);

        _worldManager.Draw(_spriteBatch);

        _gameObjectManager.Draw(_spriteBatch, dBorder);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
}
