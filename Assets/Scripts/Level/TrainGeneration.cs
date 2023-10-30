using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainGeneration : MonoBehaviour
{
    [SerializeField] private GameObject carriagePrefab;
    [SerializeField] private List<string> carriageUsedOnLevel;
    [SerializeField] private List<GameObject> _vagon;
    [SerializeField] private float vect = 0;
    public static TrainGeneration Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        TakeSaveInfoTrain();
        
    }
    static private TrainGeneration _instance;
    
    public void TakeSaveInfoTrain()
    {
        carriageUsedOnLevel = gameObject.GetComponent<SaveTrain>().LoadGameInfo(PlayerPrefs.GetString("nameTrain"));
        
    }

    public void GenerationTrain()
    {
        
        for(int i = 0; i < carriageUsedOnLevel.Count; i++)
        {
            if (carriageUsedOnLevel[i] != "ghostCarriage")
            {
                GameObject g = Resources.Load<GameObject>("Train/" + carriageUsedOnLevel[i]);
                vect -= g.GetComponent<RectTransform>().rect.width/2  ;
                GameObject vagon = Instantiate(g, new Vector3(vect, g.transform.position.y, 0), Quaternion.identity);
                if(i > 0)
                vagon.GetComponent<CarriagePassengerInfo>().GetInfoTrain(gameObject.GetComponent<ShopDictionary>().carriageDic[g.name]);
                _vagon.Add(vagon);

                vect -= g.GetComponent<RectTransform>().rect.width/2;

                _vagon[i].name = g.name;
            }
        }
        for(int i = 0; i < _vagon.Count - 1; i++)
        {
            _vagon[i].GetComponent<FixedJoint2D>().enabled = true;
            _vagon[i].GetComponent<FixedJoint2D>().connectedBody = _vagon[i + 1].GetComponent<Rigidbody2D>();
        }

        GameObject.Find("Main Camera").GetComponent<MainCamera>().GetListTrain(_vagon);
        TrainController.Instance.GetTrain(_vagon[0]);
        ParentCarriages.Instance.GetInfoLocomotive(_vagon[0].name);
        ParentCarriages.Instance.GetInfoLocomotiveImprovment(_vagon[0].name);
    }
    
}
