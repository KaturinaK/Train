using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeImprovment : MonoBehaviour
{
    [SerializeField] private Image[] starsImage;
    [SerializeField] private TextMeshProUGUI costCoin;
    [SerializeField] private TextMeshProUGUI costScore;
    [SerializeField] private TextMeshProUGUI nameOfImprovment;
    [SerializeField] private TextMeshProUGUI nowParamUpgrade;
    [SerializeField] private Button plus;
    private int stars;
    private int cost;
    private int scoreCost = 5;
    private int maxImprovmentLevel = 3;
    private string nameImprovment;
    
    public void FillInfo(string nameOfUpgread, int levelOfUpgread, int coin, int score  )
    {
        nameImprovment = nameOfUpgread;
        nameOfImprovment.text = nameImprovment;

        FillStars(levelOfUpgread);
        CountCostOfImprovment(levelOfUpgread);
        CheckButtonInterectable(levelOfUpgread, coin, score);

        nowParamUpgrade.text = InfoNowTrainParameters.Instance.InfoNowTrainParam(gameObject.name);
    }

    private void CountCostOfImprovment(int levelOfUpgread )
    {
        int levelTrain = PlayerPrefs.GetInt(PlayerPrefs.GetString("nameTrain"));
        cost = (800 + levelOfUpgread * 400) + 100 * levelTrain;

        costCoin.text = cost.ToString();

        scoreCost = (20 + levelOfUpgread * 10) + 10 * levelTrain;

        costScore.text = scoreCost.ToString();
    }

    private void FillStars(int star)
    {
        stars = star;
        for (int i = 0; i < stars; i++)
        {
            starsImage[i].sprite = Resources.Load<Sprite>("star");
        }
    }
    private void CheckButtonInterectable(int levelOfUpgread, int coin, int score)
    {

        if(levelOfUpgread < maxImprovmentLevel && coin >= cost && scoreCost < score)
        {
             plus.interactable = true;
        }
        else 
        {
            plus.interactable = false;
            
        }
        if(levelOfUpgread > maxImprovmentLevel)
        {
            costCoin.gameObject.SetActive(false);
            costScore.gameObject.SetActive(false);
        }
    }
    public void PressButtonPlus()
    {
        PersonalArea.Instance.BuySlotOfUpgrade(cost, nameImprovment, scoreCost);

    }
}
