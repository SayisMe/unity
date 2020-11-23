using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer m_iconSpr;
    Rigidbody2D m_rigid;
    ItemManager.eItemType m_type;
    public PlayerController m_player { get; set; }
    
    public void SetItem(Vector3 pos, ItemManager.eItemType type)
    {
        transform.position = pos;
        m_rigid.isKinematic = false;
        m_type = type;
        m_iconSpr.sprite = ItemManager.Instance.GetItemIcon(type);
        transform.localRotation = Quaternion.identity;
        m_rigid.velocity = Vector3.zero;
        m_rigid.angularVelocity = 0f;
        var dir = ItemManager.Instance.m_player.transform.position - transform.position;
        m_rigid.AddForce(Vector3.up * 2.2f + Vector3.right * dir.normalized.x, ForceMode2D.Impulse);

        if(m_type >= ItemManager.eItemType.Gem_Red && m_type <= ItemManager.eItemType.Gem_Blue)
            m_rigid.AddTorque(dir.x < 0 ? -0.7f : 0.7f, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            ItemManager.Instance.RemoveItem(this);
            switch(m_type)
            {
                case ItemManager.eItemType.Coin:
                    SoundManager.Instance.PlaySFX(SoundManager.eSFXClip.Get_Coin);
                    ScoreManager.Instance.SetCoinCount(1);
                    break;
                case ItemManager.eItemType.Gem_Red:
                case ItemManager.eItemType.Gem_Green:
                case ItemManager.eItemType.Gem_Blue:
                    SoundManager.Instance.PlaySFX(SoundManager.eSFXClip.Get_Gem);
                    ScoreManager.Instance.SetCoinCount((int)m_type * 10);
                    break;
                case ItemManager.eItemType.Invincible:
                    SoundManager.Instance.PlaySFX(SoundManager.eSFXClip.Get_Invincible);
                    BuffManager.Instance.SetBuff(BuffManager.eBuffType.Invincible);
                    break;
                case ItemManager.eItemType.Magnet:
                    SoundManager.Instance.PlaySFX(SoundManager.eSFXClip.Get_Item);
                    BuffManager.Instance.SetBuff(BuffManager.eBuffType.Magnet);
                    break;
            }
        }
        else if(collision.tag.Equals("ColliderBottom"))
        {
            ItemManager.Instance.RemoveItem(this);
        }
        else if(collision.tag.Equals("Magnet"))
        {
            m_rigid.isKinematic = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Magnet"))
        {
            transform.position += (m_player.transform.position - transform.position).normalized * 8f * Time.deltaTime;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag.Equals("Magnet"))
        {
            m_rigid.isKinematic = false;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        m_iconSpr = GetComponentInChildren<SpriteRenderer>();
        m_rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
