using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public static Background Instance
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
    static private Background _instance;

    public void GenerationBackground(int levelNumber)
    {
        if (levelNumber <= 10)
        {
            GameObject background = Resources.Load<GameObject>("Background/BackgroundMountain");
            GameObject back = Instantiate(background);
        }
        else if(levelNumber > 10 && levelNumber <= 20)
        {
            GameObject background = Resources.Load<GameObject>("Background/BackgroundDesert");
            GameObject back = Instantiate(background);
        }
        else if(levelNumber >= 20 && levelNumber <= 30)
        {
            GameObject background = Resources.Load<GameObject>("Background/BackgroundSnow");
            GameObject back = Instantiate(background);
        }
        else if(levelNumber >= 30 && levelNumber <= 40)
        {
            GameObject background = Resources.Load<GameObject>("Background/BackgroundGraveyard");
            GameObject back = Instantiate(background);
        }

    }
}
