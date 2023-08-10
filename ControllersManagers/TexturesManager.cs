using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using LDtk;


namespace MyGame;

public class TextureManager : ITextureManager
{
    private Dictionary<string, Texture2D> textures = new();
    private Dictionary<string, SpriteFont> fonts = new();
    private Dictionary<string, LDtkFile> worldsFiles = new();

    private ContentManager content;

    private Dictionary<string, Texture2D[]> animationFrames;

    public TextureManager(ContentManager content)
    {
        this.content = content;
        InitializeAnimationTexture();
        InitializeWorldTexture();
    }

    private void InitializeWorldTexture(){
        worldsFiles.Add("World", LDtkFile.FromFile("Maps", content));
    }

    private void InitializeAnimationTexture(){
        animationFrames = new();
        Texture2D[] runAnimations = new Texture2D[9];
        Texture2D[] idleAnimations = new Texture2D[9];
        Texture2D[] attack_1Animations = new Texture2D[5];
        Texture2D[] crouchAnimations = new Texture2D[7];
        Texture2D[] pushAnimations = new Texture2D[3];

        for (int i = 0; i < 9; i++)
        {
            runAnimations[i] = content.Load<Texture2D>($"Animations\\Player\\Run\\{i + 1}");
            idleAnimations[i] = content.Load<Texture2D>($"Animations\\Player\\Idle\\{i + 1}");
        }

        for (int i = 0; i < 5; i++)
        {
            attack_1Animations[i] = content.Load<Texture2D>($"Animations\\Player\\Attack_1\\{i + 1}");
        }

        for (int i = 0; i < 7; i++)
        {
            crouchAnimations[i] = content.Load<Texture2D>($"Animations\\Player\\Crouch\\{i + 1}");
        }

        for (int i = 0; i < 3; i++)
        {
            pushAnimations[i] = content.Load<Texture2D>($"Animations\\Player\\Push\\{i + 1}");
        }


        animationFrames.Add("run", runAnimations);
        animationFrames.Add("idle", idleAnimations);
        animationFrames.Add("attack_1", attack_1Animations);
        animationFrames.Add("crouch", crouchAnimations);
    }

    public void LoadTexture(string name, string path)
    {
        if (!textures.ContainsKey(name))
        {
            textures[name] = content.Load<Texture2D>(path);
        }
        else
        {
            // Обработка ситуации, когда текстура уже загружена
        }
    }

    public Texture2D GetTexture(string name)
    {
        if (textures.ContainsKey(name))
        {
            return textures[name];
        }
        return null;
    }

    public Dictionary<string, Texture2D[]> GetAnimationFrames()
    {
        return animationFrames;
    }

    public void LoadFont(string name, string path)
    {
        if (!fonts.ContainsKey(name))
        {
            fonts[name] = content.Load<SpriteFont>(path);
        }
        else
        {
            // Обработка ситуации, когда текстура уже загружена
        }
    }

    public SpriteFont GetFont(string name)
    {
        if (fonts.ContainsKey(name))
        {
            return fonts[name];
        }
        return null;
    }

    public void LoadWorldFile(string name, string path)
    {
        if (!worldsFiles.ContainsKey(name))
        {
            worldsFiles[name] = content.Load<LDtkFile>(path);
        }
        else
        {
            // Обработка ситуации, когда текстура уже загружена
        }
    }

    public LDtkFile GetWorldFile(string name)
    {
        if (worldsFiles.ContainsKey(name))
        {
            return worldsFiles[name];
        }
        return null;
    }
}