using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : DontDestroy<LoadSceneManager>
{
    public enum eSceneState
    {
        None = -1,
        CI,
        Title,
        Lobby,
        Game
    }
    [SerializeField]
    GameObject m_loadingObj;

    UIProgressBar m_loadingBar;
    AsyncOperation m_loadSceneState;
    eSceneState m_state = eSceneState.CI;
    eSceneState m_loadState = eSceneState.None;
    public void SetState(eSceneState state)
    {
        m_state = state;
    }
    public eSceneState GetState()
    {
        return m_state;
    }
    public void LoadSceneAsync(eSceneState sceneState)
    {
        if (m_loadState != eSceneState.None) return;

        m_loadState = sceneState;
        m_loadSceneState = SceneManager.LoadSceneAsync(sceneState.ToString());
        if (m_loadingObj != null)
        {
            m_loadingObj.SetActive(true);
            m_loadingBar.value = 0f;
        }
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {        
        m_loadingBar = GetComponentInChildren<UIProgressBar>();
        m_loadingObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {        
        if (m_loadSceneState != null)
        {
            if (m_loadSceneState.isDone)
            {
                m_state = m_loadState;
                m_loadState = eSceneState.None;
                m_loadSceneState = null;
                m_loadingBar.value = 1f;
                m_loadingObj.SetActive(false);
                PopupManager.Instance.ClearPopup();
            }
            else
            {
                // Debug.Log((int)(m_sceneLoadState.progress * 100));
                DebugString.Log((int)(m_loadSceneState.progress * 100));
                m_loadingBar.value = m_loadSceneState.progress;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (PopupManager.Instance.IsOpenPopup())
            {
                PopupManager.Instance.ClosePopup();
            }
            else
            {
                switch (m_state)
                {
                    case eSceneState.Title:
                        PopupManager.Instance.OpenPopupOkCancel("[000000]Notice[-]", "[000000]게임을 종료하시겠습니까?[-]", ()=> 
                        {

#if UNITY_EDITOR
                            UnityEditor.EditorApplication.isPlaying = false;
#else
                            Application.Quit();
#endif
                        }, null, "예", "아니오");
                        break;
                    case eSceneState.Lobby:
                        PopupManager.Instance.OpenPopupOkCancel("[000000]Notice[-]", "[000000]타이틀 화면으로 돌아가시겠습니까?[-]", ()=> 
                        {
                            LoadSceneAsync(eSceneState.Title);
                        }, null, "예", "아니오");
                        break;
                    case eSceneState.Game:
                        PopupManager.Instance.OpenPopupOkCancel("[000000]Notice[-]", "[000000]로비 화면으로 돌아가시겠습니까?[-]", ()=> 
                        {
                            LoadSceneAsync(eSceneState.Lobby);
                        }, null, "예", "아니오");
                        break;
                }
            }
        }
    }
}
