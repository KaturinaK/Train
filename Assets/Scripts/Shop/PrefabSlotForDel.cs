using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PrefabSlotForDel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
   
    [SerializeField] private GameObject buttonDel;
    [SerializeField] private TextMeshProUGUI sellaryPriceText;
    private int sellaryPrice = 100;
    private int index;
    private string _name;
    private Image image;
    private bool isHolding = false;
    // Это событие будет вызываться, когда кнопка удерживается
    public UnityEngine.Events.UnityEvent OnHold;
    
    private void Update()
    {
        if (isHolding)
        {
            OnHold.Invoke(); // Вызов события при удержании
        }
    }

    // Этот метод будет вызываться, когда кнопка нажата
    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
    }

    // Этот метод будет вызываться, когда кнопка отпущена
    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
    }
    public void LoadImage(string name, int ind, string type)
    {
        Component[] img = GetComponentsInChildren<Image>();
        for(int i = 0; i < img.Length; i++)
        {
            if (img[i].name == "ImageCarriage")
            {
                image = img[i].GetComponent<Image>();
            }
        }

        image.sprite = Resources.Load<Sprite>("Carriages/" + name);
        index = ind;
        _name = name;
        if(type == "train" || name == "ghostCarriage")
        {
            Destroy(buttonDel);
            Destroy(sellaryPriceText);
            
        }
        
        sellaryPriceText.text = sellaryPrice.ToString();

    }
    public void Click()
    {
        
        ShopController.Instance.ChangeMoney(sellaryPrice);
        GameObject.Find("PanelForTrainYouHave(Clone)").GetComponent<PanelTrainYouHave>().ShowWarningPanel(index);

    }
    public void RemoveVagon()
    {
        if (_name != "ghostCarriage" && gameObject.tag != "train")
            GameObject.Find("PanelForTrainYouHave(Clone)").GetComponent<PanelTrainYouHave>().ShowPanelWarningRemove(index, _name);
    }
}
