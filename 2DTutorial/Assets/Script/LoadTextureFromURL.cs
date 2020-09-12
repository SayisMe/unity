using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadTextureFromURL : MonoBehaviour
{
    [SerializeField]
    UITexture m_texture;
    Dictionary<string, Texture2D> m_dicCacheTexture = new Dictionary<string, Texture2D>();
    public IEnumerator CoroutineLoadTexture(string url)
    {
        var www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if(www.error != null)
        {
            Debug.Log(www.error);
        }
        if(www.isDone)
        {
            Debug.Log("Texture load success");
            var texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            m_texture.mainTexture = texture;
            m_texture.MakePixelPerfect();
            m_dicCacheTexture.Add(url, texture);
        }

    }
    public void LoadTexture(string url)
    {
        if(m_dicCacheTexture.ContainsKey(url))
        {
            m_texture.mainTexture = m_dicCacheTexture[url];
            m_texture.MakePixelPerfect();
        }
        else
        {
            StartCoroutine(CoroutineLoadTexture(url));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadTexture("http://pds27.egloos.com/pds/201407/23/84/c0080484_53cf13187368d.jpg");
        LoadTexture("https://steemitimages.com/DQmS1gGYmG3vL6PKh46A2r6MHxieVETW7kQ9QLo7tdV5FV2/IMG_1426.JPG");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
