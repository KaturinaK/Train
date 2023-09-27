using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrefabSlotForDel : MonoBehaviour
{
    private Image image;
    private int index;
    private string _name;
    [SerializeField] private GameObject buttonDel;
    [SerializeField] private TextMeshProUGUI sellaryPriceText;
    private int sellaryPrice = 100;
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

        //image = GetComponentInChildren<Image>();
        image.sprite = Resources.Load<Sprite>("Carriages/" + name);
        index = ind;
        _name = name;
        if(type == "train" || name == "ghostCarriage")
        {
            Destroy(buttonDel);
            Destroy(sellaryPriceText);
            
        }
        
        //узнать стоимость удаления
        sellaryPriceText.text = sellaryPrice.ToString();

    }
    public void Click()
    {
        
        ShopController.Instance.ChangeMoney(sellaryPrice);
        GameObject.Find("PanelForTrainYouHave(Clone)").GetComponent<PanelTrainYouHave>().DelCarriage(index);

    }

}
