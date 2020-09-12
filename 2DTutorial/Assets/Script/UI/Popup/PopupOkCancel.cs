using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOkCancel : MonoBehaviour
{
    [SerializeField]
    UILabel m_subjectLabel;
    [SerializeField]
    UILabel m_bodyLabel;
    [SerializeField]
    UILabel m_okBtnLabel;
    [SerializeField]
    UILabel m_cancelBtnLabel;

    ButtonDelegate m_okBtnDel;
    ButtonDelegate m_cancelBtnDel;
    public void SetUI(string subject, string body, ButtonDelegate okBtnDel = null, ButtonDelegate cancelBtnDel = null, string okBtnStr = "OK", string cancelBtnStr = "Cancel")
    {
        m_subjectLabel.text = subject;
        m_bodyLabel.text = body;
        m_okBtnLabel.text = okBtnStr;
        m_cancelBtnLabel.text = cancelBtnStr;
        m_okBtnDel = okBtnDel;
        m_cancelBtnDel = cancelBtnDel;
    }
    public void OnPressOK()
    {
        if(m_okBtnDel != null)
        {
            m_okBtnDel();
        }
        else
        {
            PopupManager.Instance.ClosePopup();
        }
    }
    public void OnPressCancel()
    {
        if(m_cancelBtnDel != null)
        {
            m_cancelBtnDel();
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
