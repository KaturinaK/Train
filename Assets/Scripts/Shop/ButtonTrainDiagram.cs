using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrainDiagram : MonoBehaviour
{
    private Image image;
    private int index;
    private string _name;
    void Awake()
    {
        
        image = GetComponentInChildren<Image>();
    }
    public void LoadImage(string name, int ind, string type)
    {
        image.sprite = Resources.Load<Sprite>("Carriages/" + name);
        index = ind;
        _name = name;
        gameObject.tag = type;
    }
    public void Click()
    {

        ShopController.Instance.ChangeListVagon(index, _name, gameObject.tag);
        //Debug.Log(gameObject.tag + " " +_name);
        ShopController.Instance.ShowCarriageAmount();
    }
}
