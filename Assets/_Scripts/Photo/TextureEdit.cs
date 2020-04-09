using UnityEngine;
using System.Threading.Tasks;

public class TextureEdit : MonoBehaviour {
    public Texture2D image;

    public static Texture2D Transpose(Texture2D tex)
    {
        Texture2D newTex = new Texture2D(tex.height, tex.width);
        Color32[] c = tex.GetPixels32();
        Color32[] c2 = new Color32[c.Length];
        int h = tex.height;
        int w = tex.width;
        Parallel.For(0, w,
        x => {
            Parallel.For(0, h,
            y => {
                c2[x*h + y] = c[y*w + x];
            });
        });
        newTex.SetPixels32(c2);
        newTex.Apply();
        return newTex;
    }
    public static Texture2D InvertVertically(Texture2D tex)
    {
        Texture2D newTex = new Texture2D(tex.width, tex.height);
        Color32[] c = tex.GetPixels32();
        Color32[] c2 = new Color32[c.Length];
        int h = tex.height;
        int w = tex.width;
        Parallel.For(0, w,
        x => {
            Parallel.For(0, h,
            y => {
                c2[x*w + (h - y)] = c[y*w + x];
            });
        });
        newTex.SetPixels32(c2);
        newTex.Apply();
        return newTex;
    }
    public static Texture2D InvertHorizontally(Texture2D tex)
    {
        Texture2D newTex = new Texture2D(tex.width, tex.height);
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                newTex.SetPixel(x, y, tex.GetPixel(tex.width - x, y));
            }
        }
        newTex.Apply();
        return newTex;
    }
    public static Texture2D RotateCounterClockwise(Texture2D tex, int times = 1)
    {
        for ( ; times > 0; times--)
        {
            tex = InvertHorizontally(Transpose(tex));
        }
        tex.Apply();
        return tex;
    }
    public static Texture2D RotateClockwise(Texture2D tex, int times = 1)
    {
        int h = tex.height;
        int w = tex.width;
        Color32[] pixels = tex.GetPixels32();
        Color32[] temp = new Color32[pixels.Length];
        Parallel.For(0, w,
        x => {
            Parallel.For(0, h,
            y => {
                int x2 = (h-y)-1;
                int y2 = x;
                temp[(y2)*h + x2] = pixels[(h-y-1)*w + w-x-1];
            });
        });
        var t = new Texture2D(h, w);
        t.SetPixels32(temp);
        t.Apply();
        return t;
    }
    public static Color32[] RotateMatrix(Color32[] matrix, int height, int width) {
        Color32[] ret = new Color32[height * width];
        Parallel.For(0, width,
        x => {
            Parallel.For(0, height,
            y => {
                int x2 = (height-y)-1;
                int y2 = x;
                ret[y2*height + x2] = matrix[y*width + x];
            });
        });
        return ret;
    }    
}
