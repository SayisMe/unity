using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxController : MonoBehaviour
{   
    float m_lifeTime = 1f;
    Animator m_animator;
    void RemoveEffect()
    {
        Destroy(gameObject);
    }
    float GetAnimationClipLength(string animationName)
    {
        RuntimeAnimatorController ac = m_animator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name.Equals(animationName))
            {
                return ac.animationClips[i].length;
            }
        }
        return 0;
    }
    public void SetEffect(Vector3 pos)
    {
        transform.position = pos;
        Invoke("RemoveEffect", m_lifeTime);
    }
    // Start is called before the first frame update
    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_lifeTime = GetAnimationClipLength("explosion");
    }   
}
