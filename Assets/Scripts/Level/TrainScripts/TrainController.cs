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
    private bool oneCheckStop;
    private int forCountScore = 1;
    private int score = 0;
    private int currentPenalty = 0;
    private int penalty = 200;

    [SerializeField] private GameObject[] carriageOnLevel;
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
    
    
    public void StartCheckPosition()
    {
        if (oneCheckStop)
        {
            CheckBreakToStop();
            oneCheckStop = false;
        }
    }
    public void CheckBreakToStop() 
    {
        IsStopTrue();
        
        if (trainStop != null)
        {
            CheckTrainPosition();
        }

    }
    
    public void PressGas()
    {
        oneCheckStop= true;

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

        GameController.Instance.PlaySoundNotLoop(4);
        GameController.Instance.StopSound();
        
        }
    public void PressBreakeUp()
    {
        foreach (GameObject go in carriageOnLevel)
        {
            go.GetComponent<Carriage>().PressBreakeUp();
        }
    }
    public void PressStop()
    {
        Penalty();
        GameController.Instance.CreatePanelForMessage("stop");
        foreach (GameObject go in carriageOnLevel)
        {
            go.GetComponent<Carriage>().PressStop();
        }
        
        GameController.Instance.PlaySoundNotLoop(1);
        GameController.Instance.StopSound();

        CheckBreakToStop();
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
        }

        GameController.Instance.DoorsInteractableButton(true);
        trainStop.GetComponent<TrainStop>().Destroy();

        CheckTrainPositionAndAddScore(); 
         
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
                Score += (6 - i);
                break;
            }
        }
        HUD.Instance.ShowTextScore(Score);
    }
    public void Penalty()
    {
        GameController.Instance.MinusCoin(penalty);
        currentPenalty += penalty;
    }
    public int PenaltyCount()
    {
        return currentPenalty;
    }
    public bool CheckMoving()
    {
        return !isStop;
    }
}
    


