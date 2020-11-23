using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int m_bestScore;
    public int m_curHeroIndex;
    public int m_goldOwned;
    public bool[] m_heroesOwned;
    public PlayerData()
    {
        m_heroesOwned = new bool[13];
        m_heroesOwned[0] = true;
        m_heroesOwned[10] = true;
    }
}
