using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotiveCharacteristics
{
    
    public float MaxSpeed { get; set; }
    public float MaxSpeedImprovmentForOneLevel { get; set; } //увеличение с уровнем
    public float AccelerationSpeed { get; set; }
    public float AccelerationSpeedImprovmentForOneLevel { get; set; }
    public float BrakingSpeed { get; set; }
    public float BrakingSpeedImprovmentForOneLevel { get; set; }
    public int CarriageCount { get; set; }
    public int CarriageCountImprovmentForOneLevel { get; set; }

}
public class LocomotiveInfo : MonoBehaviour
{
    public static LocomotiveInfo Instance
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
    static private LocomotiveInfo _instance;
    public static Dictionary<string, LocomotiveCharacteristics> Locomotives()
    {
        Dictionary<string, LocomotiveCharacteristics> locomotiveDic = new Dictionary<string, LocomotiveCharacteristics>();
        locomotiveDic.Add( "train1", new LocomotiveCharacteristics { MaxSpeed = 150, MaxSpeedImprovmentForOneLevel = 50, AccelerationSpeed = 0.5f, AccelerationSpeedImprovmentForOneLevel = 1, BrakingSpeed = 0.4f, BrakingSpeedImprovmentForOneLevel = 0.1f, CarriageCount = 1});
        locomotiveDic.Add( "train2", new LocomotiveCharacteristics {  MaxSpeed = 250, MaxSpeedImprovmentForOneLevel = 50, AccelerationSpeed = 1f, AccelerationSpeedImprovmentForOneLevel = 1f, BrakingSpeed = 0.5f, BrakingSpeedImprovmentForOneLevel = 0.1f, CarriageCount= 3});
        locomotiveDic.Add( "train3", new LocomotiveCharacteristics {  MaxSpeed = 350, MaxSpeedImprovmentForOneLevel = 50, AccelerationSpeed = 1.5f, AccelerationSpeedImprovmentForOneLevel = 1, BrakingSpeed = 1f, BrakingSpeedImprovmentForOneLevel= 0.1f, CarriageCount= 6 });
        return locomotiveDic;
    }

   
    public Dictionary<string, LocomotiveCharacteristics> locomotivesInfo = Locomotives();
    
    
    public int SetCarriageCount(string trainName)
    {
        return locomotivesInfo[trainName].CarriageCount;
    }


}
