using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TrainController : MonoBehaviour
{
    private GameObject sing;
    private GameObject trainStop;
    private GameObject placeToStop;
    private bool isStop;
    [SerializeField] private GameObject[] carriageOnLevel;
    public Slider sliderSpeed;
    public TextMeshProUGUI textSpeed;

    private float realSpeed = 0;
    private float nowSpeed = 0;
    private float maxSpeed; // макс скорость
    private float acceler = 2.5f; // постепенное увеличение скорости, которую выводим на экран
    private float brake = 3.5f; // постепенное уменьшение скорости, которую выводим на экран

    private int forCountScore = 1;
    private int score = 0;

    private int penalty = 0;

    [SerializeField] private GameObject train;
    [SerializeField] private float trainWidth = 0;
    [SerializeField] private float checkPoint = 0;
    [SerializeField] private float trainPoint = 0;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    public static TrainController Instance
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
    static private TrainController _instance;
    //находит объект с именем vagon и  записывает в список carriageOnLevel
    private void Start()
    {
        carriageOnLevel = GameObject.FindGameObjectsWithTag("vagon");

    }
    
    public void GetTrainSpeedCharacteristic(float speed, float realAcceleration, int levAcc, float realBrakee, int levBrake )
    {
        maxSpeed = speed;
        acceler = acceler + levAcc * realAcceleration;///!!!!!!!!!!!!!!
        brake = brake + levBrake * realBrakee;

    }
    IEnumerator GasShowRealSpeed()
    {
        while (true)
        {
            if (realSpeed > maxSpeed)
            {
                StopCoroutine("GasShowRealSpeed");
            }
            else 
            ChangeRealSpeed(acceler, acceler);
            yield return new WaitForSeconds(0.1f);
            
        }
    }
    
    IEnumerator BreakShowRealSpeed()
    {
        while (true)
        {
            if (realSpeed < 0)
            {
                StopCoroutine("BreakShowRealSpeed");
            }
            else
            {
                ChangeRealSpeed(-brake, brake);
                CheckBreakToStop();
            }
            yield return new WaitForSeconds(0.1f);
            
        }
    }
    
    private void ChangeRealSpeed(float step, float brakOrAcc)
    {
        nowSpeed =Mathf.Lerp(realSpeed, realSpeed += step, brakOrAcc*Time.deltaTime);
    }
    private void CheckBreakToStop() 
    { 
        if(nowSpeed < 11)////!!!!!!!!!!!!!!!!!
        {
            
            foreach (GameObject go in carriageOnLevel)
            {
                go.GetComponent<Carriage>().IsStopTrue();
            }
            IsStopTrue();
            if (trainStop != null)
            {   CheckTrainPositionAndAddScore();
                CheckTrainPosition();
            }
            
        }
    }
    
    public void PressGas()
    {
        StartCoroutine("GasShowRealSpeed");

        foreach (GameObject go in carriageOnLevel)
        {
            go.GetComponent<Carriage>().PressGas();
        }
        GameController.Instance.StopSoundNotLoop();
        GameController.Instance.PlaySoundLoop(0);
        isStop = false;
                                                 //StartTimer
        if (!GameController.Instance.IsGameStart())
        {
            GameController.Instance.StartCoroutine("StartTimer");

        }
    }
    public void PressGasUp()
    {
        StopCoroutine("GasShowRealSpeed");

        foreach (GameObject go in carriageOnLevel)
        {
            go.GetComponent<Carriage>().PressGasUp();
        }
    }
    public void PressBreake()
    {
        foreach (GameObject go in carriageOnLevel)
        {
            go.GetComponent<Carriage>().PressBreake();
        }
        StartCoroutine("BreakShowRealSpeed");

        GameController.Instance.PlaySoundNotLoop(4);
        GameController.Instance.StopSound();
        
        }
    public void PressBreakeUp()
    {
        StopCoroutine("BreakShowRealSpeed");
        foreach (GameObject go in carriageOnLevel)
        {
            go.GetComponent<Carriage>().PressBreakeUp();
        }
    }
    public void PressStop()
    {
        Penalty();
        GameController.Instance.CreatePanelForMessage("stop");
        ChangeRealSpeed(-realSpeed, realSpeed);
        nowSpeed = 0;
        foreach (GameObject go in carriageOnLevel)
        {
            go.GetComponent<Carriage>().PressStop();
        }
        IsStopTrue();
        GameController.Instance.PlaySoundNotLoop(1);
        GameController.Instance.StopSound();
        
        if (trainStop != null)
        {
            CheckTrainPositionAndAddScore();
            CheckTrainPosition();
        }
        else Debug.Log("тут высаживать нельзя");
    }
    public void IsStopTrue()
    {
        isStop = true;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SingStation>())
        {
            sing = collision.gameObject;
            GameController.Instance.GetSemaphoreNow(sing.GetComponent<SingStation>().ChoiseSemaphore());  //получение ссылки на ближайший семафор
            
        }
        else if (collision.gameObject.GetComponent<TrainStop>())//нужен для подсчета очков
        {
            trainStop = collision.gameObject;
        }
        else if (collision.gameObject.GetComponent<PlaceToStop>())//нужен для подсчета очков
        {
            placeToStop = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TrainStop>() && !isStop)// проверка на высаживание
        {
            Debug.Log("проехал мимо");
            GameController.Instance.CreatePanelForMessage("droveBy");
            trainStop.GetComponent<TrainStop>().Destroy(); 
        }
        else if (collision.gameObject.GetComponent<Semaphore>())
        {
            GameController.Instance.DoorsInteractableButton(false);
        }
        else if (collision.gameObject.GetComponent<SingStation>())
        {
            GameController.Instance.CountStation();
            GameController.Instance.CheckSemaphore(true);//переключаем семафор крас
        }
        else if (collision.gameObject.GetComponent<EndGame>())
        {
            GameController.Instance.CreateEndGamePanel();
            GameObject.Find("Main Camera").GetComponent<MainCamera>().enabled = false;
        }
        else if (collision.gameObject.GetComponent<StopTrain>())
        {
            Debug.Log("пауза");
            GameController.Instance.EndStopTrain();
        }
    }
    private void CheckTrainPosition()
    {
        if (placeToStop != null)
        { placeToStop.GetComponent<PlaceToStop>().ChangePic();
            Debug.Log("placeToStop CheckTrainPosition");
        }

        GameController.Instance.DoorsInteractableButton(true);
        trainStop.GetComponent<TrainStop>().Destroy();
        
    }
    public void GetTrain(GameObject t)
    {
        train = t;
    }
    private void CheckTrainPositionAndAddScore()
    {
        trainWidth = train.GetComponent<RectTransform>().rect.width / 2;
        checkPoint = trainStop.transform.position.x;
        trainPoint = train.transform.position.x;

        trainPoint += trainWidth;
        for (int i = 1; i <= 5; i++)
        {
            if(trainPoint > (checkPoint - i * forCountScore) && trainPoint < (checkPoint + i * forCountScore))
            {
                //Debug.Log("можно высаживать");
                Score += (6 - i);
                break;
            }
        }
        //Debug.Log("очки " + Score);
        HUD.Instance.ShowTextScore(Score);
    }
    public void Penalty()
    {
        GameController.Instance.MinusCoin(penalty);
        penalty += 200;
    }
    public int PenaltyCount()
    {
        return penalty;
    }
    public bool CheckMoving()
    {
        return !isStop;
    }
}
    


