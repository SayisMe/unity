using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenu_Shop : MonoBehaviour, ILobbyMenu
{
    public void OpenUI()
    {
        gameObject.SetActive(true);
    }
    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
    public GameObject GetGameObject()
    {
        return gameObject;
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
