using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    [SerializeField]
    //[Range(0.1f, 2f)]
    float m_speed = 0.5f;
    [SerializeField]
    Animator m_animator;
    [SerializeField]
    GameObject m_projectilePrefab;
    [SerializeField]
    Transform m_firePos;
    [SerializeField]
    AudioClip m_gunFireSound;
    [SerializeField]
    GameItemInventory m_myInven;
    AudioSource m_audio;
    SpriteRenderer m_sprRen;
    Rigidbody2D m_rigid;
    BoxCollider2D m_collider;
    Vector3 m_dir;
    bool m_isFire;
    bool m_isFall;
    bool m_isGrounded;
    float m_moveAmount;
    float m_checkDist;
    void PlaySound()
    {
        m_audio.PlayOneShot(m_gunFireSound);
    }
    void CreateProjectile()
    {
        var obj = Instantiate(m_projectilePrefab) as GameObject;
        var projectile = obj.GetComponent<ProjectileController>();
        projectile.SetProjectile(m_firePos.position, transform.eulerAngles.y == 0 ? Vector3.left : Vector3.right);
        //obj.transform.position = m_firePos.position;
    }
    public void OnPressAttack()
    {
        m_animator.SetBool("IsFire", true);
    }
    public void OnReleaseAttack()
    {
        m_animator.SetBool("IsFire", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Ground"))
        {              
            m_animator.SetInteger("JumpState", 0);
           // m_rigid.velocity = Vector2.zero;
            m_isFall = false;
            m_isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Ground"))
        {
            m_isGrounded = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        m_rigid = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_sprRen = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<BoxCollider2D>();
    }
    /*private void OnGUI()
    {
        if (GUI.Button(new Rect((Screen.width - 200) / 2, (Screen.height - 60) / 2, 200, 60), "TITLE"))
        {
            LoadSceneManager.Instance.LoadSceneAsync("Title");
        }        
    }*/
    private void FixedUpdate()
    {
     //   if (!m_isFire)
     //       m_rigid.AddForce(m_dir * m_speed * Time.fixedDeltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        var padDir = MovePad.Instance.GetAxis();

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            m_animator.SetBool("IsMove", false);
            m_dir = Vector3.zero;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_animator.SetBool("IsMove", false);
            m_dir = Vector3.zero;
        }
        if(padDir == Vector2.zero)
        {
            m_animator.SetBool("IsMove", false);
            m_dir = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || padDir.x < 0f)
        {
            //gameObject.transform.position  += Vector3.left * m_speed * Time.deltaTime;
            m_dir = Vector3.left;
            m_animator.SetBool("IsMove", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            //m_sprRen.flipX = false;
        }        
        if (Input.GetKeyDown(KeyCode.RightArrow) || padDir.x > 0f)
        {
            //gameObject.transform.position += Vector3.right * m_speed * Time.deltaTime;
            m_dir = Vector3.right;
            m_animator.SetBool("IsMove", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            //m_sprRen.flipX = true;
        }
        if (padDir != Vector2.zero)
            m_dir = new Vector3(padDir.x, 0f, 0f);
        if(Input.GetKeyDown(KeyCode.I))
        {
            m_myInven.HandleInventory();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnPressAttack();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnReleaseAttack();
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            m_rigid.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
            m_animator.SetInteger("JumpState", 1);                        
        }       
        var stateInfo = m_animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("fire"))
            m_isFire = true;
        else
            m_isFire = false;
        m_moveAmount = m_speed * Time.deltaTime;
        m_checkDist = m_moveAmount;
        if (m_moveAmount < (m_collider.bounds.size.x / 2) - m_collider.offset.x)
        {
            m_checkDist = (m_collider.bounds.size.x / 2) - m_collider.offset.x;
        }
        var hit = Physics2D.Raycast(transform.position, m_dir, m_checkDist, -1 & (~(1 << LayerMask.NameToLayer("Player"))));
        if(hit.collider != null)
        {
            m_moveAmount = hit.distance - ((m_collider.bounds.size.x / 2) - m_collider.offset.x);
        }        
        Debug.DrawRay(transform.position, m_dir * m_checkDist, Color.magenta);
        if (!m_isFire)
            gameObject.transform.position += m_dir * m_moveAmount;

        if (m_rigid.velocity.y < 0f)
        {            
            if (!m_isFall && !m_isGrounded)
            {                
                m_isFall = true;
                m_animator.SetInteger("JumpState", 2);
            }
        }
    }
}
