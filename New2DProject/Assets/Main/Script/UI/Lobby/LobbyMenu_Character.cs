using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenu_Character : MonoBehaviour, ILobbyMenu
{
    [SerializeField]
    LobbyController m_lobby;
    [SerializeField]
    Vector2[] m_heroTexturePosTable;
    [SerializeField]
    UI2DSprite m_heroSpr;
    int m_select;
    TweenPosition m_heroSpriteTween;
    [SerializeField]
    UIButton[] m_buttons;
    [SerializeField]
    UISprite m_alphaSprite;
    
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
    public Vector2 GetHeroSpritePos(int index)
    {
        return m_heroTexturePosTable[index];
    }
    public Sprite GetHeroSprite(int index)
    {
        return m_heroSpr.sprite2D;
    }
    public void OnPressLeft()
    {
        m_select--;
        if(m_select < 0)
        {
            m_select = m_heroTexturePosTable.Length - 1;
        }
        LoadHeroSprite();
        ResetInfo();
    }
    public void OnPressRight()
    {
        m_select++;
        if(m_select > m_heroTexturePosTable.Length - 1)
        {
            m_select = 0;
        }
        LoadHeroSprite();
        ResetInfo();
    }
    public void OnPressSelect()
    {
        PlayerDataManager.Instance.SetCurHeroIndex(m_select);
        m_lobby.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    void ResetInfo()
    {
        for (int i = 0; i < m_buttons.Length; i++)
            m_buttons[i].gameObject.SetActive(false);
        if(PlayerDataManager.Instance.IsBuyHero(m_select))
        {
            m_buttons[0].gameObject.SetActive(true);
            m_alphaSprite.depth = -1;
        }
        else
        {
            m_buttons[1].gameObject.SetActive(true);
            m_alphaSprite.depth = m_heroSpr.depth + 1;
        }
    }
    void LoadHeroSprite()
    {
        m_heroSpr.sprite2D = Resources.Load<Sprite>(string.Format("Character/character_{0:00}", m_select + 1));
        m_heroSpr.MakePixelPerfect();
        m_heroSpr.transform.localPosition = m_heroTexturePosTable[m_select];
        m_heroSpriteTween.from = m_heroTexturePosTable[m_select];
        m_heroSpriteTween.to = m_heroSpriteTween.from + Vector3.down * 20;
    }
    int MenuIndex()
    {
        return 0;
    }
    private void OnEnable()
    {
        m_select = PlayerDataManager.Instance.GetCurHeroIndex();
        LoadHeroSprite();
        ResetInfo();
        m_lobby.m_callback += MenuIndex;
    }
    private void OnDisable()
    {
        m_lobby.m_callback -= MenuIndex;
    }
    // Start is called before the first frame update
    void Awake()
    {
        m_heroSpriteTween = GetComponentInChildren<TweenPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
