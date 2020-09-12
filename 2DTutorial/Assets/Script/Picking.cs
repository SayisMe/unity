using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picking : MonoBehaviour
{
    Ray m_ray;
    RaycastHit m_rayHit;
    GameObject m_selectObject;
    GameObject GetTouchedObject()
    {
        m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(m_ray, out m_rayHit, 1000f))
        {
            return m_rayHit.collider.gameObject;
        }
        return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            m_selectObject = GetTouchedObject();
            if(m_selectObject != null)
            {
                m_selectObject.transform.position = new Vector3(m_selectObject.transform.position.x, m_selectObject.transform.position.y, -1f);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(m_selectObject != null)
            {
                m_selectObject.transform.position = new Vector3(m_selectObject.transform.position.x, m_selectObject.transform.position.y, 0f);
            }
        }
        if(m_selectObject != null)
            Debug.DrawRay(m_ray.origin, m_ray.direction * m_rayHit.distance, Color.red);
        else
            Debug.DrawRay(m_ray.origin, m_ray.direction * 1000f, Color.green);
    }
}
