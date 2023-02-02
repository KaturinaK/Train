using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    public GameObject train;

    public Rigidbody2D rbTrain;

    public Vector2 direction;

    [Header("Controls")]
    public bool assel;
    public bool breake;

    [Header("Train steeings")]
    public float enginePower = 250;
    public float breakeForce = 15000;

    void Start()
    {
        rbTrain = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //assel = Input.GetKeyDown(KeyCode.W);
        //breake = Input.GetKeyDown(KeyCode.S);

        if (Input.GetKeyDown(KeyCode.W))
        {
            train.transform.position += transform.transform.right * enginePower * Time.deltaTime;
        }
    }
}
