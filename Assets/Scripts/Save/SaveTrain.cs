using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class SaveNeedTrain
    {
        public List<string> CarriageUsedOnLevel;
        public List<int> CarriagesYouHave;
    }
public class SaveTrain : MonoBehaviour
{
    
    
    public void SaveGameInfo(List<string> carriage, string nameTrain)
    {
        SaveNeedTrain infoSave = new SaveNeedTrain() { CarriageUsedOnLevel = carriage };
        string s = "SaveTrain" + nameTrain;
        SaveSystem.Set(s, infoSave);
    }
    public void SaveCarriagesCountInfo(List<int> countCarriage, string nameTrain)
    {
        SaveNeedTrain infoSave = new SaveNeedTrain() { CarriagesYouHave = countCarriage };
        string s = "SaveCarriagesCount" + nameTrain;
        SaveSystem.Set(s, infoSave);
    }
    public List<string> LoadGameInfo(string nameTrain)
    {
        string s = "SaveTrain" + nameTrain;
        var save = SaveSystem.Get<SaveNeedTrain>(s);
        return save.CarriageUsedOnLevel;

    }
    public List<int> LoadCarriagesCountInfo(string nameTrain)
    {
        string s = "SaveCarriagesCount" + nameTrain;
        var save = SaveSystem.Get<SaveNeedTrain>(s);
        return save.CarriagesYouHave;
    }
}
