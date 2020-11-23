using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToPng : MonoBehaviour
{
    Sprite[] m_sprites;
    // Start is called before the first frame update
    void Start()
    {
        m_sprites = Resources.LoadAll<Sprite>("Font/text_01");
        for(int i = 0; i< m_sprites.Length; i++)
        {
            var spr = m_sprites[i];
            Texture2D tex = new Texture2D((int)spr.rect.width, (int)spr.rect.height, TextureFormat.ARGB32, false);
            tex.SetPixels(0, 0, tex.width, tex.height, spr.texture.GetPixels((int)spr.rect.x, (int)spr.rect.y, (int)spr.rect.width, (int)spr.rect.height));
            var bytes = tex.EncodeToPNG();  //바이트 배열을 반환
            string path = string.Format("{0}/Main/Data/Sprites/ImageFonts/imagefont_{1:00}.png", Application.dataPath, i + 1);
            System.IO.File.WriteAllBytes(path, bytes);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
