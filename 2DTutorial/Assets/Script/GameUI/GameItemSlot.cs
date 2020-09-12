using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItemSlot : MonoBehaviour
{
    [SerializeField]
    UISprite m_selectSpr;
    GameItemInventory m_inven;
    GameItem m_item = null;
    public GameItem Item { get { return m_item; } set { m_item = value; } }

    public void SetSlot(GameItemInventory inventory)
    {
        m_inven = inventory;
        Unselect();
    }
    public void RemoveItem()
    {
        if (m_item == null) return;
        Destroy(m_item.gameObject);
        m_item = null;
        Unselect();
    }
    public bool IsEmpty()
    {
        return Item == null;
    }
    public bool IsSelected()
    {
        return m_selectSpr.enabled == true;
    }
    public void Select()
    {
        m_selectSpr.enabled = true;
    }
    public void Unselect()
    {
        m_selectSpr.enabled = false;
    }
    public void OnSelecte()
    {
        m_inven.OnSelectSlot(this);
    }
    public void SetItem(GameItem item)
    {
        m_item = item;
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
    }
    // Start is called before the first frame update
    void Start()
    {
        Unselect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
