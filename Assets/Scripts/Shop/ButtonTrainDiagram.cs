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
    // ��� ������� ����� ����������, ����� ������ ������������
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
            OnHold.Invoke(); // ����� ������� ��� ���������
        }
    }

    // ���� ����� ����� ����������, ����� ������ ������
    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
    }

    // ���� ����� ����� ����������, ����� ������ ��������
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
