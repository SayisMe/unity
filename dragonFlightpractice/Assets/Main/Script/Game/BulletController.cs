using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    float m_speed = 10f;

    float m_lifeTime = 2f;
    PlayerController m_player;
    void RemoveBullet()
    {
        m_player.RemoveBullet(this);
    }
    private void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnEnable()
    {
        Invoke("RemoveBullet", m_lifeTime);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * m_speed * Time.deltaTime;
    }
}
