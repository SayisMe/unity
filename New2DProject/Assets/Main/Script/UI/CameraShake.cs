using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    float m_duration = 3f;
    [SerializeField]
    float m_power = 0.1f;
    bool m_isStart;
    float m_time;
    Vector3 m_orgPos;
    public void ShakeCamera(float duration, float power)
    {
        m_duration = duration;
        m_power = power;
        m_isStart = true;
        m_time = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_orgPos = transform.position;    //수정?
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isStart)
        {
            m_time += Time.deltaTime;
            var dir = (Vector3)(Random.insideUnitCircle.normalized * m_power);
            transform.position = m_orgPos + dir;
            if(m_time >= m_duration)
            {
                transform.position = m_orgPos;
                m_isStart = false;
            }
        }
    }
}
