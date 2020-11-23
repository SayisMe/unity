using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : SingletonMonoBehaviour<BuffManager>
{
    public enum eBuffType
    {
        Invincible,
        Magnet,
        Max
    }
    public class BuffData
    {
        public float m_duration;
    }
    PlayerController m_player;
    CameraShake m_camShake;
    Dictionary<eBuffType, BuffData> m_dicBuffTable = new Dictionary<eBuffType, BuffData>();
    Dictionary<eBuffType, float> m_dicBuffList = new Dictionary<eBuffType, float>();
    public void SetBuff(eBuffType buff)
    {
        StartCoroutine(Coroutine_BuffProcess(buff));
    }
    IEnumerator Coroutine_BuffProcess(eBuffType buff)
    {
        if (m_dicBuffList.ContainsKey(buff))
        {
            if(buff == eBuffType.Invincible)
                m_camShake.ShakeCamera(m_dicBuffTable[buff].m_duration, 0.2f);
            m_dicBuffList[buff] = 0f;
            yield break;
        }
        else
        {
            m_dicBuffList.Add(buff, 0f);
            switch (buff)
            {
                case eBuffType.Invincible:
                    m_player.SetInvincibleEffect();
                    m_camShake.ShakeCamera(m_dicBuffTable[buff].m_duration, 0.2f);
                    GameManager.Instance.SetState(GameManager.eGameState.Invincible);
                    break;
                case eBuffType.Magnet:
                    m_player.SetMagnetEffect();
                    break;
            }
        }
        var buffdata = m_dicBuffTable[buff];
        while (true)
        {
            m_dicBuffList[buff] += Time.deltaTime;
            yield return null;
            if (m_dicBuffList[buff] >= buffdata.m_duration)
            {
                m_dicBuffList.Remove(buff);
                switch (buff)
                {
                    case eBuffType.Invincible:
                        m_player.ReleaseInvincibleEffect();
                        m_player.SetShockWaveEffect();
                        GameManager.Instance.SetState(GameManager.eGameState.Normal);
                        yield break;
                    case eBuffType.Magnet:
                        m_player.ReleaseMagnetEffect();
                        yield break;
                }
            }
        }
    }
    protected override void OnStart()
    {
        m_camShake = Camera.main.GetComponent<CameraShake>();
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        m_dicBuffTable.Add(eBuffType.Invincible, new BuffData() { m_duration = 3f });
        m_dicBuffTable.Add(eBuffType.Magnet, new BuffData() { m_duration = 6f });
    }
}
