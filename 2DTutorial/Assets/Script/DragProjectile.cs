using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragProjectile : MonoBehaviour
{
    Rigidbody2D m_rigid;
    SpringJoint2D m_springJoint;
    Transform m_catapultTransform;
    Vector2 m_prevVelocity;
    Vector3 m_startPos;
    LineRenderer[] m_bandLine;

    bool m_isDrag;

    float m_maxDist = 8f;
    float m_sqrMaxDist;
    float m_radius;

    private void OnMouseDown()
    {
        m_isDrag = true;
        m_springJoint.enabled = true;
    }    
    private void OnMouseUp()
    {
        m_rigid.isKinematic = false;
        m_isDrag = false;
    }
    void Initilaize()
    {
        m_rigid.isKinematic = true;
        m_springJoint.enabled = false;
    }
    void OnEnable()
    {
        Initilaize();
        InitBand();
    }
    void InitBand()
    {
        m_bandLine[0].startWidth = 0.3f;
        m_bandLine[0].endWidth = 0.3f;
        m_bandLine[0].SetPosition(0, m_bandLine[0].transform.position);
        m_bandLine[0].sortingLayerName = "Middle";
        m_bandLine[0].sortingOrder = 2;

        m_bandLine[1].startWidth = 0.4f;
        m_bandLine[1].endWidth = 0.4f;
        m_bandLine[1].SetPosition(0, m_bandLine[1].transform.position);
        m_bandLine[1].sortingLayerName = "Middle";
        m_bandLine[1].sortingOrder = 4;

        m_bandLine[0].SetPosition(1, m_startPos);
        m_bandLine[1].SetPosition(1, m_startPos);
    }
    void DrawBand()
    {
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos = new Vector3(targetPos.x, targetPos.y, 0f);
        var dir = targetPos - m_catapultTransform.position;
        m_bandLine[0].SetPosition(1, transform.position + dir.normalized * m_radius);
        m_bandLine[1].SetPosition(1, transform.position + dir.normalized * m_radius);
    }
    // Start is called before the first frame update
    void Awake()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_springJoint = GetComponent<SpringJoint2D>();
        m_catapultTransform = m_springJoint.connectedBody.transform;
        m_sqrMaxDist = m_maxDist * m_maxDist;
        m_bandLine = m_catapultTransform.parent.GetComponentsInChildren<LineRenderer>();
        m_startPos = transform.position;
        m_radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isDrag)
        {
            var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos = new Vector3(targetPos.x, targetPos.y, 0f);
            var dir = targetPos - m_catapultTransform.position;
            if(dir.sqrMagnitude > m_sqrMaxDist)
            {
                transform.position = m_catapultTransform.position + dir.normalized * m_maxDist;
            }
            else
            {
                transform.position = m_catapultTransform.position + dir;
            }
        }
        else if(!m_rigid.isKinematic && m_springJoint.enabled)
        {
            if(m_rigid.velocity.sqrMagnitude < m_prevVelocity.sqrMagnitude)
            {
                m_springJoint.enabled = false;
                m_rigid.velocity = m_prevVelocity;
                InitBand();
            }
            m_prevVelocity = m_rigid.velocity;
        }
        if(m_springJoint.enabled)
        {
            DrawBand();
        }
    }
}
