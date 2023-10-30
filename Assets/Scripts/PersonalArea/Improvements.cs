using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ImprovementCharacteristic
{
    public int Level { get; set; }
    public float LevelMaxSpeed { get; set; }
    public float LevelAccelerationSpeed { get; set; }
    public float LevelBrakingSpeed { get; set; }
    public int LevelCarriageCount { get; set; }
    public int LevelPrestige { get; set; }
    public List<int> improvmentLevels { get; set; }
}

public class Improvements : MonoBehaviour
{
    
    public static Improvements Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        GetLevel();
        
    }
    static private Improvements _instance;
    public static Dictionary<string, ImprovementCharacteristic> Improvments()
    {
        Dictionary<string, ImprovementCharacteristic> improvementDict = new Dictionary<string, ImprovementCharacteristic>();
        improvementDict.Add( "train1", new ImprovementCharacteristic { Level = 0, improvmentLevels = new List<int>() });
        improvementDict.Add( "train2", new ImprovementCharacteristic { Level = 0, improvmentLevels = new List<int>() });
        improvementDict.Add( "train3", new ImprovementCharacteristic { Level = 0, improvmentLevels = new List<int>() });
        return improvementDict;
    }

    public Dictionary<string, ImprovementCharacteristic> improvementsInfo = Improvments();
    public ImprovementCharacteristic TakeLevel(string nameTrain)
    {
        GetLevel();
        return improvementsInfo[nameTrain];
    }
    private void GetLevel()
    {
        foreach (var s in improvementsInfo.Keys)
        {
           
            improvementsInfo[s].improvmentLevels = GetComponent<SaveGame>().LoadListImprovements(s);
        }
    }
    public void GetUpgrade(string key, int index)
    {
        improvementsInfo[key].improvmentLevels[index]++;
        PlayerPrefs.SetInt(PlayerPrefs.GetString("nameTrain"), PlayerPrefs.GetInt(PlayerPrefs.GetString("nameTrain")) + 1);// TrainLevel сохран€ю в PlayerPrefs

        SaveListImprovments(improvementsInfo[key].improvmentLevels);
    }
    private void SaveListImprovments(List<int> list)
    {
        gameObject.GetComponent<SaveGame>().SaveListImprovements(list, PlayerPrefs.GetString("nameTrain"));

    }
}
