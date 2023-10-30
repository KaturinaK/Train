using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveStartNewGame : MonoBehaviour
{
    [SerializeField] private GameObject buttonContinue;
    private void Start()
    {
        if (PlayerPrefs.GetInt("isGameStart") == 1)
            buttonContinue.GetComponent<Button>().interactable = true;
    }


}
