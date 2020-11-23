using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float m_speed = 14f;
    Rigidbody2D m_rigid;
    Vector3 m_dir;
    Vector3 m_prevPos;
    [SerializeField]
    Transform m_firePos;
    [SerializeField]
    GameObject m_bulletPrefab;
    GameObjectPool<BulletController> m_bulletPool;
    public void RemoveBullet(BulletController bullet)
    {
        bullet.gameObject.SetActive(false);
        m_bulletPool.Set(bullet);
    }
    
    void CreateBullet()
    {
        var bullet = m_bulletPool.Get();
        bullet.transform.position = m_firePos.position;
        bullet.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_prevPos = transform.position;
        m_bulletPool = new GameObjectPool<BulletController>(15, () =>   //람다로 그냥 간단히 함수 구현
        {
            var obj = Instantiate(m_bulletPrefab) as GameObject;
            obj.SetActive(false);
            var bullet = obj.GetComponent<BulletController>();
            return bullet;
        }
        );
        InvokeRepeating("CreateBullet", 2f, 0.15f);
    }

    // Update is called once per frame
    /*
    private void FixedUpdate()
    {
        m_dir = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        m_rigid.MovePosition(transform.position + m_dir * m_speed * Time.fixedDeltaTime);
        
    }
    */
    void Update()
    {
        m_dir = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        Vector3 tempPos = transform.position + m_dir * m_speed * Time.deltaTime;
        var screenPos = Camera.main.WorldToScreenPoint(tempPos);
        if(screenPos.x >= 0f && screenPos.x <= Screen.width)
        {
            transform.position = tempPos;
        }
        else if(screenPos.x < 0f)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0f, screenPos.y));
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }
        else if(screenPos.x > Screen.width)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, screenPos.y));
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }
    }
    private void LateUpdate()
    {
        m_prevPos = transform.position;
    }
}
