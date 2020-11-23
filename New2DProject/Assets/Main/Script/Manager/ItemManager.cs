using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    public enum eItemType
    {
        Coin,
        Gem_Red,
        Gem_Green,
        Gem_Blue,
        Invincible,
        Magnet,
        MAX
    }
    [SerializeField]
    Sprite[] m_iconSprites;
    [SerializeField]
    GameObject m_gameItemPrefab;
    GameObjectPool<GameItem> m_gameItemPool;
    public PlayerController m_player { get; private set; }
    int[] m_itemPriorityTable = new int[] { 80, 5, 3, 2, 8, 2 };
    public void CreateItem(Vector3 pos)
    {
        var item = m_gameItemPool.Get();
        var type = (eItemType)Util.GetPriority(m_itemPriorityTable);
            //(eItemType)Random.Range((int)eItemType.Coin, (int)eItemType.MAX);
        item.gameObject.SetActive(true);
        item.SetItem(pos, type);
    }
    public void RemoveItem(GameItem item)
    {
        item.gameObject.SetActive(false);
        m_gameItemPool.Set(item);
    }
    public Sprite GetItemIcon(eItemType type)
    {
        return m_iconSprites[(int)type];
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        m_gameItemPool = new GameObjectPool<GameItem>(20, () =>
        {
            var obj = Instantiate(m_gameItemPrefab) as GameObject;
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            var item = obj.GetComponent<GameItem>();
            item.m_player = m_player;
            return item;
        });
        m_iconSprites = Resources.LoadAll<Sprite>("GameItem");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
