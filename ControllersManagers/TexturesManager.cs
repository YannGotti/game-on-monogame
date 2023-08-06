using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MyGame;

public class TextureManager : ITextureManager
{
    private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
    private Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

    private ContentManager content;

    private Dictionary<string, Texture2D[]> animationFrames;

    public TextureManager(ContentManager content)
    {
        this.content = content;
        InitializeTexture();
    }

    void InitializeTexture(){
        animationFrames = new();
        Texture2D[] runAnimations = new Texture2D[9];
        Texture2D[] idleAnimations = new Texture2D[9];
        Texture2D[] attack_1Animations = new Texture2D[5];
        Texture2D[] crouchAnimations = new Texture2D[7];

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
}