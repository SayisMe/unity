using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILobbyMenu
{
    void OpenUI();
    void CloseUI();
    GameObject GetGameObject();
}
