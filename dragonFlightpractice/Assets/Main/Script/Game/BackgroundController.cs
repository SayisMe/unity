using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    SpriteRenderer m_sprRen;
    [SerializeField]
    float m_speed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        m_sprRen = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_sprRen.material.mainTextureOffset += Vector2.up * m_speed * Time.deltaTime;
    }
}
