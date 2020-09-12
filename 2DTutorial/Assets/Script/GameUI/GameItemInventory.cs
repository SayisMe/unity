using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameItemInventory : MonoBehaviour
{
    public enum eItemType
    {
        Ball,
        Bomb,
        Bowling,
        Coin,
        Hat,
        Magnet,
        Spring,
        Max
    }
    int m_maxSlotCount = 24;
    [SerializeField]
    GameObject m_slotPrefab;
    [SerializeField]
    GameObject m_itemPrefab;
    [SerializeField]
    UIGrid m_grid;
    [SerializeField]
    UILabel m_invenInfo;
    [SerializeField]
    Sprite[] m_iconSprites;
    UITweener m_posTween;
    List<GameItemSlot> m_slotList = new List<GameItemSlot>();
    int m_fulledSlotCount;
    
    void InitInventory()
    {        
        for (int i = 0; i < m_maxSlotCount; i++)
        {
            var obj = Instantiate(m_slotPrefab) as GameObject;
            obj.transform.SetParent(m_grid.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            var slot = obj.GetComponent<GameItemSlot>();
            slot.SetSlot(this);
            m_slotList.Add(slot);
        }
        m_invenInfo.text = string.Format("{0}/{1}", m_fulledSlotCount, m_slotList.Count);
    }
    public void ExtendMaxSlot()
    {
        for (int i = 0; i < 6; i++)
        {
            var obj = Instantiate(m_slotPrefab) as GameObject;
            obj.transform.SetParent(m_grid.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            var slot = obj.GetComponent<GameItemSlot>();
            slot.SetSlot(this);
            m_slotList.Add(slot);
        }
        m_grid.Reposition();
        m_invenInfo.text = string.Format("{0}/{1}", m_fulledSlotCount, m_slotList.Count);
    }
    public void OnSelectSlot(GameItemSlot slot)
    {
        for (int i = 0; i < m_slotList.Count; i++)
        {
            if (m_slotList[i].IsSelected())
            {
                m_slotList[i].Unselect();
                break;
            }
        }
        slot.Select();
    }
    public void CrateItem()
    {    
        for (int i = 0; i < m_slotList.Count; i++)
        {
            if (m_slotList[i].IsEmpty())
            {
                var type = (eItemType)Random.Range((int)eItemType.Ball, (int)eItemType.Max);
                var obj = Instantiate(m_itemPrefab) as GameObject;
                var item = obj.GetComponent<GameItem>();
                item.SetItem(this, type, Random.Range(1, 100));
                m_slotList[i].SetItem(item);
                m_fulledSlotCount++;
                m_invenInfo.text = string.Format("{0}/{1}", m_fulledSlotCount, m_slotList.Count);
                break;
            }
        }
    }
    public void DeleteItem()
    {
        for (int i = 0; i < m_slotList.Count; i++)
        {
            if (m_slotList[i].IsSelected())
            {
                if (m_slotList[i].IsEmpty())
                {
                    return;
                }
                m_slotList[i].RemoveItem();
                m_fulledSlotCount--;
                m_invenInfo.text = string.Format("{0}/{1}", m_fulledSlotCount, m_slotList.Count);
                break;
            }
        }
    }
    public Sprite GetItemIconSprite(eItemType type)
    {
        return m_iconSprites[(int)type];
    }
    // Start is called before the first frame update
    void Start()
    {
        m_posTween = GetComponent<UITweener>();
        InitInventory();
        gameObject.SetActive(false);
    }

    public void HandleInventory()
    {
        if (!gameObject.activeSelf)
        {
            m_posTween.ResetToBeginning();
            m_posTween.PlayForward();
            gameObject.SetActive(true);
        }
        else
            gameObject.SetActive(false);        
    }
   
}
