using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.GraphicsBuffer;

public class PersonalArea : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private TextMeshProUGUI textTrainLevel;
    [SerializeField] private TextMeshProUGUI textTrainName;
    [SerializeField] private GameObject slotUpgreadPrefab;
    [SerializeField] private Transform content;
    private int score = 0;
    private int coin = 0;
    private int improvmentLevel;
    private string trainUserName;
    private string nameTrain;
    private ImprovementCharacteristic improvement;
    private List<string> improvmentName = new List<string>() { "Престиж (чем выше уровень престижа, тем больше пассажиров садятся в твой поезд)", "Максимальная скорость", "Ускорение", "Торможение", "Место для вагонов" };
    private List<GameObject> improvmentList = new List<GameObject>();

   // private int[] costOfLevel = new int[] { 800, 1200, 1600 };

    public static PersonalArea Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        nameTrain = PlayerPrefs.GetString("nameTrain");

        ShowParam();
    }

    static private PersonalArea _instance;

    private void Start()
    {
        for (int i = 0; i < improvmentName.Count; i++)
        {
            improvmentLevel = improvement.improvmentLevels[i];

            GameObject slot = Instantiate(slotUpgreadPrefab, content, false);
            slot.name = i.ToString();
            slot.GetComponent<UpgradeImprovment>().FillInfo(improvmentName[i], improvmentLevel, coin, score);

            improvmentList.Add(slot);
        }

        if (PlayerPrefs.GetInt("NewGameInfo") == 4)
            NewGameInfo.Instance.ShowPanelEducationPersonalArea();
    }
    private void ShowParam()
    {
        trainUserName = gameObject.GetComponent<ShopDictionary>().TrainName(nameTrain);

        score = PlayerPrefs.GetInt("score");
        textScore.text = score.ToString();

        coin = PlayerPrefs.GetInt("coin");
        textCoin.text = coin.ToString();

        textTrainName.text = trainUserName;

        improvement = GetComponent<Improvements>().TakeLevel(nameTrain);

        textTrainLevel.text = PlayerPrefs.GetInt(PlayerPrefs.GetString("nameTrain")).ToString();
    }
    public void BuySlotOfUpgrade(int costCoin, string nameUpgread, int costScore)//если nameUpgread == LevelCarriageCount, добавлять в список вагонов призрак
    {
        coin -= costCoin;
        textCoin.text = coin.ToString();
        PlayerPrefs.SetInt("coin", coin);

        score -= costScore;
        textScore.text = score.ToString();
        PlayerPrefs.SetInt("score", score);

        int index = improvmentName.IndexOf(nameUpgread);
        Improvements.Instance.GetUpgrade(nameTrain, index);
        improvement = GetComponent<Improvements>().improvementsInfo[nameTrain];

        textTrainLevel.text = PlayerPrefs.GetInt(PlayerPrefs.GetString("nameTrain")).ToString();

        for (int i =0; i< improvmentList.Count; i++)
        {
            improvmentLevel = improvement.improvmentLevels[i];
            improvmentList[i].GetComponent<UpgradeImprovment>().FillInfo(improvmentName[i], improvmentLevel, coin, score);
        }

        if(nameUpgread == "Место для вагонов")
        {
            GetComponent<SaveGame>().AddGhostToListCarriagesUsedOnLevel(PlayerPrefs.GetString("nameTrain"));
        }
    }


    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public List<GameObject> SlotList()
    {
        return improvmentList;
    }
}
