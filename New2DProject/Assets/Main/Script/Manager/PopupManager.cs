using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ButtonDelegate();

public class PopupManager : DontDestroy<PopupManager>
{
    [SerializeField]
    GameObject m_popupOkCancelPrefab;
    [SerializeField]
    GameObject m_popupOkPrefab;
    int m_popupDepth = 1000;
    int m_popupGap = 10;
    List<GameObject> m_popupList = new List<GameObject>();
    public void OpenPopupOkCancel(string subject, string body, ButtonDelegate okBtnDel = null, ButtonDelegate cancelBtnDel = null, string okBtnStr = "Ok", string cancelBtnStr = "Cancel")
    {
        var obj = Instantiate(m_popupOkCancelPrefab) as GameObject;
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        var panels = obj.GetComponentsInChildren<UIPanel>();

        for(int i = 0; i < panels.Length; i++)
        {
            panels[i].depth = m_popupDepth + m_popupList.Count * m_popupGap + i;
        }
        var popup = obj.GetComponent<PopupOkCancel>();
        popup.SetUI(subject, body, okBtnDel, cancelBtnDel, okBtnStr, cancelBtnStr);

        m_popupList.Add(obj);
    }
    public void OpenPopupOk(string subject, string body, ButtonDelegate okBtnDel = null, string okBtnStr = "Ok")
    {
        var obj = Instantiate(m_popupOkPrefab) as GameObject;
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        var panels = obj.GetComponentsInChildren<UIPanel>();

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].depth = m_popupDepth + m_popupList.Count * m_popupGap + i;
        }
        var popup = obj.GetComponent<PopupOk>();
        popup.SetUI(subject, body, okBtnDel, okBtnStr);

        m_popupList.Add(obj);
    }
    public bool IsOpenPopup()
    {
        return m_popupList.Count == 0 ? false : true;
    }
    public void ClosePopup()
    {
        if(m_popupList.Count > 0)
        {
            Destroy(m_popupList[m_popupList.Count - 1]);
            m_popupList.Remove(m_popupList[m_popupList.Count - 1]);
        }
    }
    public void ClearPopup()
    {
        for(int i = 0; i < m_popupList.Count; i++)
        {
            Destroy(m_popupList[i]);
        }
        m_popupList.Clear();
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_popupOkCancelPrefab = Resources.Load("Prefab/UI/Popup/PopupOkCancel") as GameObject;
        m_popupOkPrefab = Resources.Load("Prefab/UI/Popup/PopupOk") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Random.Range(1, 101) % 2 == 0)
                OpenPopupOkCancel("Notice", "안녕하세요. SBS 게임 아카데미입니다.\r\n팝업 테스트 중 입니다.");
            else
                OpenPopupOk("Error", "코로나 여파로 인하여 게임 서비스를 당분간 종료합니다.", null, "확인");
        }
    }
}
