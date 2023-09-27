using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semaphore : MonoBehaviour
{
    
    public void SwitchSemaphoreLights(bool b)
    {
        //Debug.Log("SSL");
        if (b == true)
        {
            //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sem red");
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sem red/" + PlayerPrefs.GetInt("Page"));
            //Debug.Log("Red");
        }
        else 
        { 
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sem green/" + PlayerPrefs.GetInt("Page"));
           // Debug.Log("Green");
        }
    }
}
