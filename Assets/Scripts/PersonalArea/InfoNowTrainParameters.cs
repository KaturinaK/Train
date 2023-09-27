using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InfoNowTrainParameters : MonoBehaviour
{
    private List<int> levelsList;
    private LocomotiveCharacteristics locomotiveCharacteristics;
    public static InfoNowTrainParameters Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        TakeLevelsList();
        TakeLocomInfo();
    }
    static private InfoNowTrainParameters _instance;

    private void TakeLocomInfo()
    {
        locomotiveCharacteristics = GetComponent<LocomotiveInfo>().locomotivesInfo[PlayerPrefs.GetString("nameTrain")];
    }
    public void TakeLevelsList()
    {
        levelsList = Improvements.Instance.TakeLevel(PlayerPrefs.GetString("nameTrain")).improvmentLevels;
    }
    public string InfoNowTrainParam(string nameSlot)
    {
        switch (nameSlot)
        {
            case "0":
                return ForCase0();
            case "1":
                return ForCase1();
            case "2":
                    return ForCase2();
            case "3":
                    return ForCase3();
            case "4":
                    return ForCase4();
            default:
                return "";
        }
    }
    private string ForCase0()//престиж
    {

        string prest = levelsList[0].ToString();
        return prest;
    }
    private string ForCase1()//скорость
    {
        string speed = (locomotiveCharacteristics.MaxSpeed + locomotiveCharacteristics.MaxSpeedImprovmentForOneLevel * levelsList[1]).ToString();
        
        return speed;
    }
    private string ForCase2()//ускорение
    {
        string acceler = (locomotiveCharacteristics.AccelerationSpeed + locomotiveCharacteristics.AccelerationSpeedImprovmentForOneLevel * levelsList[2]).ToString();
        return acceler;
    }
    private string ForCase3()//торможение
    {
        string brake = (locomotiveCharacteristics.BrakingSpeed + locomotiveCharacteristics.BrakingSpeedImprovmentForOneLevel * levelsList[3]).ToString();
        return brake;
    }
    private string ForCase4()//место для вагона
    {
        string vagons = (locomotiveCharacteristics.CarriageCount + levelsList[4]).ToString();
        return vagons;
    }
}
