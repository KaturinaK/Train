using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    
    private List<int> starInLevels = new List<int>();
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject buttonLeft;
    [SerializeField] private GameObject buttonRight;
    [SerializeField] private TextMeshProUGUI _starsText;
    [SerializeField] private Image picBackground;
    private int _score;
    private int starsForOpen10Level = 5;/// тут будет не 1 а 25
    private int maxPages = 4;
    public static LevelController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        gameObject.GetComponent<SaveGame>().LoadGameInfo();

       
    }
    static private LevelController _instance;
    private void Start()
    {
        LoadPicBackground();

        CreateButtonLevel();

        ShowStars();

        CheckInterectableButtonLeftRight();

        if (PlayerPrefs.GetInt("NewGameInfo") == 1)
            NewGameInfo.Instance.ShowPanelStartEducation();
    }
    private void ShowStars()
    {
        _starsText.text = GetCountStars() + " / " + StarsForOpen10Level();
    }

    private void CheckInterectableButtonLeftRight()
    {
        if(PlayerPrefs.GetInt("Page") < PlayerPrefs.GetInt("PageAccess"))
        {
            if(PlayerPrefs.GetInt("Page") < maxPages)
            buttonRight.GetComponent<Button>().interactable = true;
        }
        else buttonRight.GetComponent<Button>().interactable = false;

        if(PlayerPrefs.GetInt("Page") > 1)
        {
            buttonLeft.GetComponent<Button>().interactable = true;
        }
        else buttonLeft.GetComponent<Button>().interactable = false;
    }
    public void ClickRightButton()
    {
        PlayerPrefs.SetInt("Page", PlayerPrefs.GetInt("Page") + 1);
        CheckInterectableButtonLeftRight();
        LoadPicBackground();
        DelButtonLevel();

        ShowStars();
    }
    public void ClickLeftButton()
    {
        PlayerPrefs.SetInt("Page", PlayerPrefs.GetInt("Page") - 1);
        CheckInterectableButtonLeftRight();
        LoadPicBackground();
        DelButtonLevel();

        ShowStars();
    }
    public void LoadPicBackground()
    {
        picBackground.sprite = Resources.Load<Sprite>("picForPage" + PlayerPrefs.GetInt("Page"));
    }
    public void LoadSaveInfo(int score, List<int> stars)
    {
        _score = score;
        starInLevels = stars;
    }
    public void CreateButtonLevel()
    {
        int i1 = (PlayerPrefs.GetInt("Page") - 1) * 10 + 1;
        int i2 = PlayerPrefs.GetInt("Page") * 10;
        for (int i = i1; i <= i2; i++)
        {
            GameObject buttonClone = Instantiate(button, Content.transform, false);
            buttonClone.GetComponent<ButtonLevel>().FillInfo(i, DifficultLevel(i), starInLevels[i - 1]);
        }
        
    }
    
    private int DifficultLevel(int number)
    {
       return  number - (10 * (PlayerPrefs.GetInt("Page") - 1));
        
    }
    
    public void TakeInfoLevel(int star, int numberLev)//тут получаю значение звезд, номер уровня//функция вызывается после уровня
    {
        
        if(starInLevels[numberLev - 1] <= star)
            starInLevels[numberLev - 1] = star;
        gameObject.GetComponent<SaveGame>().SaveGameInfo(_score, starInLevels);
        ShowStars();
        // проверка если уровень был /10
        if (numberLev % 10 != 0)
            Count10Button();
        DelButtonLevel();
        
    }
    public void DelButtonLevel()
    {
        foreach (Transform child in Content.transform)
        {
            Destroy(child.gameObject);
        }
        CreateButtonLevel();
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    private int GetCountStars()
    {
        int _star = 0;
        for (int i = 0; i < starInLevels.Count; i++)
        {
            _star += starInLevels[i];
        }
        return _star;
    }
    private void Count10Button()
    {
        if(GetCountStars() % StarsForOpen10Level() == 0 && GetCountStars() != 0 )
        {
            PlayerPrefs.SetInt("10button", PlayerPrefs.GetInt("10button") + 1);
        }
    }
    private int StarsForOpen10Level()
    {
        return starsForOpen10Level * PlayerPrefs.GetInt("Page");
    }
}
