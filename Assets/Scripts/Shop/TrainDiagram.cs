using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainDiagram : MonoBehaviour
{

    [SerializeField] private GameObject button;
    [SerializeField] private GameObject buttonBluePlus;
    [SerializeField] private GameObject buttonDestroy;
    [SerializeField] private GameObject Content;
    [SerializeField] private List<string> carriageUsedOnLevel;
    private string newName;

    void Start()
    {
        TakeSaveInfoTrain();
        CreateButtonDiagram();
        
    }
    private void CreateButtonDiagram()
    {
        for (int i = carriageUsedOnLevel.Count - 1; i >= 0; i--)
        {
            if (CheckCreatePlus(i))
            { 
                GameObject buttonCloneBluePlus = Instantiate(buttonBluePlus, Content.transform, false);
                buttonCloneBluePlus.GetComponent<BluePlus>().Info(i);//!!!!!!!!!!!!!!!
            }
            
            GameObject buttonClone = Instantiate(button, Content.transform, false);
            string s = ShopController.Instance.carriageDic[carriageUsedOnLevel[i]].Type;
            buttonClone.GetComponent<ButtonTrainDiagram>().LoadImage(carriageUsedOnLevel[i], i, s);

            
        }
    }
    
    public void TakeSaveInfoTrain()
    {
        carriageUsedOnLevel = GameObject.Find("ShopController").GetComponent<SaveTrain>().LoadGameInfo(PlayerPrefs.GetString("nameTrain"));
        
    }
    public void ButtonDestroy()
    {
        ShopController.Instance.DestroyDiagram();
        buttonBluePlus.GetComponent<Button>().interactable = true;//!!!!!!!!!!!!!!!
    }
    private bool CheckCreatePlus(int i)
    {
        if (carriageUsedOnLevel.Count > i + 1 && carriageUsedOnLevel.IndexOf("ghostCarriage") != -1)
        {
            if (carriageUsedOnLevel[i + 1] != "ghostCarriage")
                return true;
            else return false;
        }
        else return false;
    }
    public void BluePlus(int i)//!!!!!!!!!!!!!!!
    {
        
        TakeSaveInfoTrain();

        newName = ShopController.Instance.NameSlotForUse();
        if (ShopController.Instance.SlotWhichUseNow().GetComponent<ShopSlot>().ChangeCount()  && ShopController.Instance.carriageDic[newName].Type != "train" )
        {
            carriageUsedOnLevel.Insert(i + 1, newName);
            carriageUsedOnLevel.RemoveAt(carriageUsedOnLevel.Count - 1);

            ShopController.Instance.SaveListCarriageUsedOnLevel(carriageUsedOnLevel);

            ShopController.Instance.SlotWhichUseNow().GetComponent<ShopSlot>().ChangeUseCarriageCount(-1);///!!!

            ShopController.Instance.DestroyDiagram();
            ShopController.Instance.CreateTrainDiagram();


            
        }
        
        //Debug.Log(ShopController.Instance.carriageDic[newName].Type);
    }
    
}
