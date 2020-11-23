using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingletonMonoBehaviour<EffectManager>
{
    [SerializeField]
    GameObject m_explosionFxPrefab;
    GameObjectPool<ParticleController> m_effectPool;

    public void CreateEffect(Vector3 pos)
    {
        var effect = m_effectPool.Get();
        effect.gameObject.SetActive(true);
        effect.SetEffect(pos);
    }
    public void RemoveEffect(ParticleController effect)
    {
        effect.gameObject.SetActive(false);
        m_effectPool.Set(effect);
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_effectPool = new GameObjectPool<ParticleController>(20, () =>
        {
            var obj = Instantiate(m_explosionFxPrefab) as GameObject;
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            var effect = obj.GetComponent<ParticleController>();
            return effect;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
