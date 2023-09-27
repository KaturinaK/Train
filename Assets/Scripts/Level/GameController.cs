using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int score = 0;
    private int coin = 0;
    public GameObject humanPanel;
    public GameObject endGamePanel;
    public GameObject panelForMessage;
    public Transform canvas;
    private int station = 0;
    private int maxStation = 0;
    private int time = 0;
    private bool isGameStart=false;

    
    private int maxScore = 0;
    private int playerScore = 0;

    private int levelDifficult;
    public AudioSource musicLoop;
    public AudioSource musicNotLoop;
    public AudioClip[] clip;
    private GameObject semaphoreNow;
    [SerializeField] private GameObject stationPrefab;
    [SerializeField] private GameObject endSpotsPrefab;
    private GameObject st;
    private int _levelNumber;

    [SerializeField] private Image openDoors;
    [SerializeField] private Button openDoor;
    private float lerpSpeed = 0.1f;
    private int minValue = 0;
    private int maxValue = 1;

    [SerializeField] private GameObject[] carriageOnLevel;

    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private GameObject buttonRight;
    [SerializeField] private GameObject buttonLeft;

    [SerializeField] private GameObject panelPause;

    public int Coin
    {
        get { return coin; }
        set { coin = value; }
    }
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    public int Station
    {
        get { return station; }
        set { station = value; }
    }

    public static GameController Instance
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
    static private GameController _instance;

    
    public void PlaySoundLoop(int i)
    {
        
        musicLoop.clip = clip[i];
        musicLoop.Play();
    }   
    public void StopSound()
    {
        musicLoop.Stop();
    }
    public void StopSoundNotLoop()
    {
        musicNotLoop.Stop();
    }
    public void PlaySoundNotLoop(int i)
    {
        musicNotLoop.clip = clip[i];
        musicNotLoop.Play();
    }

    IEnumerator StartTimer()
    {
        isGameStart = true;
        for (; ; )
        {
            Timer();
            yield return new WaitForSeconds(1);
        }
        
    }
    private void Timer()
    {
        time++;
        timeText.text = "Time " + time.ToString();
    }
    public bool IsGameStart()
    {
        return isGameStart;
    }
   
    public void GetCoin(int _coin)
    {
        Coin +=_coin;
        HUD.Instance.ShowTextCoin(Coin);
    }
    public void MinusCoin(int penalty)
    {
        Coin -= penalty;
        HUD.Instance.ShowTextCoin(Coin);
    }
   
    public void CreateEndGamePanel()
    {
       // Debug.Log(maxScore + "CreateEndGamePanel");
        Score = TrainController.Instance.Score;
        GameObject endPanel = Instantiate(endGamePanel, canvas, false);
        endPanel.name = "EndLevelInfo";
        StopAllCoroutines();

        playerScore = Score;
        int penalty = TrainController.Instance.PenaltyCount();
        endPanel.GetComponent<FinalLevelPanel>().FillInfo(CountStar(), playerScore + "/" + maxScore, Coin, playerScore, penalty, time, _levelNumber, CountTime(levelDifficult));///////////

        
    }
    public void SetInfoFinLevel()
    {
        InfoDontDestroy.Instance.TakeInfoLevel(CountStar());///вот тут передаю в Донт звезды
    }
    public void ReloadScene()
    {
        InfoDontDestroy.Instance.TakeInfo(levelDifficult, _levelNumber);
    }
    public void CountStation()
    {
        station++;
    }
    public bool CheckLastStation()///должны все выйти
    {
        if (station == maxStation)
        {
            return true;
        }
        else
        {
            return false;   
        }
    } 
    public int CountStar()//переписать
    {
        int star = 3;
        if (Score < maxScore * 0.9f)
        {
            star--;
        }
        if (Score < maxScore * 0.8f)
        {
            star--;
        }
        if (time > CountTime(levelDifficult))//CountTime
        {
            star--;
        }
        return star;
    }
    private int CountTime(int diff)//расчет времени для разного уровня сложности 
    {
        
        if(diff == 1 || diff == 5 || diff == 9 || diff == 13)
            return 150 + (-10 * (PlayerPrefs.GetInt("Page") - 1));
        if(diff == 2 || diff == 6 || diff == 10 || diff == 14)
            return 175 + (-10 * (PlayerPrefs.GetInt("Page") - 1));
        if(diff == 3 || diff == 7 || diff == 11 || diff == 15)
            return 240 + (-10 * (PlayerPrefs.GetInt("Page") - 1));
        if(diff == 4 || diff == 8 || diff == 12 || diff == 16)
            return 270 + (-10 * (PlayerPrefs.GetInt("Page") - 1));
        else return 0;
    }
    public void EndStopTrain()
    {
        
        Time.timeScale = 0f;
    }
    public void CheckSemaphore(bool b)
    {
        if (semaphoreNow != null)
            semaphoreNow.GetComponent<Semaphore>().SwitchSemaphoreLights(b);
    }
    public void GetSemaphoreNow(GameObject semaphor)
    {
        semaphoreNow = semaphor;
    }
    public void PauseOn()
    {
        Time.timeScale = 0f;
        panelPause.SetActive(true);
    }
    public void PauseOff()
    {
        Time.timeScale = 1f;
        panelPause.SetActive(false);
    }
    public void GenerationStations(int diff, int numLev)
    {
        Time.timeScale = 1f;

        levelDifficult = diff;
        _levelNumber = numLev;
        maxStation = CountStation(diff);
        maxScore = maxStation * 5;///*5 потому что максимум за хорошую остановку 5 очков

        int distanceBetweenStantions = 100;
        int minDistanceBetweenStantion = 50;
        int pathLength = maxStation * distanceBetweenStantions;

        int pathLenghtWhithounLastStantion = pathLength - minDistanceBetweenStantion;
        int xPositioon = 0;

        for (int i = 1; i < maxStation; i++)
        {
            int randomXPosition = Mathf.RoundToInt(Random.Range(minDistanceBetweenStantion, (pathLenghtWhithounLastStantion - xPositioon) / (maxStation - (i -1)) ));
            xPositioon += randomXPosition;
            st = Instantiate(stationPrefab, new Vector2(xPositioon, 1), stationPrefab.transform.rotation);
            st.GetComponent<StationPic>().AddPicStation();
            st.GetComponent<StationPic>().AddPicStop();
            st.GetComponent<StationPic>().SnowPicTrainStation();
        }

        xPositioon = pathLength;
        st = Instantiate(stationPrefab, new Vector2(1 * xPositioon, 1), stationPrefab.transform.rotation);
        st.GetComponent<StationPic>().AddPicStation();
        st.GetComponent<StationPic>().AddPicStop();
        st.GetComponent<StationPic>().SnowPicTrainStation();
        Instantiate(endSpotsPrefab, new Vector2(xPositioon + 50, 1), endSpotsPrefab.transform.rotation);///вот тут написала для точек

        Debug.Log(PlayerPrefs.GetInt("NewGameInfo") + "NewGameInfo");
        if (PlayerPrefs.GetInt("NewGameInfo") == 2)
            NewGameInfo.Instance.ShowPanelEducationOnLevel1();
    }
    private int CountStation(int diff)
    {
        if (PlayerPrefs.GetInt("NewGameInfo") == 2)
            return 3;
        else
        {
            int coef = 1;
            if (diff == coef + 4 * (PlayerPrefs.GetInt("Page") - 1))
            {
                return 5;
            }
            if (diff == coef + 1 + 4 * (PlayerPrefs.GetInt("Page") - 1))
            {
                return 7;
            }
            if (diff == coef + 2 + 4 * (PlayerPrefs.GetInt("Page") - 1))
            {
                return 10;
            }
            if (diff == coef + 3 + 4 * (PlayerPrefs.GetInt("Page") - 1))
            {
                return 11;
            }
            else return 0;
        }
    }
    public void LoadMenuLevel()
    {
        SceneManager.LoadScene("MenuLevel", LoadSceneMode.Single);
    }


    public void CheckInteractableButtonLeftRight(bool left, bool right)
    {
        buttonLeft.GetComponent<Button>().interactable = left;
        buttonRight.GetComponent<Button>().interactable = right;
    }
    public void TurnCameraToLeft()
    {
        GameObject.Find("Main Camera").GetComponent<MainCamera>().TurnCameraToLeft();
    }
    public void TurnCameraToRight()
    {
        GameObject.Find("Main Camera").GetComponent<MainCamera>().TurnCameraToRight();
    }
    public void DoorsInteractableButton(bool isOpen)
    {
        openDoor.interactable = isOpen;
    }
    public void OpenDoor()
    {
        StartCoroutine(FillCircle());
    }
    IEnumerator FillCircle()
    {
        Debug.Log("Door!!!!!!!!!!!!!!!!!!");
        while (openDoors.fillAmount < 1 && !TrainController.Instance.CheckMoving())
        {
            openDoors.fillAmount += Mathf.Lerp(minValue, maxValue, lerpSpeed);
            yield return new WaitForSeconds(0.000000000001f);
        }
        GameController.Instance.CheckSemaphore(false);//переключаем семафор зел
        DoorsInteractableButton(false);
        if (!TrainController.Instance.CheckMoving())
            Bording();
        else
        {
            Debug.Log("penalty");
            CreatePanelForMessage("wrongStart");
            TrainController.Instance.Penalty();
            DoorsInteractableButton(false);

        }
        openDoors.fillAmount = 0;
    }
    private void Bording()
    {
        carriageOnLevel = GameObject.FindGameObjectsWithTag("vagon");


        foreach (GameObject vagon in carriageOnLevel)
        {
            if (vagon.GetComponent<CarriagePassengerInfo>() != null)
                vagon.GetComponent<CarriagePassengerInfo>().PassengerTraffic();
        }
    }
    public void CreatePanelForMessage(string message)
    {
        GameObject panelForText = Instantiate(panelForMessage, canvas, false);

        panelForText.GetComponent<PanelForMessage>().ShowMassage(message);

        Destroy(panelForText, 5);
    }
   
}
