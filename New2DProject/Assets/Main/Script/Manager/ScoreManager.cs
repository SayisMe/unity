using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    [SerializeField]
    UILabel m_flightScoreLabel;
    [SerializeField]
    UILabel m_huntScoreLabel;
    [SerializeField]
    UILabel m_coinCountLabel;
    [SerializeField]
    GameObject m_scoreObj;
    int m_flightScore;
    int m_huntScore;
    int m_coinCount;

    public void SetUI()
    {
        m_scoreObj.SetActive(true);
    }
    public void CloseUI()
    {
        m_scoreObj.SetActive(false);
    }
    public int SetFlightScore(float score)
    {
        m_flightScore += Mathf.RoundToInt(score);
        m_flightScoreLabel.text = string.Format("{0:n0}", m_flightScore);
        return m_flightScore;
    }
    public int GetFlightScore()
    {
        return m_flightScore;
    }
    public int SetHuntScore(int score)
    {
        m_huntScore += score;
        m_huntScoreLabel.text = string.Format("{0:n0}", m_huntScore);
        return m_huntScore;
    }
    public int GetHuntScore()
    {
        return m_huntScore;
    }
    public int SetCoinCount(int score)
    {
        m_coinCount += score;
        m_coinCountLabel.text = string.Format("{0:n0}", m_coinCount);
        return m_coinCount;
    }
    public int GetCoinCount()
    {
        return m_coinCount;
    }
    private void OnEnable()
    {
        SetUI();
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        SetFlightScore(0);
        SetHuntScore(0);
        SetCoinCount(0);
    }
}
