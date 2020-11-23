using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    GameObject m_bgObject;
    [SerializeField]
    GameObject m_titleObject;
    public void SetTitle()
    {
        LoadSceneManager.Instance.SetState(LoadSceneManager.eSceneState.Title);
        m_bgObject.SetActive(true);
        m_titleObject.SetActive(true);
    }
    public void GoNextScene()
    {
        LoadSceneManager.Instance.LoadSceneAsync(LoadSceneManager.eSceneState.Lobby);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_bgObject.SetActive(false);
        m_titleObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && Input.GetKeyDown(KeyCode.Escape) == false)
        {
            GoNextScene();
        }
    }
}
