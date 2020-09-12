using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{   
    bool[] m_isOn = new bool[] { false, false };
    string m_id = "아이디를 입력하세요";
    string m_pass = "비밀번호를 입력하세요";
    bool m_isShowList;
    int m_select;
    string[] m_weaponList = new string[] { "목검", "낡은철검", "철제검", "미스릴검", "화염의마검", "데몬슬레이어" };
    AsyncOperation m_sceneLoadState;
    public void GoNextScene()
    {
        LoadSceneManager.Instance.LoadSceneAsync(LoadSceneManager.eSceneState.Lobby);
    }
    private void OnGUI()
    {
        /*if (GUI.Button(new Rect((Screen.width - 200) / 2, (Screen.height - 60) / 2, 200, 60), "START"))
        {
            SceneManager.LoadScene("Game");
        }*/
        GUILayout.BeginArea(new Rect(10, (Screen.height - 300), 200, 300), GUI.skin.window);
        if (GUILayout.Button("START"))
        {

        }
        m_isOn[0] = GUILayout.Toggle(m_isOn[0], "Option 1");
        if (m_isOn[0])
        {
            GUILayout.TextArea("무적모드 활성화!");
        }
        m_isOn[1] = GUILayout.Toggle(m_isOn[1], "Option 2");
        if (m_isOn[1])
        {
            GUILayout.TextArea("게임속도 증가!");
        }
        m_id = GUILayout.TextField(m_id);
        m_pass = GUILayout.PasswordField(m_pass, '*');
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width - 210, (Screen.height - 300), 200, 300), "장비리스트", GUI.skin.window);
        m_isShowList = GUILayout.Toggle(m_isShowList, "무기 리스트", GUI.skin.button);
        if (m_isShowList)
        {
            m_select = GUILayout.SelectionGrid(m_select, m_weaponList, 1);
        }
        GUILayout.EndArea();
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
