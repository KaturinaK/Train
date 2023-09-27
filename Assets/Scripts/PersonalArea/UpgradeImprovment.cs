using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeImprovment : MonoBehaviour
{
    [SerializeField] private Image[] starsImage;
    private int stars;

    private int cost;
    [SerializeField] private TextMeshProUGUI costCoin;

    private int scoreCost = 5;
    [SerializeField] private TextMeshProUGUI costScore;

    private int maxImprovmentLevel = 3;

    private string nameImprovment;
    [SerializeField] private TextMeshProUGUI nameOfImprovment;

    [SerializeField] private Button plus;

    [SerializeField] private TextMeshProUGUI nowParamUpgrade;

    public void FillInfo(string nameOfUpgread, int levelOfUpgread, int coin, int score  )
    {

        nameImprovment = nameOfUpgread;
        nameOfImprovment.text = nameImprovment;

        FillStars(levelOfUpgread);
        CountCostOfImprovment(levelOfUpgread);
        CheckButtonInterectable(levelOfUpgread, coin, score);

        nowParamUpgrade.text = InfoNowTrainParameters.Instance.InfoNowTrainParam(gameObject.name);
        //InfoNowTrainParameters.Instance.TakeLevelsList();
       // Debug.Log(gameObject.name + "slot");
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
