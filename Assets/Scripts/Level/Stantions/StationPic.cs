using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationPic : MonoBehaviour
{
     [SerializeField] private GameObject picStation;
     [SerializeField] private GameObject picStop;
     [SerializeField] private GameObject picTrainStation;
    public void StationImageManipulations()
    {
        AddPicStation();
        AddPicStop();
        SnowPicTrainStation();
    }
    private void AddPicStation()
    {
        int picNumber = Random.Range(1, 12);

        picStation.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Station" + PlayerPrefs.GetInt("Page") + "/station" + picNumber);
        
    }
    private void AddPicStop()
    {
        picStop.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("stop" + PlayerPrefs.GetInt("Page"));
    }
    private void SnowPicTrainStation()
    {
        picTrainStation.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("SingStation" + PlayerPrefs.GetInt("Page"));
    }
}
