using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    [SerializeField]
    UI2DSprite m_icon;
    [SerializeField]
    UILabel m_countLabel;
    GameItemInventory.eItemType m_type;
    GameItemInventory m_inven;
    int m_count;
    public void SetItem(GameItemInventory inventory, GameItemInventory.eItemType type, int count)
    {
        m_inven = inventory;
        m_type = type;
        m_count = count;
        m_countLabel.text = string.Format("{0:00}", m_count);
        m_icon.sprite2D = m_inven.GetItemIconSprite(m_type);
        //m_icon.width = (int)m_icon.sprite2D.rect.width;
        //m_icon.height = (int)m_icon.sprite2D.rect.height;
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
