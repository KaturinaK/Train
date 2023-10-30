using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public static HUD Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

    }
    static private HUD _instance;

    void Start()
    {
        
        ShowTextCoin(GameController.Instance.Coin );
        ShowTextScore(GameController.Instance.Score);
    }

    public void ShowTextCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
    public void ShowTextScore( int score)
    {
        scoreText.text = score.ToString();
    }
    
}
