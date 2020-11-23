using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOk : MonoBehaviour
{
    [SerializeField]
    UILabel m_subjectLabel;
    [SerializeField]
    UILabel m_bodyLabel;
    [SerializeField]
    UILabel m_okBtnLabel;
  
    ButtonDelegate m_okBtnDel;
    public void SetUI(string subject, string body, ButtonDelegate okBtnDel = null, string okBtnStr = "OK")
    {
        m_subjectLabel.text = subject;
        m_bodyLabel.text = body;
        m_okBtnLabel.text = okBtnStr;
        m_okBtnDel = okBtnDel;
    }
    public void OnPressOK()
    {
        if (m_okBtnDel != null)
        {
            m_okBtnDel();
        }
        else
        {
            PopupManager.Instance.ClosePopup();
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
