using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePad : SingletonMonoBehaviour<MovePad>
{
    [SerializeField]
    UISprite m_padBG;
    [SerializeField]
    UISprite m_padBtn;
    [SerializeField]
    Camera m_uiCamera;
    float m_maxDist = 0.2f;
    float m_maxSqrDist;    
    Vector3 m_dir;
    bool m_isDrag;
    RaycastHit m_hit;
    int m_fingerID;
    public Vector2 GetAxis()
    {
        return m_dir.normalized;
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_uiCamera = GameObject.Find("UI Root").GetComponentInChildren<Camera>();        
        m_maxSqrDist = m_maxDist * m_maxDist;
        m_fingerID = -1;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)
            || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)
            || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)
            || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            m_dir = Vector3.zero;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            m_dir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            m_dir += Vector3.right;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            m_dir += Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            m_dir += Vector3.down;
        }

        m_padBtn.transform.position = transform.position + m_dir.normalized * m_maxDist;
        //click
        if (Input.GetMouseButtonDown(0))
        {
            m_isDrag = true;
            var ray = m_uiCamera.ScreenPointToRay(Input.mousePosition);
            m_hit = new RaycastHit();
            if (Physics.Raycast(ray, out m_hit, 100f, 1 << LayerMask.NameToLayer("UI")))
            {
                if (m_hit.collider.transform == m_padBG.transform)
                {
                    m_dir = m_hit.point - transform.position;
                    if (m_dir.sqrMagnitude > m_maxSqrDist)
                    {
                        m_padBtn.transform.position = transform.position + m_dir.normalized * m_maxDist;
                    }
                    else
                    {
                        m_padBtn.transform.position = transform.position + m_dir;
                    }
                }
                else
                {
                    m_hit = new RaycastHit();
                }
            }

        }
        //drag
        if (m_isDrag && m_hit.collider != null)
        {
            m_dir = m_uiCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (m_dir.sqrMagnitude > m_maxSqrDist)
            {
                m_padBtn.transform.position = transform.position + m_dir.normalized * m_maxDist;
            }
            else
            {
                m_padBtn.transform.position = transform.position + m_dir;
            }
        }
        //drag end
        if (Input.GetMouseButtonUp(0))
        {
            m_isDrag = false;
            m_padBtn.transform.localPosition = Vector3.zero;
            m_dir = Vector3.zero;
        }
#elif UNITY_ANDROID || UNITY_IPHONE
        for (int i = 0; i < Input.touchCount; i++)
        {
            if(Input.touches[i].phase == TouchPhase.Began)//touch
            {
                //m_isDrag = true;
                var ray = m_uiCamera.ScreenPointToRay(Input.touches[i].position);
                m_hit = new RaycastHit();
                if (Physics.Raycast(ray, out m_hit, 100f, 1 << LayerMask.NameToLayer("UI")))
                {
                    if (m_hit.collider.transform == m_padBG.transform)
                    {
                        m_dir = m_hit.point - transform.position;
                        if (m_dir.sqrMagnitude > m_maxSqrDist)
                        {
                            m_padBtn.transform.position = transform.position + m_dir.normalized * m_maxDist;
                        }
                        else
                        {
                            m_padBtn.transform.position = transform.position + m_dir;
                        }
                        m_fingerID = Input.touches[i].fingerId;
                    }
                    else
                    {
                        m_hit = new RaycastHit();
                    }
                }
            }
            if(Input.touches[i].phase == TouchPhase.Moved && m_fingerID == Input.touches[i].fingerId)
            {
                m_dir = m_uiCamera.ScreenToWorldPoint(Input.touches[i].position) - transform.position;

                if (m_dir.sqrMagnitude > m_maxSqrDist)
                {
                    m_padBtn.transform.position = transform.position + m_dir.normalized * m_maxDist;
                }
                else
                {
                    m_padBtn.transform.position = transform.position + m_dir;
                }
            }
            if((Input.touches[i].phase == TouchPhase.Ended || Input.touches[i].phase == TouchPhase.Canceled) && Input.touches[i].fingerId == m_fingerID)
            {
                m_fingerID = -1;
                m_padBtn.transform.localPosition = Vector3.zero;
                m_dir = Vector3.zero;
            }
        }
#endif
    }
}

