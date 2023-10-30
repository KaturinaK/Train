using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
   private Coroutine Coroutine;
    public static MoveController Instance
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
    static private MoveController _instance;
    
    
    public void ButtonGasMouseDown()
    {
        TrainController.Instance.PressGas();
    }
    public void ButtonGasMouseUp()
    {
        TrainController.Instance.PressGasUp();
    }
    public void ButtonBreakMouseDown()
    {
        TrainController.Instance.PressBreake();
    }
    public void ButtonBreakMouseUp()
    {
        TrainController.Instance.PressBreakeUp();
    }
    public void ButtonStopMouseDown()
    {
        TrainController.Instance.PressStop();
        Coroutine = StartCoroutine(VibrateOn());
        
    }
    public void ButtonStopMouseUp()
    {
        StopCoroutine(Coroutine);
        Debug.Log("stop");
    }
    IEnumerator VibrateOn()
    {
        for (; ; )
        {
            Handheld.Vibrate();
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
