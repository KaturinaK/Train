using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private List<GameObject> _vagon;
    private int i = 0;
    private bool left;
    private bool right;
    private void Update()
    {
        if (_vagon != null && _vagon[i] != null)
        {
            transform.position = new Vector3(_vagon[i].transform.position.x, 0.4f, -100);
        }
        CheckButtonInteractable();
    }
    public void GetListTrain(List<GameObject> vagon)
    {
        _vagon = vagon;
    }

    private void CheckButtonInteractable()
    {
        if (i >= 0 && i < _vagon.Count - 1)
        {
            left = true;
        }
        else left = false;

        if (i > 0 && i <= _vagon.Count - 1)
        {
            right = true;
        }
        else right = false;

        GameController.Instance.CheckInteractableButtonLeftRight(left, right);
    }
    
    public void TurnCameraToLeft()
    {
        if (i >= 0 && i < _vagon.Count - 1)
        i++;
         
    }
    
    public void TurnCameraToRight()
    {
        if(i > 0 && i <= _vagon.Count - 1)
        i--;
    }
}
