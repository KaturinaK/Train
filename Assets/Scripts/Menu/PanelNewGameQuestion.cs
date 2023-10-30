using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelNewGameQuestion : MonoBehaviour
{
    public void Yes()
    {
        GameObject.Find("Save").GetComponent<SaveGame>().NewGame();
    }
    public void No()
    {
        Destroy(gameObject);
    }
}
