using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    public enum eLobbyMenu
    {
        Character,
        Inventory,
        Hatchling,
        Shop,
        Clan,
        Max
    }
    [SerializeField]
    GameObject m_bottomMenuObj;
    [SerializeField]
    GameObject m_lobbyMenuObj;
    [SerializeField]
    UI2DSprite m_heroSpr;
    UIButton[] m_menuButtons;
    public System.Func<int> m_callback;

    ILobbyMenu[] m_lobbyMenu;
    public void OnPressMenuButton(UIButton button)
    {
        gameObject.SetActive(false);
        var menuName = button.name.Substring(7);
        for(int i = 0; i< m_lobbyMenu.Length; i++)
        {
            var name = m_lobbyMenu[i].GetGameObject().name;
            if(name.Contains(menuName))
            {
                m_lobbyMenu[i].OpenUI();
            }
            else
            {
                m_lobbyMenu[i].CloseUI();
            }
        }
    }
    private void OnEnable()     //setActive가 false였다가 true
    {
        if(m_callback != null)
        {
            int menuIndex = m_callback();
            if(menuIndex == (int)eLobbyMenu.Character)      //캐릭터 메뉴에서 로비 메뉴로 복귀했을 경우
            {
                var menu = m_lobbyMenu[menuIndex].GetGameObject().GetComponent<LobbyMenu_Character>();
                m_heroSpr.sprite2D = menu.GetHeroSprite(PlayerDataManager.Instance.GetCurHeroIndex());
                m_heroSpr.MakePixelPerfect();
                m_heroSpr.transform.localPosition = menu.GetHeroSpritePos(PlayerDataManager.Instance.GetCurHeroIndex());
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_menuButtons = m_bottomMenuObj.GetComponentsInChildren<UIButton>();
        m_lobbyMenu = m_lobbyMenuObj.GetComponentsInChildren<ILobbyMenu>();

        var menu = m_lobbyMenu[0].GetGameObject().GetComponent<LobbyMenu_Character>();
        m_heroSpr.sprite2D = menu.GetHeroSprite(PlayerDataManager.Instance.GetCurHeroIndex());
        m_heroSpr.MakePixelPerfect();
        m_heroSpr.transform.localPosition = menu.GetHeroSpritePos(PlayerDataManager.Instance.GetCurHeroIndex());

        for (int i = 0; i < m_lobbyMenu.Length; i++)
        {
            m_lobbyMenu[i].CloseUI();
        }
        for (int i = 0; i< m_menuButtons.Length; i++)
        {
            EventDelegate del = new EventDelegate();
            del.target = this;
            del.methodName = "OnPressMenuButton";
            del.parameters[0] = Util.MakeParameter(m_menuButtons[i], typeof(UIButton));
            m_menuButtons[i].onClick.Add(del);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
