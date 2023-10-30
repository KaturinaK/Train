using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.ShaderData;

public class CarriagePassengerInfo : MonoBehaviour
{
    private CarriageInfo carriage;
    private int _passengerEnter = 0;
    private int _passengerExit = 0;
    private int _passengerInCarriage = 0;
    private int coin = 0;
    private int trainPrestigeLevel;
    private int stationNumber = 0;
    [SerializeField] Slider passengerInCarriageText;

    private void Start()
    {
        if(carriage.Capacity != 0)
        {
            passengerInCarriageText.maxValue = carriage.Capacity;
            passengerInCarriageText.value = _passengerInCarriage;
        }
        
    }
    public void GetInfoTrain(CarriageInfo vagon)
    {
        carriage = vagon;
    }
    public void PassengerTraffic()
    {
        if (carriage.Capacity != 0)
        {
            stationNumber = GameController.Instance.Station;
            trainPrestigeLevel = GetComponent<Carriage>().GetPrestige();
            CountPassengerExit();
            if (!GameController.Instance.CheckLastStation())
            {
                CountPassengerEnter();
            }

            passengerInCarriageText.value = _passengerInCarriage;
        }
    }
    private int RandomPassengerEnter()
    {
        return Mathf.RoundToInt(Random.Range((carriage.Capacity - _passengerInCarriage) * (0.3f + trainPrestigeLevel * 0.1f), (carriage.Capacity - _passengerInCarriage) * (0.6f + trainPrestigeLevel * 0.1f)));
    }
    private int RandomePassengerExit()
    {
        return Mathf.RoundToInt(Random.Range(_passengerInCarriage * 0.15f * stationNumber, _passengerInCarriage * 0.45f * stationNumber));
    }
    private void CountPassengerExit()
    {
        //exit
        if (GameController.Instance.CheckLastStation())
        {
            _passengerExit = _passengerInCarriage;
            _passengerInCarriage = 0;
        }
        else
        {
            _passengerExit = RandomePassengerExit();
            if (_passengerExit >= _passengerInCarriage)
            {
                _passengerExit -= _passengerInCarriage;
                _passengerInCarriage = 0;

            }
            else
            {
                _passengerInCarriage -= _passengerExit;
            }
        }
        CountCoin();
    }
    private void CountPassengerEnter()
    {

        //enter
        _passengerEnter = RandomPassengerEnter();
        
        if (_passengerEnter >= carriage.Capacity - _passengerInCarriage)
        {
            _passengerInCarriage = carriage.Capacity;
        }
        else
        {
            _passengerInCarriage += _passengerEnter;
        }
        Debug.Log("пассажиров должно сесть " + _passengerEnter);
    }
    private void CountCoin()
    {
        coin += _passengerExit * carriage.Fare;
        GameController.Instance.GetCoin(coin);
    }
    
}
