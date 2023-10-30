using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSShow : MonoBehaviour
{
    private float maxFps = 60;
    private float fps;
    [SerializeField] private TextMeshProUGUI fpsShow;

    private void Awake()
    {
        Application.targetFrameRate = (int)maxFps;
    }
    private void Update()
    {
        fps = 1.0f / Time.deltaTime;
        fpsShow.text = "FPS " + (int)fps;
    }
    
}
