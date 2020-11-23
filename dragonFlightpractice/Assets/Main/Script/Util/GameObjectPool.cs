using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool<T> where T : class
{
    public delegate T CreateFuncDelegate();
    CreateFuncDelegate CreateFunc; 
    Queue<T> m_queue;
    int m_count;
    public GameObjectPool(int count, CreateFuncDelegate createFunc) //만들 갯수, 어떤 걸 만들지
    {
        m_count = count;
        CreateFunc = createFunc;
        m_queue = new Queue<T>(m_count);
        Allocate();
    }
    void Allocate() // 실제로 메모리를 활성화
    {
        for(int i = 0; i< m_count; i++)
        {
            var obj = CreateFunc();
            m_queue.Enqueue(obj);
        }
    }
    public T Get()
    {
        if (m_queue.Count > 0)
            return m_queue.Dequeue();
        return CreateFunc();
    }
    public void Set(T item)
    {
        m_queue.Enqueue(item);
    }
}
