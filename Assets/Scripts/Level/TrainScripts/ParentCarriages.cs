using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentCarriages : MonoBehaviour
{
    private LocomotiveCharacteristics _locomotive;
    private ImprovementCharacteristic _locomotiveIprovment;

    public static ParentCarriages Instance
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
    static private ParentCarriages _instance;
    public void GetInfoLocomotive(string key)
    {

        _locomotive = LocomotiveInfo.Instance.locomotivesInfo[key];

    }
    public LocomotiveCharacteristics LocomotiveInstance()
    {
        return _locomotive;
    }
    
    public void GetInfoLocomotiveImprovment(string key)
    {

        _locomotiveIprovment = Improvements.Instance.TakeLevel(key);
    }
    public ImprovementCharacteristic LocomotiveImprovment()
    {
        return _locomotiveIprovment;
    }

}
