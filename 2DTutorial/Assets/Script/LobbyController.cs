using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    private void OnGUI()
    {
        if(GUI.Button(new Rect((Screen.width - 200) / 2, (Screen.height - 70) / 2, 200, 70), "START"))
        {
            LoadSceneManager.Instance.LoadSceneAsync(LoadSceneManager.eSceneState.Game);
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
