using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public enum eGameState
    {
        Normal,
        Invincible,
        Result,
        Max
    }
    eGameState m_state;
    [SerializeField]
    BackgroundController m_bgCtr;
    [SerializeField]
    GameResult m_gameResult;

    public eGameState GetState()
    {
        return m_state;
    }
    public void SetState(eGameState state)
    {
        if (m_state != state)
            m_state = state;

        switch(m_state)
        {
            case eGameState.Normal:
                m_bgCtr.SetSpeedScale(1f);
                MonsterManager.Instance.SetMonsterCreate(1f);
                break;
            case eGameState.Invincible:
                m_bgCtr.SetSpeedScale(7f);
                MonsterManager.Instance.SetMonsterCreate(7f);
                break;
            case eGameState.Result:
                MonsterManager.Instance.CancelMonsterCreate();
                m_gameResult.SetResult();
                ScoreManager.Instance.CloseUI();
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
