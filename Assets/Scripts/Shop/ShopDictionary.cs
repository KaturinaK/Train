using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageInfo
{
    public string Name { get; set; }
    public string Category { get; set; }
    public int Cost { get; set; }
    public string Type { get; set; }
    public string Discription { get; set; }
    public int Capacity { get; set; }
    public int Fare { get; set; }
}

public class ShopDictionary : MonoBehaviour
{
    public static Dictionary<string, CarriageInfo> Carriages()
    {
        Dictionary<string, CarriageInfo> carriagesDic = new Dictionary<string, CarriageInfo>();
        carriagesDic.Add("ghostCarriage", new CarriageInfo { Name = "", Category = "", Cost = 800, Type = "vagon", Discription = "отличное место под ваш вагон" });
        carriagesDic.Add("carriage1", new CarriageInfo { Name = "Эконом", Category = "econom", Cost = 200, Type = "vagon", Discription = "вмещает 40 человек, стоимость проезда 5 монет", Capacity = 40, Fare = 5 });
        carriagesDic.Add("carriage2", new CarriageInfo { Name = "Стандарт", Category = "standart", Cost =600, Type = "vagon", Discription = "вмещает 25 человек, стоимость проезда 10 монет", Capacity = 30, Fare = 10 });
        carriagesDic.Add("carriage3", new CarriageInfo { Name = "Люкс", Category = "luxe", Cost =800, Type = "vagon", Discription = "вмещает 15 человек, стоимость проезда 30 монет", Capacity = 15, Fare = 30 });
        carriagesDic.Add("carriage4", new CarriageInfo { Name = "Ресторан", Category = "restorant", Cost =1200, Type = "vagon", Discription = "увеличивает стоимость проезда на 15%", Capacity = 0 });

        carriagesDic.Add("train1", new CarriageInfo { Name = "Первый", Category = "econom", Cost =1000, Type = "train" });
        carriagesDic.Add("train2", new CarriageInfo { Name = "Второй", Category = "standart", Cost =4000, Type = "train" });
        carriagesDic.Add("train3", new CarriageInfo { Name = "Третий", Category = "luxe", Cost =10000, Type = "train" });
        return carriagesDic;
    }
    public int Coin { get; set; }
    
    public Dictionary<string, CarriageInfo> carriageDic = Carriages();

    public int GetCountLocomotive()
    {
        int i = 0;
        foreach(CarriageInfo ci in carriageDic.Values)
        {
            if(ci.Type == "train")
            {
                i++;
            }
        }
        return i;
    }
    public string TrainName(string key)
    {
        return carriageDic[key].Name;

    }
    public List<string> LocomotivesNameList()
    {
        List<string> locomotivesNameList = new List<string>();
        foreach (CarriageInfo ci in carriageDic.Values)
        {
            if (ci.Type == "train")
                locomotivesNameList.Add(ci.Name);
        }
        return locomotivesNameList;
    }
}

