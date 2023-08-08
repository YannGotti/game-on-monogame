using System.Collections.Generic;
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

    private IEntityManager _entityManager;

    private IWorldManager _worldManager;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.ApplyChanges();

        _textureManager = new TextureManager(Content);
        _entityManager = new EntityManager();
        _worldManager = new WorldManager(_textureManager, _entityManager);

        _camera = new Camera(GraphicsDevice.Viewport);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _worldManager.LoadWorld("World", _spriteBatch);


        _textureManager.LoadTexture("iron", "iron");
        _textureManager.LoadFont("font", "font");


        _player = new(new Vector2(500, 605), _textureManager, _entityManager);
        _entityManager.AddEntity(_player);


    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _entityManager.Update(gameTime);

        _camera.Update(_player.position, GraphicsDevice.Viewport);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(transformMatrix: _camera.Transform);

        Rectangle dBorder = _camera.GetCameraBounds(GraphicsDevice.Viewport);

        _worldManager.Draw(_spriteBatch);

        _entityManager.Draw(_spriteBatch, dBorder);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
}
