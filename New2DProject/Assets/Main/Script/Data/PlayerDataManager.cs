using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JsonFx.Json;

public class PlayerDataManager : DontDestroy<PlayerDataManager>
{
    const int BASIC_GOLD = 1000;
    PlayerData m_myData = new PlayerData();
    System.Text.StringBuilder m_sb = new System.Text.StringBuilder();
    public int GetCurHeroIndex()
    {
        return m_myData.m_curHeroIndex;
    }
    public void SetCurHeroIndex(int index)
    {
        m_myData.m_curHeroIndex = index;
        Save();
    }
    public void SetBestScore(int score)
    {
        if(score > m_myData.m_bestScore)
            m_myData.m_bestScore = score;
    }
    public int GetBestScore()
    {
        return m_myData.m_bestScore;
    }
    public int GetGold()
    {
        return m_myData.m_goldOwned;
    }
    public void IncreaseGold(int gold)
    {
        m_myData.m_goldOwned += gold;
    }
    public bool DecreaseGold(int gold)
    {
        if (m_myData.m_goldOwned - gold < 0)
            return false;
        m_myData.m_goldOwned -= gold;
        Save();
        return true;
    }
    public bool IsBuyHero(int index)
    {
        return m_myData.m_heroesOwned[index];
    }
    string ArrayToString<T>(T[] array)
    {
        m_sb.Clear();

        for(int i = 0; i< array.Length; i++)
        {
            m_sb.AppendFormat("{0}{1}", array[i], i < array.Length - 1 ? "," : string.Empty);
        }
        return m_sb.ToString();
    }
    void StringToArray<T>(string dataStr, T[] array)
    {
        var results = dataStr.Split(',');
        for(int i = 0; i< results.Length; i++)
        {
            array[i] = (T)System.Convert.ChangeType(results[i], typeof(T));
        }
    }
    public void Save()
    {
        /*
        PlayerPrefs.SetInt("PLAYER_DATA_BEST_SCORE", m_myData.m_bestScore);
        PlayerPrefs.SetInt("PLAYER_DATA_GOLD_OWNED", m_myData.m_goldOwned);
        PlayerPrefs.SetInt("PLAYER_DATA_CUR_HERO_INDEX", m_myData.m_curHeroIndex);
        var result = ArrayToString(m_myData.m_heroesOwned);
        PlayerPrefs.SetString("PLAYER_DATA_HEROES_OWNED", result); */
        var jsonData = JsonWriter.Serialize(m_myData);
        //Debug.Log(jsonData);
        PlayerPrefs.SetString("PLAYER_DATA", jsonData);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        /*m_myData.m_bestScore = PlayerPrefs.GetInt("PLAYER_DATA_BEST_SCORE", 0);
        m_myData.m_goldOwned = PlayerPrefs.GetInt("PLAYER_DATA_GOLD_OWNED", BASIC_GOLD);
        m_myData.m_curHeroIndex = PlayerPrefs.GetInt("PLAYER_DATA_CUR_HERO_INDEX", 0);
        var result = PlayerPrefs.GetString("PLAYER_DATA_HEROES_OWNED", string.Empty);
        if(!string.IsNullOrEmpty(result))
            StringToArray(result, m_myData.m_heroesOwned);
        for(int i = 0; i< m_myData.m_heroesOwned.Length;i++)
        {
            Debug.Log(m_myData.m_heroesOwned[i]);
        }*/
        var jsonData = PlayerPrefs.GetString("PLAYER_DATA", string.Empty);
        if(!string.IsNullOrEmpty(jsonData))
        {
            m_myData = JsonReader.Deserialize<PlayerData>(jsonData);
        }
    }
    // Start is called before the first frame update
    protected override void OnAwake()
    {
        //PlayerPrefs.DeleteAll();
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
