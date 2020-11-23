using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    [SerializeField]
    UI2DSprite m_sdSprite;
    [SerializeField]
    UILabel m_totalScoreLabel;
    [SerializeField]
    GameObject m_bestRecordObj;
    [SerializeField]
    UILabel m_flightScoreLabel;
    [SerializeField]
    UILabel m_huntScoreLabel;
    [SerializeField]
    UILabel m_coinCountLabel;
    [SerializeField]
    UILabel m_bestScoreLabel;

    public void SetResult()
    {
        bool isBest = false;
        gameObject.SetActive(true);
        m_bestRecordObj.SetActive(false);
        int totalScore = ScoreManager.Instance.GetFlightScore() + ScoreManager.Instance.GetHuntScore();
        if(totalScore > PlayerDataManager.Instance.GetBestScore())
        {
            isBest = true;
            m_bestRecordObj.SetActive(true);
            PlayerDataManager.Instance.SetBestScore(totalScore);
        }
        m_sdSprite.sprite2D = Resources.Load<Sprite>(string.Format("SD/sd_{0:00}{1}", PlayerDataManager.Instance.GetCurHeroIndex() + 1, isBest ? "_highscore" : string.Empty));
        m_bestScoreLabel.text = string.Format("{0:n0}", PlayerDataManager.Instance.GetBestScore());
        m_totalScoreLabel.text = string.Format("{0:n0}", totalScore);
        m_flightScoreLabel.text = string.Format("{0:n0}", ScoreManager.Instance.GetFlightScore());
        m_huntScoreLabel.text = string.Format("{0:n0}", ScoreManager.Instance.GetHuntScore());
        m_coinCountLabel.text = string.Format("{0:n0}", ScoreManager.Instance.GetCoinCount());
        PlayerDataManager.Instance.Save();

    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
