using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    Transform m_target;
    bool m_isMove;
    // Start is called before the first frame update
    void Start()
    {
        m_isMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        var screenPos = Camera.main.WorldToScreenPoint(m_target.position);
        if(screenPos.x > Screen.width/2 && !m_isMove)
        {
            m_isMove = true;
        }
        if(m_isMove)
        {
            transform.position = new Vector3(m_target.position.x, m_target.position.y > 0f ? m_target.position.y : 0f, transform.position.z);
        }
    }
}
