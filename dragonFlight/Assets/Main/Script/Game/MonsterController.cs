
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public enum eMonsterType
    {
        White,
        Yellow,
        Pink,
        Max
    }
    [SerializeField]
    float m_speed = 2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("ColliderBottom"))
        {
            MonsterManager.Instance.RemoveMonster(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * m_speed * Time.deltaTime;
    }
}
