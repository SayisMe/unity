using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    [SerializeField]
    UISprite m_fadeSprite;
    bool m_isStart;
    float m_interval = 3f;
    float m_curTime = 0f;
    int m_hp = 100;
    int m_min;
    int m_sec;
   
    IEnumerator CoroutineTimer(float time)
    {        
        while (true)
        {
            yield return new WaitForSeconds(time);
            m_sec++;
            if (m_sec >= 60)
            {
                m_sec = 0;
                m_min++;
            }
            if (m_min >= 1)
                break;
            Debug.Log(string.Format("{0:00} min {1:00} sec", m_min, m_sec));
        }
    }
    IEnumerator CoroutineFadeIn(float duration)
    {
        m_fadeSprite.color = new Color(0f, 0f, 0f, 0f);
        float time = 0f;
        while (true)
        {
            time += Time.deltaTime;
            m_fadeSprite.color = new Color(0f, 0f, 0f, time / duration);
            if(time >= duration)
            {
                m_fadeSprite.color = Color.black;
                break;
            }
            /*if (m_fadeSprite.color.a < 1f)
            {
                m_fadeSprite.color = new Color(0f, 0f, 0f, m_fadeSprite.color.a + 0.01f);
            }
            else
            {
                m_fadeSprite.color = Color.black;
                break;
            }*/
            yield return null;
        }
       
    }
    // Start is called before the first frame update
    void Start()
    {
        DebugString.Log("start");
        //StartCoroutine("PrintMessge");
        DebugString.Log("End");
        //StartCoroutine(CoroutineTimer(1f));
    }

    // Update is called once per frame
    void Update()
    {      
        if(Input.GetKeyDown(KeyCode.A))
        {
            StopAllCoroutines();
            StartCoroutine(CoroutineFadeIn(0.5f));            
        }
        /*if(m_isStart)
        {
            if(m_fadeSprite.color.a < 1f)
            {
                m_fadeSprite.color = new Color(0f, 0f, 0f, m_fadeSprite.color.a + 0.01f);
            }
            else
            {
                m_fadeSprite.color = Color.black;
                m_isStart = false;
            }
        }*/
     
    }
}
