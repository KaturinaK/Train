using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalLevelPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] stars;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI penaltyText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private List<string> carriageUsedOnLevel;
    private int _coin;
    public void FillInfo(int star, string scoreInfo, int coin, int score, int penalty, int time, int level, int maxTime)
    {
        _coin = coin;
        for(int i = 0; i < star; i++)//подгружаем звезды из ресорсес
        {
            stars[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("star");
        }
        //check restorant
        AddCoinPerVagonRest();

        scoreText.text = scoreInfo.ToString();
        coinText.text = _coin.ToString();
        penaltyText.text = penalty.ToString();
        timeText.text = time + "/" + maxTime;

        if (penalty >= _coin)
        {
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 0);
        }
        else
        {
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + _coin);
        }
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + score);
        if(level % 10 == 0 && star > 0)
        {
            PlayerPrefs.SetInt("PageAccess", PlayerPrefs.GetInt("PageAccess") + 1);
        }

        Debug.Log(PlayerPrefs.GetInt("NewGameInfo") + "NewGameInfo");
        if (PlayerPrefs.GetInt("NewGameInfo") == 3)
            NewGameInfo.Instance.ShowPanelEducationOnLevel4();
    }
    public void LoadSceneMenuLevel()
    {
        GameController.Instance.SetInfoFinLevel();
    }
    public void ReloadScene()
    {
        GameController.Instance.ReloadScene();
    }
    private void AddCoinPerVagonRest()
    {
        carriageUsedOnLevel = gameObject.GetComponent<SaveTrain>().LoadGameInfo(PlayerPrefs.GetString("nameTrain"));

        foreach (string s in carriageUsedOnLevel)
        {
            if (GameObject.Find("EndLevelInfo").GetComponent<ShopDictionary>().TrainName(s) == "Ресторан")
            {
                _coin += Mathf.RoundToInt(_coin * 0.15f);
            }
            
        }
    }
}
