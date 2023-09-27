using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    private int numberLevel;
    private int difficulty;
    private int stars;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image[] starsImage; 
    

    public void FillInfo(int level, int diff, int star)
    {
        numberLevel = level;
        difficulty = diff;
        stars = star;

        levelText.text = numberLevel.ToString();
        for(int i = 0; i < stars; i++)
        {
            starsImage[i].sprite = Resources.Load<Sprite>("star");
        }
        if(level % 10 == 0 && level/10 > PlayerPrefs.GetInt("10button"))
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }
    public void LoadLevel()
    {
        InfoDontDestroy.Instance.TakeInfo(difficulty, numberLevel);
    }

}
