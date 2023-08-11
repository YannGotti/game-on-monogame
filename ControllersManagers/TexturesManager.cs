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
        animationFrames = new Dictionary<string, Texture2D[]>()
        {
            { "run", LoadAnimationFrames("Run", 9) },
            { "idle", LoadAnimationFrames("Idle", 9) },
            { "attack_1", LoadAnimationFrames("Attack_1", 5) },
            { "crouch", LoadAnimationFrames("Crouch", 7) },
            { "push", LoadAnimationFrames("Push", 3) }
        };
    }

    private Texture2D[] LoadAnimationFrames(string animationName, int frameCount)
    {
        Texture2D[] animationFrames = new Texture2D[frameCount];
        for (int i = 0; i < frameCount; i++)
        {
            animationFrames[i] = content.Load<Texture2D>($"Animations\\Player\\{animationName}\\{i + 1}");
        }
        return animationFrames;
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