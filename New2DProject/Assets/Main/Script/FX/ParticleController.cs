using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    ParticleSystem[] m_particles;
    public void SetEffect(Vector3 position)
    {
        transform.position = position;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_particles = GetComponentsInChildren<ParticleSystem>();
    }
    void Update()
    {
        bool isPlaying = false;
        for(int i = 0; i < m_particles.Length; i++)
        {
            if(m_particles[i].isPlaying)
            {
                isPlaying = true;
                break;
            }
        }
        if(!isPlaying)
        {
            EffectManager.Instance.RemoveEffect(this);
        }
    }
}
