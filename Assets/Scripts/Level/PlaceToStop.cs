using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceToStop : MonoBehaviour
{
    [SerializeField] private GameObject placeToStopImage;

    public void ChangePic()
    {
        placeToStopImage.SetActive(false);
    }
}
