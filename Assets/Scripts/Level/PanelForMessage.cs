using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelForMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    public void ShowMassage(string message)
    {
        if(message == "wrongStart")
        {
            //messageText.text = "transfer of passengers not completed";
            messageText.text = "посадка пассажиров прервана";
        }
        if(message == "stop")
        {
            //messageText.text = "emergency brake";
            messageText.text = "экстренное торможение";
        }
        if(message == "droveBy")
        {
            //messageText.text = "drove past the station";
            messageText.text = "проехал мимо станции";
        }

    }
}
