using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTrainDiagram : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image image;
    private int index;
    private string _name;
    // Это событие будет вызываться, когда кнопка удерживается
    public UnityEngine.Events.UnityEvent OnHold;
    private bool isHolding = false;
    void Awake()
    {
        
        image = GetComponentInChildren<Image>();
    }
    
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
        image.sprite = Resources.Load<Sprite>("Carriages/" + name);
        index = ind;
        _name = name;
        gameObject.tag = type;
    }
    public void PutVagon()
    {
        
        ShopController.Instance.ChangeListVagon(index, _name, gameObject.tag);
        ShopController.Instance.ShowCarriageAmount();
    }
    public void RemoveVagon()
    {
        if(_name != "ghostCarriage" && gameObject.tag != "train")
            GameObject.Find("TrainDiagram(Clone)").GetComponent<TrainDiagram>().ShowPanelWarning(index, _name);
    }
}
