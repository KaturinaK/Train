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
    
    
    public void SaveGameInfo(List<string> carriage, string nameTrain)//может сюда добавить nameTrain? чтобы запоминать разные списки для разных локомотивов
    {
        SaveNeedTrain infoSave = new SaveNeedTrain() { CarriageUsedOnLevel = carriage };
        string s = "SaveTrain" + nameTrain;
        SaveSystem.Set(s, infoSave);
        //Debug.Log(s);
    }
    public List<string> LoadGameInfo(string nameTrain)//!!!!!!!!!!!
    {
        string s = "SaveTrain" + nameTrain;
        var save = SaveSystem.Get<SaveNeedTrain>(s);
        //Debug.Log(s);
        return save.CarriageUsedOnLevel;

    }
    public void SaveCarriagesCountInfo(List<int> countCarriage, string nameTrain)
    {
        SaveNeedTrain infoSave = new SaveNeedTrain() { CarriagesYouHave = countCarriage };
        string s = "SaveCarriagesCount" + nameTrain;
        SaveSystem.Set(s, infoSave);
        //SaveSystem.Set("SaveCarriagesCount", infoSave);
    }
    public List<int> LoadCarriagesCountInfo(string nameTrain)
    {
        string s = "SaveCarriagesCount" + nameTrain;
        //var save = SaveSystem.Get<SaveNeedTrain>("SaveCarriagesCount");
        var save = SaveSystem.Get<SaveNeedTrain>(s);
        return save.CarriagesYouHave;
    }
}
