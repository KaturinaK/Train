using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SingStation : MonoBehaviour
{
    [SerializeField] private GameObject semaphore;

    public GameObject ChoiseSemaphore()
    {
        return semaphore;
    }
    
}
