using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : SingletonMonobehaviour<MonsterManager>
{
    [SerializeField]
    GameObject m_monsterPrefab;
    GameObjectPool<MonsterController> m_monsterPool;
    Vector2 m_startPos = new Vector2(-2.31f, 7f);
    float m_posGap = 1.14f;
    public void RemoveMonster(MonsterController monster)
    {
        monster.gameObject.SetActive(false);
        m_monsterPool.Set(monster);
    }
    void CreateMonsters()
    {
        for(int i = 0; i<5; i++)
        {
            var mon = m_monsterPool.Get();
            mon.transform.position = m_startPos + Vector2.right * i * m_posGap;
            mon.gameObject.SetActive(true);
        }
    }
    
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_monsterPool = new GameObjectPool<MonsterController>(20, () =>
        {
            var obj = Instantiate(m_monsterPrefab) as GameObject;
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            var mon = obj.GetComponent<MonsterController>();
            return mon;
        });
        InvokeRepeating("CreateMonsters", 3f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
