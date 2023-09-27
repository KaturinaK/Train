using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SingStation : MonoBehaviour
{
    [SerializeField] private GameObject semaphore;

    public void Destroy()//??????????????
    {
        //GameController.Instance.AddScore();

        Destroy(gameObject);
    }
    public GameObject ChoiseSemaphore()
    {
        //Debug.Log(semaphore);
        return semaphore;
    }
    
}
