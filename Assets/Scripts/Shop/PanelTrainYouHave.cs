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
    
}
