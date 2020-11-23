using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    SpriteRenderer m_sprRen;
    [SerializeField]
    float m_speed = 0.2f;
    float m_speedScale = 1f;
    public void SetSpeedScale(float scale)
    {
        m_speedScale = scale;
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        m_sprRen = GetComponent<SpriteRenderer>();
        if(SoundManager.Instance != null)
            SoundManager.Instance.PlayBGM(SoundManager.eBGMClip.BGM01);
    }

    // Update is called once per frame
    void Update()
    {
        var amount = m_speed * m_speedScale * Time.deltaTime;
        if(ScoreManager.Instance != null)
            ScoreManager.Instance.SetFlightScore(amount * 1000);
        m_sprRen.material.mainTextureOffset += Vector2.up * amount;
    }
}
