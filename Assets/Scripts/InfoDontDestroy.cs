using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoDontDestroy : MonoBehaviour
{
    private int difficult;
    private int numberLevel;
    private int _stars;
    [SerializeField] private List<string> carriageUsedOnLevel;
    public static InfoDontDestroy Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    static private InfoDontDestroy _instance;

    
    public void TakeInfo(int diff, int numLevel)
    {
        difficult = diff;
        numberLevel = numLevel;
        
        StartCoroutine("LoadScenes");
        
    }
   
    IEnumerator LoadScenes()
    {
        
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        
        yield return null;
        GameController.Instance.GenerationStations(difficult, numberLevel);
        TrainGeneration.Instance.GenerationTrain();
        Background.Instance.GenerationBackground(numberLevel);
    }
    IEnumerator LoadSceneMenu()
    {
        SceneManager.LoadScene("MenuLevel", LoadSceneMode.Single);
        yield return null;
        LevelController.Instance.TakeInfoLevel(_stars, numberLevel);

    }
    public void TakeInfoLevel(int star)//тут получаю значение звезд
    {
        _stars = star;

        StartCoroutine("LoadSceneMenu");
    }
    
   
}
