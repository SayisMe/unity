using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    float m_speed = 10f;
    [SerializeField]
    Vector3 m_dir = Vector3.right;
    [SerializeField]
    float m_lifeTime = 2f;
    [SerializeField]
    GameObject m_sfxExplosionPrefab;
    SpriteRenderer m_sprRen;
    Rigidbody2D m_rigid;

    void RemoveProjectile()
    {
        Destroy(gameObject);
    }
    void CreateExplosion(Vector3 pos)
    {
        var obj = Instantiate(m_sfxExplosionPrefab) as GameObject;
        var explosion = obj.GetComponent<SfxController>();
        explosion.SetEffect(pos);
    }
    public void SetProjectile(Vector3 pos, Vector3 dir)
    {
        transform.position = pos;
        m_dir = dir;
        m_sprRen.flipX = dir.x < 0f ? true : false;
        //m_rigid.AddForce(m_dir * m_speed, ForceMode2D.Impulse);
       // Invoke("RemoveProjectile", m_lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Background"))
        {
            CreateExplosion(transform.position);
            Destroy(gameObject);
        }
    }
    /*private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag.Equals("Background"))
        {
            CreateExplosion(transform.position);
            Destroy(gameObject);            
        }
    }
   /* private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }*/
    // Start is called before the first frame update
    void Awake()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_sprRen = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += m_dir * m_speed * Time.deltaTime;
    }
}
