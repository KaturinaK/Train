using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    public KeyCode move = KeyCode.D; // �������� ������
    public KeyCode smoothStop = KeyCode.A; // ������� ����������
    public KeyCode stop = KeyCode.Space; // ������� ����������
    public float moveSpeed = 250; // ���� ��������
   //public bool invert = true; // ������������� �����������
    public float smoothStep = 1; // ������� ���������/����������
    public float stopStep = 10; // ��� ��� �������� ����������
    public float freeStep = 0f; // ����������� ������� ��������, ����� ������� �������� �� ������
    [HideInInspector] public float velocity; // ������� �������� ���� �� �
    private JointMotor2D jointMotor;
    private WheelJoint2D[] wheel;
    private Rigidbody2D body;
   // private int inv;
    private float curSpeed, curSmooth;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        wheel = GetComponents<WheelJoint2D>();
        jointMotor.maxMotorTorque = 10000;
        jointMotor.motorSpeed = 0;
        foreach (WheelJoint2D w in wheel)
        {
            w.useMotor = true;
            w.motor = jointMotor;
        }
    }

    float Round(float value, float to) // ��������� �� ���������� ��������
    {
        return ((int)(value * to)) / to;
    }

    void MovementControl()
    {
        //curSmooth = freeStep;
        if (Input.GetKey(move))
        {
            curSpeed = -moveSpeed; //* inv;
            curSmooth = smoothStep;
        }
        else if (Input.GetKey(smoothStop))
        {
            curSpeed = 0;
            curSmooth = smoothStep;
        }
        else if (Input.GetKey(stop))
        {
            curSpeed = 0;
            curSmooth = stopStep;
        }
        else
        {
            //curSpeed = 200;
            curSmooth = freeStep;
        }
    }
    public void PressGas()
    {
        curSpeed = -moveSpeed; //* inv;
        curSmooth = smoothStep;
    }
    public void PressBreake()
    {
        curSpeed = 0;
        curSmooth = smoothStep;
    }
    public void PressStop()
    {
        curSpeed = 0;
        curSmooth = stopStep;
    }
    void Update()
    {
        velocity = Round(body.velocity.x, 100f); // ��������� �� �����
        //if (invert) inv = -1; else if (!invert) inv = 1;

        MovementControl();
        AdjustSpeed();
    }

    void AdjustSpeed()
    {
        jointMotor.motorSpeed = Mathf.Lerp(jointMotor.motorSpeed, curSpeed, curSmooth * Time.deltaTime);
        foreach (WheelJoint2D w in wheel)
        {
            w.motor = jointMotor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SingStation")
        {
            Debug.Log("���");
        }
    }
}
    


