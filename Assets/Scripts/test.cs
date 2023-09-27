using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class test : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _test;

    public static test Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

    }
    static private test _instance;

    public void Test(int coin, int score, int penalty, int time)
    {
        _test.text = coin + "coin " + score + "score " + penalty + "penalty " + time + " time";
    }
    public void PressButton()
    {
    }
    
    

    /*public void Count()
    {
        int page = 4;
        
        int i1 = (page - 1) * 10 + 1;
        Debug.Log("i1 " + i1);
        int i2 = page * 10;
        Debug.Log("i2 " + i2);
        for (int i = i1; i <= i2; i++)
        {
            
            //Debug.Log("DifficultLevel " + DifficultLevel(i));
            Debug.Log("i " + i);
            Plus();
            //buttonClone.GetComponent<ButtonLevel>().FillInfo(i, DifficultLevel(i), starInLevels[i - 1]);
            //Debug.Log(DifficultLevel(i));
        }
    }*/
       

}
