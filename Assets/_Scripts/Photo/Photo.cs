using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Photo {
    private Texture2D texture;

    public Photo(Texture2D tex)
    {
        this.texture = tex;
    }
    public Sprite GetAsSprite()
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
    }
    public Texture2D GetAsTexture2D()
    {
        return texture;
    }
    public void SetTexture(Texture2D tex)
    {
        this.texture = tex;
    }
}

