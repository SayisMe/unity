using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : DontDestroy<SoundManager>
{
    public enum eAudioType
    {
        BGM,
        SFX,
        MAX
    }
    public enum eBGMClip
    {
        BGM01,
        MAX
    }
    public enum eSFXClip
    {
        Get_Coin,
        Get_Gem,
        Get_Invincible,
        Get_Item,
        Mon_Die,
        Max
    }
    [SerializeField]
    AudioSource[] m_audio;
    [SerializeField]
    AudioClip[] m_bgmClip;
    [SerializeField]
    AudioClip[] m_sfxClip;

    public void SetVolume(float volume)
    {
        if (volume < 0f) volume = 0f;
        if (volume > 1f) volume = 1f;
        for(int i = 0; i<(int)eAudioType.MAX; i++)
        {
            m_audio[i].volume = volume;
        }
    }
    public void SetVolumeBGM(float volume)
    {
        if (volume < 0f) volume = 0f;
        if (volume > 1f) volume = 1f;
        m_audio[(int)eAudioType.BGM].volume = volume;
    }
    public void SetVolumeSFX(float volume)
    {
        if (volume < 0f) volume = 0f;
        if (volume > 1f) volume = 1f;
        m_audio[(int)eAudioType.SFX].volume = volume;
    }
    public void PlayBGM(eBGMClip bgm)
    {
        m_audio[(int)eAudioType.BGM].clip = m_bgmClip[(int)bgm];
        m_audio[(int)eAudioType.BGM].Play();
    }
    public void PlaySFX(eSFXClip sfx)
    {
        //효과음을 각각 개별로 인스턴스를 해서 여러 효과음이 동시에 일어났을 때 먼저 일어난 것을 중단시키지 않음
        m_audio[(int)eAudioType.SFX].PlayOneShot(m_sfxClip[(int)sfx]);      
    }
    // Start is called before the first frame update
    protected override void OnAwake()
    {
        m_audio = new AudioSource[(int)eAudioType.MAX];
        m_audio[(int)eAudioType.BGM] = gameObject.AddComponent<AudioSource>();
        m_audio[(int)eAudioType.BGM].playOnAwake = false;
        m_audio[(int)eAudioType.BGM].loop = true;

        m_audio[(int)eAudioType.SFX] = gameObject.AddComponent<AudioSource>();
        m_audio[(int)eAudioType.SFX].playOnAwake = false;
        m_audio[(int)eAudioType.SFX].loop = false;
        m_bgmClip = Resources.LoadAll<AudioClip>("Sound/BGM");
        m_sfxClip = Resources.LoadAll<AudioClip>("Sound/SFX");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
