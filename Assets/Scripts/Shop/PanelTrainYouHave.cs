using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTrainYouHave : MonoBehaviour
{
    [SerializeField] private Button destroy;
    [SerializeField] private List<string> carriageUsedOnLevel;
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject prefabSlot;
    [SerializeField] private GameObject panelWarning;
    [SerializeField] private GameObject panelWarningRemove;
    private int _indexOfSlot;
    private int _indexOfChoosenSlot;
    private string _nameOfChoosenSlot;
    
    private void Start()
    {
        CreateSlot();
    }
    public void DestroyPanel()
    {
        ShopController.Instance.DestroyPrefabPanelTrainYouHave();
    }

    private void CreateSlot()
    {
        TakeListCarriageUsedOnLevel();
        for (int i = carriageUsedOnLevel.Count - 1; i >= 0; i--)
        {
            GameObject clon = Instantiate(prefabSlot, Content.transform, false);
            string s = ShopController.Instance.carriageDic[carriageUsedOnLevel[i]].Type;
            clon.GetComponent<PrefabSlotForDel>().LoadImage(carriageUsedOnLevel[i], i, s);
        }
    }
    private void TakeListCarriageUsedOnLevel()
    {
        carriageUsedOnLevel = GameObject.Find("ShopController").GetComponent<SaveTrain>().LoadGameInfo(PlayerPrefs.GetString("nameTrain"));
    }
    private void SaveListCarriageUsedOnLevel()
    {
        ShopController.Instance.SaveListCarriageUsedOnLevel(carriageUsedOnLevel);
    }
    public void ShowWarningPanel(int index)
    {
        panelWarning.SetActive(true);
        _indexOfSlot = index;
    }
    public void DelVagon()
    {
        DelCarriage(_indexOfSlot);
        panelWarning.SetActive(false);
    }
    public void ExitWarningPanel()
    {
        panelWarning.SetActive(false);
    }
    public void DelCarriage(int index)
    {
        TakeListCarriageUsedOnLevel();

        carriageUsedOnLevel[index] = "ghostCarriage";
        
        ShopController.Instance.ShowCarriageAmount();

        SaveListCarriageUsedOnLevel();
        DelSlotForDel();

    }
    private void DelSlotForDel()
    {
        foreach (var child in Content.GetComponentsInChildren<Transform>())
        {
            if(child.gameObject.name != "Content")
            Destroy(child.gameObject);
        }
        CreateSlot();

    }
    public void ShowPanelWarningRemove(int ind, string nameOld)
    {
        panelWarningRemove.SetActive(true);
        _indexOfChoosenSlot = ind;
        _nameOfChoosenSlot = nameOld;
    }
    public void ExitPanelWarningRemove()
    {
        panelWarningRemove.SetActive(false);
    }
    public void RemoveVagon()
    {
        ShopController.Instance.RemoveVagon(_indexOfChoosenSlot, _nameOfChoosenSlot);
        ShopController.Instance.ShowCarriageAmount();
        ShopController.Instance.ReloadPanelTrainYouHave();
        panelWarningRemove.SetActive(false);
    }
}
