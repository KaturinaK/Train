using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainStop : MonoBehaviour
{
    //[SerializeField] private GameObject semaphore;
    public void Destroy()
    {
        
        Destroy(gameObject);
    }
    
    /*public GameObject ChoiseSemaphore()
    {
        return semaphore;
    }*/
}
