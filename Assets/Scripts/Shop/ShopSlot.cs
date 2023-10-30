using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    private CarriageInfo vagon = new CarriageInfo();

    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _discriptionText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _carriageCount;
    [SerializeField] private Image _carriage;
    [SerializeField] private Image _coinImage;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject useButton;
    [SerializeField] private List<string> locomotivesNameList;
    private int index;
    private int _count;
    private int _carriageCost;
    public void FillInfo(CarriageInfo carriage, int i, int count)
    {
        vagon = carriage;
        
        index = i;
        _count = count;
        if(vagon.Category == "" && useButton != null)
        {
            Destroy(useButton);

        }
        ShowCarriageCount(_count);
        if(useButton != null)
            CheckCanUse();

        locomotivesNameList = ShopController.Instance.LocomotivesNameList();
        
        ShowInfo();
    }
    public void ShowInfo()
    {
        CheckCanBuy();
        _nameText.text = vagon.Name;
        _coinText.text = CarriageCost(gameObject.name).ToString();
        _discriptionText.text = vagon.Discription;
        _carriage.sprite = Resources.Load<Sprite>("Carriages/" + gameObject.name);
        
        if(vagon.Type == "train" && _count >0)
            LimitBuyLocomotiveAndRestVagon();
        if (vagon.Name == "Ресторан" && _count > 0)
            LimitBuyLocomotiveAndRestVagon();
        
    }
    
    private void ShowCarriageCount(int count)
    {
        _carriageCount.text = count.ToString();
    }
    
    private void CheckCanBuy()
    {
        if (!ShopController.Instance.CheckMoney(CarriageCost(gameObject.name)) || CheckCanBuyGhost())
        {
            buyButton.GetComponent<Button>().interactable = false;
        }
        if(vagon.Type == "train" && ShopController.Instance.CheckMoney(CarriageCost(gameObject.name)))
        {
            buyButton.GetComponent<Button>().interactable = CheckCanBuyLocomotive();
        }
    }
    private bool CheckCanBuyLocomotive()
    {
        // эта проверка должна быть  только для локомотивов
        if (locomotivesNameList.IndexOf(vagon.Name) + 1 <= PlayerPrefs.GetInt("PageAccess"))
            return true;
        else return false;
    }
    private bool CheckCanBuyGhost()
    {
        if (!ShopController.Instance.CanBuyCarriage() && vagon.Category == "")
        {
            return true;
        }
        else return false;
    }
    private void CheckCanUse()
    {
        useButton.GetComponent<Button>().interactable = _count > 0;
    }
    private void BuySlot()
    {
        
        if (ShopController.Instance.CheckMoney(CarriageCost(gameObject.name)))
        {
            ShopController.Instance.ChangeMoney(-CarriageCost(gameObject.name));
            
            ChangeUseCarriageCount(1);
            if(vagon.Category == "")// for ghostCarriage
            {
                ShopController.Instance.AddcarriageUsedOnLevel();
            }
        }
        ShowInfo();
    }
    
    public void UseSlot()
    {
        
        ShopController.Instance.CreateTrainDiagram();
        string s = ShopController.Instance.carriageDic[gameObject.name].Type;
        ShopController.Instance.TakeSlotForUse(gameObject.name, s, gameObject);

    }

    public void ChangeUseCarriageCount(int p)
    {
        
            _count += p;
            ShopController.Instance.ChangeCarriageCount(index, p);
            ShowCarriageCount(_count);
            if (useButton != null)
                CheckCanUse();
        
    }
    public bool ChangeCount()
    {
        return ShopController.Instance.CheckCountBought(index);
    }
    public int CarriageCost(string nameUsedCarriage)
    {
        
        int p = _count + ShopController.Instance.UsedVagonCount(nameUsedCarriage)+1;
        _carriageCost = vagon.Cost * p;
        return _carriageCost;
    }
    private void LimitBuyLocomotiveAndRestVagon()
    {
        buyButton.GetComponent<Button>().interactable = false;
        _coinText.text = "-";
    }

}
