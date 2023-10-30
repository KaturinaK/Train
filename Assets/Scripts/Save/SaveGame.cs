using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

class SaveNeed
{
    public int Score;
    
    public List<int> StarsOnLevel;


}
class SaveImprovement
{
    public List<int> improvementLevel;
}
public class SaveGame : MonoBehaviour
{
    [SerializeField] Transform canvas;
    [SerializeField] GameObject panelNewGameQuestion;

    public void CreatePanelNewGameQuestion()
    {
        if (PlayerPrefs.GetInt("isGameStart") == 0)
        {
            NewGame();
        }
        else
        { GameObject panelWithQuestion = Instantiate(panelNewGameQuestion, canvas, false); }
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("coin", 10000);//0
        PlayerPrefs.SetInt("score", 1000);//0
        PlayerPrefs.SetString("nameTrain", "train1");
        PlayerPrefs.SetInt("PageAccess", 1);///1
        PlayerPrefs.SetInt("10button", 0);
        PlayerPrefs.SetInt("Page", 1);
        PlayerPrefs.SetInt(PlayerPrefs.GetString("nameTrain"), 0);
        PlayerPrefs.SetInt("isGameStart", 1);
        PlayerPrefs.SetInt("NewGameInfo", 1);

        CreateInfoStart();
        CreateInfoTrainStart();
        SceneManager.LoadScene("MenuLevel", LoadSceneMode.Single);
        //фуункция создание списка для колва купленных ввагонов
        CreateListCarriagesYouHave(PlayerPrefs.GetString("nameTrain"));
        CreateListImprovements();


    }
    public void ContinueGame()
    {
        
        SceneManager.LoadScene("MenuLevel", LoadSceneMode.Single);
    }
    //загрузка сцены магазина с кнопки
    public void Shop()
    {
        SceneManager.LoadScene("Shop", LoadSceneMode.Single);
    }
    public void LoadScenePersonalArea()
    {
        SceneManager.LoadScene("PersonalArea", LoadSceneMode.Single);
    }
    public void CreateInfoStart()//при новой игре. 
    {
        SaveNeed saveNeed = new SaveNeed() { StarsOnLevel = new List<int>() , Score = 0};
        for (int i = 0; i < 40; i++)
        {
            saveNeed.StarsOnLevel.Add(0);
        }

        SaveSystem.Set("SaveGame", saveNeed);
        
    }
    public void CreateInfoTrainStart()
    {
        for (int i = 1; i <= 3; i++)
        {

            SaveNeedTrain saveNewGame = new SaveNeedTrain() { CarriageUsedOnLevel = new List<string>() };
            string s = "SaveTrain" + "train" + i;
                                               
            saveNewGame.CarriageUsedOnLevel.Add("train" + i);

            saveNewGame.CarriageUsedOnLevel.Add("carriage1");
            SaveSystem.Set(s, saveNewGame);
        }
    }
    public void CreateListCarriagesYouHave(string nameTrain)
    {
        for (int i = 1; i <= 3; i++)
        {
            SaveNeedTrain listIntCarriages = new SaveNeedTrain() { CarriagesYouHave = new List<int>() };
            string f = "SaveCarriagesCount" + "train" + i;
            foreach (string s in gameObject.GetComponent<ShopDictionary>().carriageDic.Keys)
            {
                listIntCarriages.CarriagesYouHave.Add(0);
            }
            int count = GetComponent<ShopDictionary>().GetCountLocomotive();
            listIntCarriages.CarriagesYouHave[listIntCarriages.CarriagesYouHave.Count - count] = 1;///даем 1 стартовый локомотив
            SaveSystem.Set(f, listIntCarriages);
        }
    }
    private void CreateListImprovements()
    {
        for (int i = 1; i <= 3; i++)
        {
            SaveImprovement improvement = new SaveImprovement() { improvementLevel = new List<int>() };
            for (int y = 0; y < 5; y++)
            {
                improvement.improvementLevel.Add(0);
            }
            string f = "SaveImprovements" + "train" + i;
            SaveSystem.Set(f, improvement);
        }

    }
    public void SaveGameInfo(int score, List<int> stars)
    {
        SaveNeed test = new SaveNeed() { Score = score, StarsOnLevel = stars };
        SaveSystem.Set("SaveGame", test);
    }
    public void SaveListImprovements(List<int> list, string nameTrain)
    {
        SaveImprovement saveImprovement = new SaveImprovement() { improvementLevel = list };
        string s = "SaveImprovements" + nameTrain;
        SaveSystem.Set(s, saveImprovement);
    }
    public void LoadGameInfo()
    {
        var save = SaveSystem.Get<SaveNeed>("SaveGame");
        LevelController.Instance.LoadSaveInfo(save.Score, save.StarsOnLevel);
    }
    
    
    public List<int> LoadListImprovements(string nameTrain)
    {
        
        string s = "SaveImprovements" + nameTrain;
        var save = SaveSystem.Get<SaveImprovement>(s);
        return save.improvementLevel;
        
    }
    
    public void AddGhostToListCarriagesUsedOnLevel(string nameTrain)
    {
        string s = "SaveTrain" + nameTrain;
        var save = SaveSystem.Get<SaveNeedTrain>(s);
        
        save.CarriageUsedOnLevel.Add("ghostCarriage");

        SaveSystem.Set(s, save);
    }
}
