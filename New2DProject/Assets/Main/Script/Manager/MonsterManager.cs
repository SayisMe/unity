using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : SingletonMonoBehaviour<MonsterManager>
{
    [System.Serializable]
    public class MonsterParts
    {
        public Sprite[] m_parts;
    }
    public enum eMonsterType
    {
        White,
        Yellow,
        Pink,
        Bomb,
        Max
    }
    [SerializeField]
    GameObject m_monsterPrefab;
    GameObjectPool<MonsterController> m_monsterPool;
    Vector2 m_startPos = new Vector2(-2.31f, 7f);
    
    [SerializeField]
    MonsterParts[] m_partsSpr;      //사실상 2차원 배열 구조
    List<MonsterController> m_monsterList = new List<MonsterController>();
    float m_posGap = 1.14f;
    int m_line;
    float m_scale = 1f;
    public void SetMonsterCreate(float scale)
    {
        m_scale = scale;
        for(int i = 0; i < m_monsterList.Count; i++)
        {
            m_monsterList[i].SetSpeedScale(m_scale);
        }
        CancelInvoke("CreateMonsters");
        InvokeRepeating("CreateMonsters", 3f / m_scale, 5f / m_scale);
    }
    public void CancelMonsterCreate()
    {
        CancelInvoke("CreateMonsters");

    }
    public Sprite[] GetMonsterParts(eMonsterType type)
    {
        return m_partsSpr[(int)type].m_parts;
    }
    
    public void RemoveMonster(MonsterController monster)
    {
        if (m_monsterList.Remove(monster))
        {
            m_monsterPool.Set(monster);
            monster.gameObject.SetActive(false);
        }   
    }
    public void RemoveMonsters(int line)
    {
        for(int i = 0; i< m_monsterList.Count; i++)
        {
            if(m_monsterList[i].m_line == line)
            {
                m_monsterList[i].SetDie();
                m_monsterList[i].gameObject.SetActive(false);
                m_monsterPool.Set(m_monsterList[i]);
            }
        }
        m_monsterList.RemoveAll(mon => mon.m_isAlive == false);
    }
    void CreateMonsters()
    {
        var type = eMonsterType.White;
        bool isBomb = false;
        bool check = false;
        for (int i = 0; i < 5; i++)
        {
            var mon = m_monsterPool.Get();
            do
            {
                type = (eMonsterType)Random.Range((int)eMonsterType.White, (int)eMonsterType.Max);
                if (type == eMonsterType.Bomb && !isBomb)
                {
                    isBomb = true;
                    check = false;
                }
                else if(type == eMonsterType.Bomb && isBomb)
                {
                    check = true;
                }
                else
                {
                    check = false;
                }
            } while(check);
            
            mon.SetMonster(type, m_line);
            mon.SetSpeedScale(m_scale);
            mon.transform.position = m_startPos + Vector2.right *  (i * m_posGap);
            mon.gameObject.SetActive(true);
            m_monsterList.Add(mon);
        }
        m_line++;
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_monsterPool = new GameObjectPool<MonsterController>(20, () => {
            var obj = Instantiate(m_monsterPrefab) as GameObject;
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            var mon = obj.GetComponent<MonsterController>();
            return mon;
        });
        m_partsSpr = new MonsterParts[(int)eMonsterType.Max];
        for(int i = 0; i< (int)eMonsterType.Max; i++)
        {
            m_partsSpr[i] = new MonsterParts();
            m_partsSpr[i].m_parts = Resources.LoadAll<Sprite>(string.Format("Monster/dragon_{0:00}", i+1));
        }
        InvokeRepeating("CreateMonsters", 3f, 5f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
