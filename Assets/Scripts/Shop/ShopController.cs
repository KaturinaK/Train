using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : ShopDictionary
{

    [SerializeField] private Transform content;
    [SerializeField] private Transform panelForDiagram;
    [SerializeField] private Transform panelBackDiagram;
    [SerializeField] private Transform panelPanelTrainYouHave;
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private TextMeshProUGUI textCarriagesAvailable;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject trainDiagram;
    [SerializeField] private GameObject prefabPanelTrainYouHave;
    [SerializeField] private List<string> carriageUsedOnLevel;
    [SerializeField] private List<int> carriagesCountYouHave;

    private string nameSlotForUse;
    private string tagSlotForUse;
    private GameObject diagram;
    private GameObject slotNow;
    private GameObject _panelTrainYouHave;
    private GameObject slot;

    //private string locomotiveName;
    public static ShopController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        //locomotiveName = PlayerPrefs.GetString("nameTrain");

        Coin = PlayerPrefs.GetInt("coin");
        textCoin.text = Coin.ToString();

        carriageUsedOnLevel = gameObject.GetComponent<SaveTrain>().LoadGameInfo(PlayerPrefs.GetString("nameTrain"));
        carriagesCountYouHave = gameObject.GetComponent<SaveTrain>().LoadCarriagesCountInfo(PlayerPrefs.GetString("nameTrain"));

        ShowCarriageAmount();
    }
    static private ShopController _instance;
    private void Start()
    {
        CreateSlot();

        if (PlayerPrefs.GetInt("NewGameInfo") == 5)
            NewGameInfo.Instance.ShowPanelEducationShop1();
    }
    private void CreateSlot()
    {
        carriagesCountYouHave = gameObject.GetComponent<SaveTrain>().LoadCarriagesCountInfo(PlayerPrefs.GetString("nameTrain"));
        int i = 0;
        foreach (string key in carriageDic.Keys)
        {
            slot = Instantiate(slotPrefab, content, false);

            slot.name = key;
            slot.GetComponent<ShopSlot>().FillInfo(carriageDic[key], i, carriagesCountYouHave[i]);
            i++;
        }
    }
    private void ReloadSlots()
    {
        GameObject contentShop = GameObject.Find("ContentShop");
        foreach(var slot in contentShop.GetComponentsInChildren<Transform>())
        {
            if(slot.name != "ContentShop")
            {
                Destroy(slot.gameObject);
            }
        }
        CreateSlot();
    }
    public void ShowCarriageAmount()
    {
        carriageUsedOnLevel = gameObject.GetComponent<SaveTrain>().LoadGameInfo(PlayerPrefs.GetString("nameTrain"));
        int trainLength = carriageUsedOnLevel.Count - 1;//get carriages you can have
        int trainOccupiedCarriage = trainLength;//get carriages you already have
        foreach (var s in carriageUsedOnLevel)
        {
            if(s == "ghostCarriage")
            {
                trainOccupiedCarriage--;
            }
        }
        textCarriagesAvailable.text = trainOccupiedCarriage.ToString() + "/" + trainLength.ToString();
    }
    public void ChangeCarriageCount(int index, int p)
    {
        carriagesCountYouHave = gameObject.GetComponent<SaveTrain>().LoadCarriagesCountInfo(PlayerPrefs.GetString("nameTrain"));
        carriagesCountYouHave[index]+= p;
        gameObject.GetComponent<SaveTrain>().SaveCarriagesCountInfo(carriagesCountYouHave, PlayerPrefs.GetString("nameTrain"));

    }
    public bool CheckCountBought(int i)
    {
        if (carriagesCountYouHave[i] > 0)
        {
            return true;
        }
        else return false;
    }
    public void AddcarriageUsedOnLevel()
    {
        carriageUsedOnLevel = gameObject.GetComponent<SaveTrain>().LoadGameInfo(PlayerPrefs.GetString("nameTrain"));
        carriageUsedOnLevel.Add("ghostCarriage");
        SaveListCarriageUsedOnLevel(carriageUsedOnLevel);
        ShowCarriageAmount();
    }
    public void SaveListCarriageUsedOnLevel(List<string> _carriageUsedOnLevel)
    {
        gameObject.GetComponent<SaveTrain>().SaveGameInfo(SortListCarriage(_carriageUsedOnLevel), PlayerPrefs.GetString("nameTrain"));
        for (int i = 1; i <= 3; i++)
        {
            string s = "SaveTrain" + "train" + i;
            List<string> list = GameObject.Find("ShopController").GetComponent<SaveTrain>().LoadGameInfo("train" + i);
            
        }
    }
    private List<string> SortListCarriage(List<string> _carriageUsedOnLevel)
    {
        List<string> list = new List<string>();
        list = carriageUsedOnLevel;
        int countGhost = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == "ghostCarriage")
            {
                countGhost++;
                list.RemoveAt(i);
            }

        }
        for (int i = 0; i < countGhost; i++)
        {
            list.Add("ghostCarriage");
        }
        return list;
    }
    public bool CheckMoney(int money)
    {
        if (Coin >= money)
        { return true; }
        else return false;
    }
    public void ChangeMoney(int money)
    {
        Coin += money;
        PlayerPrefs.SetInt("coin", Coin);
        textCoin.text = Coin.ToString();
    }


    public void GetCoin(int coin)
    {
        Coin = coin;
        textCoin.text = Coin.ToString();
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    public void CreateTrainDiagram()
    {
        panelBackDiagram.gameObject.SetActive(true);
        GameObject diagramClon = Instantiate(trainDiagram, panelForDiagram, false);
        diagram = diagramClon;
    }
    public void DestroyDiagram()
    {
        Destroy(diagram);
        panelBackDiagram.gameObject.SetActive(false);
    }
    public void TakeSlotForUse(string name, string tag, GameObject slot)
    {
        nameSlotForUse = name;
        tagSlotForUse = tag;
        slotNow = slot;
    }
   
    public string NameSlotForUse()
    {
        return nameSlotForUse;
    }
   
    public GameObject SlotWhichUseNow()
    {
        return slotNow;
    }
    public void ChangeListVagon(int index, string nameOld, string nowType)
    {
        if (nameSlotForUse != "" && nowType == tagSlotForUse && slotNow.GetComponent<ShopSlot>().ChangeCount())
        {
            if (nowType == "train")
            {
                PlayerPrefs.SetString("nameTrain", nameSlotForUse);
                ReloadSlots();
            }
            else
            {
                slotNow.GetComponent<ShopSlot>().ChangeUseCarriageCount(-1);
            }
            carriageUsedOnLevel = gameObject.GetComponent<SaveTrain>().LoadGameInfo(PlayerPrefs.GetString("nameTrain"));
            carriageUsedOnLevel[index] = nameSlotForUse;
            SaveListCarriageUsedOnLevel(carriageUsedOnLevel);

            if(!slotNow.GetComponent<ShopSlot>().ChangeCount())
                nameSlotForUse = "";
            if (CheckCanChangeCount(nameOld, nowType))

                GameObject.Find(nameOld).GetComponent<ShopSlot>().ChangeUseCarriageCount(1);
            ReloadTrainDiagram();
        }
    }
    public void RemoveVagon(int index, string nameOld)
    {
        GameObject.Find(nameOld).GetComponent<ShopSlot>().ChangeUseCarriageCount(1);

        carriageUsedOnLevel = gameObject.GetComponent<SaveTrain>().LoadGameInfo(PlayerPrefs.GetString("nameTrain"));
        carriageUsedOnLevel[index] = "ghostCarriage";
        SaveListCarriageUsedOnLevel(carriageUsedOnLevel);

    }
    public void ReloadTrainDiagram()
    {
        DestroyDiagram();
        CreateTrainDiagram();
    }
    public void ReloadPanelTrainYouHave()
    {
        DestroyPrefabPanelTrainYouHave();
        CreatePanelTrainYouHave();
    }
    private bool CheckCanChangeCount(string nameOld, string nowType)
    {
        int c = 0;
        if (nameOld == "ghostCarriage")
            c++;
        if(nowType == "train")
            c++;
        if(c > 0)
            return false;
        else return true;
    }
    public int UsedVagonCount(string nameUsedCarriage)
    {
        int p = 0;
        foreach(var s in carriageUsedOnLevel)
        {
            if(s == nameUsedCarriage)
            {
                p++;
            }
        }
        
        return p;
        
    }

    public void CreatePanelTrainYouHave()
    {
        _panelTrainYouHave = Instantiate(prefabPanelTrainYouHave, panelPanelTrainYouHave, false);
        
    }
    public void DestroyPrefabPanelTrainYouHave()
    {
        Destroy(_panelTrainYouHave);
    }
    public bool CanBuyCarriage()
    {
        int carriageAmount = gameObject.GetComponent<LocomotiveInfo>().SetCarriageCount(PlayerPrefs.GetString("nameTrain"));//получила из словаря сколько может быть вагонов
                                                                                                    
        int carriageAmountYouHave = carriagesCountYouHave[0];//ghost
       

        if(carriageAmountYouHave < carriageAmount)
        {
            return true;
        }
        else return false;
    }
}
