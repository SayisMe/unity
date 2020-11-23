using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 m_dir;
    Vector3 m_startClickPos;
    Vector3 m_dragDir;
    Vector2 m_mousedir;
    [SerializeField]
    float m_speed = 14f;
    Rigidbody2D m_rigid;
    Animator m_animator;
    Vector3 m_prevPos;
    [SerializeField]
    Transform m_firePos;
    [SerializeField]
    GameObject m_bulletPrefab;
    [SerializeField]
    GameObject m_magnetFxObj;
    [SerializeField]
    GameObject m_invincibleFxObj;
    [SerializeField]
    GameObject m_shockwaveFxObj;
    GameObjectPool<BulletController> m_bulletPool;
    TweenScale m_shockWaveTween;
    bool m_isDrag;
    public void SetShockWaveEffect()
    {
        m_shockwaveFxObj.SetActive(true);
        m_shockWaveTween.ResetToBeginning();
        m_shockWaveTween.PlayForward();
    }
    public void ReleaseShockWaveEffect()
    {
        m_shockwaveFxObj.SetActive(false);
    }
    public void SetInvincibleEffect()
    {
        CancelInvoke("CreateBullet");
        m_animator.Play("Invincible", 0, 0f);
        m_invincibleFxObj.SetActive(true);
    }
    public void ReleaseInvincibleEffect()
    {
        InvokeRepeating("CreateBullet", 2f, 0.15f);
        m_animator.Play("Fly", 0, 0f);
        m_invincibleFxObj.SetActive(false);
    }
    public void SetMagnetEffect()
    {
        m_magnetFxObj.SetActive(true);
    }
    public void ReleaseMagnetEffect()
    {
        m_magnetFxObj.SetActive(false);
    }
    public void RemoveBullet(BulletController bullet)
    {
        bullet.gameObject.SetActive(false);
        m_bulletPool.Set(bullet);
    }
    public void SetDie()
    {
        CancelInvoke("CreateBullet");
        gameObject.SetActive(false);
    }
    void CreateBullet()
    {
        var bullet = m_bulletPool.Get();
        bullet.transform.position = m_firePos.position;
        bullet.gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Monster"))
        {
            if (GameManager.Instance.GetState() == GameManager.eGameState.Invincible) return;
            SetDie();
            GameManager.Instance.SetState(GameManager.eGameState.Result);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_shockWaveTween = GetComponentInChildren<TweenScale>();
        m_prevPos = transform.position;
        m_bulletPool = new GameObjectPool<BulletController>(15, () => 
        {
            var obj = Instantiate(m_bulletPrefab) as GameObject;
            obj.SetActive(false);
            var bullet = obj.GetComponent<BulletController>();
            return bullet;
        });
        m_magnetFxObj.SetActive(false);
        m_invincibleFxObj.SetActive(false);
        m_shockwaveFxObj.SetActive(false);
        InvokeRepeating("CreateBullet", 2f, 0.15f);
    }
   /* private void FixedUpdate()
    {
        m_dir = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        m_rigid.MovePosition(transform.position + m_dir * m_speed * Time.fixedDeltaTime);
    }*/
    // Update is called once per frame

    void Update()
    {        
        m_dir = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        if(Input.GetMouseButtonDown(0))
        {
            m_isDrag = true;
            m_startClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            /*
            m_mousedir = new Vector2(Input.mousePosition.x, screenPos.y);
            Vector2 pos = Camera.main.ScreenToWorldPoint(m_mousedir);
            if(m_mousedir.x <= Screen.width/2)
            {
                m_mousedir.x = Screen.width/2;
            }
            else if(m_mousedir.x >= Screen.width/2)
            {
                m_mousedir.x = Screen.width/2;
            }
            transform.position = pos;
            */
        }
        if(Input.GetMouseButtonUp(0))
        {
            m_isDrag = false;
        }
        if(m_isDrag)
        {
            var endClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_dragDir = endClickPos - m_startClickPos;
            m_dragDir = new Vector3(m_dragDir.x, 0f);
            m_startClickPos = endClickPos;
        }
        Vector3 tempPos = transform.position + m_dir * m_speed * Time.deltaTime;
        if (m_dragDir != Vector3.zero) tempPos = transform.position + m_dragDir;
        var screenPos = Camera.main.WorldToScreenPoint(tempPos);
        if(screenPos.x >= 0f && screenPos.x <= Screen.width)
        {
            transform.position = tempPos;
        }
        else if (screenPos.x < 0f)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0f, screenPos.y));            
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }
        else if (screenPos.x > Screen.width)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, screenPos.y));
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }
        
    }    
}
