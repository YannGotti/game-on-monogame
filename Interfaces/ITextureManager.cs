using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public interface ITextureManager
{
    void LoadTexture(string name, string path);
    Texture2D GetTexture(string name);

    Dictionary<string, Texture2D[]> GetAnimationFrames();

    void LoadFont(string name, string path);
    SpriteFont GetFont(string name);
}