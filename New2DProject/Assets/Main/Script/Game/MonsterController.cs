using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    float m_speed = 3.5f;
    float m_speedScale = 1f;
    MonsterManager.eMonsterType m_type;
    Animator m_animator;
    SpriteRenderer[] m_parts;
    int m_hp;
    public int m_line { get; set; }
    public bool m_isAlive { get; set; }
    void AnimEvent_HitFinish()
    {
        m_animator.Play("Fly", 0, 0f);
    }
    public void SetSpeedScale(float scale)
    {
        m_speedScale = scale;
    }
    public void SetMonster(MonsterManager.eMonsterType type, int line)
    {
        m_type = type;
        ChangeParts(type);
        m_hp = (int)type + 1;
        m_line = line;
        m_isAlive = true;
    }
    public void SetDamage(int damage)
    {
        m_hp--;
        m_animator.Play("Hit", 0, 0f);
        if (m_hp <= 0)
        {
            if (m_type == MonsterManager.eMonsterType.Bomb)
            {
                MonsterManager.Instance.RemoveMonsters(m_line);
            }
            else
            {
                SetDie();
                MonsterManager.Instance.RemoveMonster(this);
            }
        }
    }
    public void SetDie()
    {
        m_isAlive = false;
        EffectManager.Instance.CreateEffect(transform.position);
        ItemManager.Instance.CreateItem(transform.position);
        SoundManager.Instance.PlaySFX(SoundManager.eSFXClip.Mon_Die);
        ScoreManager.Instance.SetHuntScore(((int)m_type + 1) * 112);

    }
    void ChangeParts(MonsterManager.eMonsterType type)
    {
        var parts = MonsterManager.Instance.GetMonsterParts(type);

        m_parts[0].sprite = parts[0];
        m_parts[1].sprite = m_parts[2].sprite = parts[3];
        m_parts[3].sprite = m_parts[4].sprite = parts[1];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("ColliderBottom"))
        {
            MonsterManager.Instance.RemoveMonster(this);
        }
        else if(collision.tag.Equals("ShockWave"))
        {
            m_isAlive = false;
            EffectManager.Instance.CreateEffect(transform.position);
            SoundManager.Instance.PlaySFX(SoundManager.eSFXClip.Mon_Die);
            MonsterManager.Instance.RemoveMonster(this);
        }
        else if(collision.tag.Equals("Invincible"))
        {
            SetDie();
            MonsterManager.Instance.RemoveMonster(this);
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        m_parts = GetComponentsInChildren<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * m_speed * m_speedScale * Time.deltaTime;
    }
}
