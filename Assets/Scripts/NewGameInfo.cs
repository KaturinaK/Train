using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameInfo : MonoBehaviour
{
    [SerializeField] private GameObject PanelNewGameInfoLevel;
    [SerializeField] private GameObject PanelStartEducation;
    [SerializeField] private GameObject PanelEducationOnLevel1;
    [SerializeField] private GameObject PanelEducationOnLevel2;
    [SerializeField] private GameObject PanelEducationOnLevel3;
    [SerializeField] private GameObject PanelEducationOnLevel4;
    [SerializeField] private GameObject PanelEducationPersonalArea;
    [SerializeField] private GameObject PanelEducationShop1;
    [SerializeField] private GameObject PanelEducationShop2;
    [SerializeField] private GameObject PanelEducationShop3;
    [SerializeField] private GameObject PanelEducationShop4;
    [SerializeField] private GameObject PanelEducationShop5;
    public static NewGameInfo Instance
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
    static private NewGameInfo _instance;

    public void ShowPanelNewGameInfoLevel()
    {
        PanelNewGameInfoLevel.SetActive(true);
    }
    public void HidePanelNewGameInfoLevel()
    {
        PanelNewGameInfoLevel.SetActive(false);
    }
    public void ShowPanelStartEducation()
    {
        PanelStartEducation.SetActive(true);
    }
    public void HidePanelStartEducation()
    {
        PanelStartEducation.SetActive(false);
        PlayerPrefs.SetInt("NewGameInfo",  6);
        Debug.Log(PlayerPrefs.GetInt("NewGameInfo") + "NewGameInfo");
    }
   public void YesPanelStartEducation()
    {
        PanelStartEducation.SetActive(false);
        ShowPanelNewGameInfoLevel();

        PlayerPrefs.SetInt("NewGameInfo", PlayerPrefs.GetInt("NewGameInfo") + 1);
        Debug.Log(PlayerPrefs.GetInt("NewGameInfo") + "NewGameInfo");
    }
    public void ShowPanelEducationOnLevel1()
    {
        PanelEducationOnLevel1.SetActive(true);
    }
    public void HidePanelEducationOnLevel1()
    {
        PanelEducationOnLevel1.SetActive(false);
        ShowPanelEducationOnLevel2();
    }
    public void ShowPanelEducationOnLevel2()
    {
        PanelEducationOnLevel2.SetActive(true);
    }
    public void HidePanelEducationOnLevel2()
    {
        PanelEducationOnLevel2.SetActive(false);
        ShowPanelEducationOnLevel3();
    }
    public void ShowPanelEducationOnLevel3()
    {
        PanelEducationOnLevel3.SetActive(true);
    }
    public void HidePanelEducationOnLevel3()
    {
        PanelEducationOnLevel3.SetActive(false);

        PlayerPrefs.SetInt("NewGameInfo", PlayerPrefs.GetInt("NewGameInfo") + 1);
        Debug.Log(PlayerPrefs.GetInt("NewGameInfo") + "NewGameInfo");
    }
    public void ShowPanelEducationOnLevel4()
    {
        PanelEducationOnLevel4.SetActive(true);
    }
    public void HidePanelEducationOnLevel4()
    {
        PanelEducationOnLevel4.SetActive(false);

        PlayerPrefs.SetInt("NewGameInfo", PlayerPrefs.GetInt("NewGameInfo") + 1);
        Debug.Log(PlayerPrefs.GetInt("NewGameInfo") + "NewGameInfo");

        SceneManager.LoadScene("PersonalArea", LoadSceneMode.Single);
    }
    public void ShowPanelEducationPersonalArea()
    {
        PanelEducationPersonalArea.SetActive(true);
    }
    public void HidePanelEducationPersonalArea()
    {
        PanelEducationPersonalArea.SetActive(false);

        PlayerPrefs.SetInt("NewGameInfo", PlayerPrefs.GetInt("NewGameInfo") + 1);
        Debug.Log(PlayerPrefs.GetInt("NewGameInfo") + "NewGameInfo");

        SceneManager.LoadScene("Shop", LoadSceneMode.Single);
    }
    public void ShowPanelEducationShop1()
    {
        PanelEducationShop1.SetActive(true);
    }
    public void HidePanelEducationShop1()
    {
        PanelEducationShop1.SetActive(false);
        ShowPanelEducationShop2();
    }
    public void ShowPanelEducationShop2()
    {
        PanelEducationShop2.SetActive(true);
    }
    public void HidePanelEducationShop2()
    {
        PanelEducationShop2.SetActive(false);
        ShowPanelEducationShop3();
    }
    public void ShowPanelEducationShop3()
    {
        PanelEducationShop3.SetActive(true);
    }
    public void HidePanelEducationShop3()
    {
        PanelEducationShop3.SetActive(false);
        ShowPanelEducationShop4();
    }
    public void ShowPanelEducationShop4()
    {
        PanelEducationShop4.SetActive(true);
    }
    public void HidePanelEducationShop4()
    {
        PanelEducationShop4.SetActive(false);
        ShowPanelEducationShop5();
    }
    public void ShowPanelEducationShop5()
    {
        PanelEducationShop5.SetActive(true);
    }
    public void HidePanelEducationShop5()
    {
        PanelEducationShop5.SetActive(false);

        PlayerPrefs.SetInt("NewGameInfo", PlayerPrefs.GetInt("NewGameInfo") + 1);
        Debug.Log(PlayerPrefs.GetInt("NewGameInfo") + "NewGameInfo");
    }
}
