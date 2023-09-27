using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlus : MonoBehaviour
{
    private int index;
    public void Info(int i)
    {
        index = i;
    }
    public void Clic()
    {
        GameObject.Find("TrainDiagram(Clone)").GetComponent<TrainDiagram>().BluePlus(index);

    }
}
