using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGas : MonoBehaviour
{
    //private bool b = false;
   
    
    public void ButtonMouseDown()
    {
        //Debug.Log("����");
       // b = true;

        TrainController.Instance.PressGas();
    }
    public void ButtonMouseUp()
    {
        //Debug.Log("�� ����");
       // b = false;

        TrainController.Instance.PressGasUp();
    }
}
