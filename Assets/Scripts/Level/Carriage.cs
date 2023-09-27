using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Carriage : ParentCarriages
{
    private WheelJoint2D[] wheel;
    private Rigidbody2D body;
    private JointMotor2D jointMotor;
    private float carSpeed, carSmooth;
    
    private bool isPessGas;
    private bool isPessBreak;
    private bool isStop;

    private float moveSpeed; // макс скорость
    private float smoothAcceleration; // плавное ускорение
    private float smoothBrake; // плавное торможение
    private float stopStep = 1000; // шаг для быстрого торможения

    private ImprovementCharacteristic locomotiveCharacteristic;
    private void Awake()
    {
        
        body = GetComponent<Rigidbody2D>();
        wheel = GetComponents<WheelJoint2D>();
        jointMotor.maxMotorTorque = 10000;///
        jointMotor.motorSpeed = 0;
        foreach (WheelJoint2D w in wheel)
        {
            w.useMotor = true;
            w.motor = jointMotor;
        }
    }
    
    private void TakeLocomotiveInfo()
    {
        locomotiveCharacteristic = ParentCarriages.Instance.LocomotiveImprovment();
        
        LocomotiveCharacteristics locomotive = ParentCarriages.Instance.LocomotiveInstance();
       // moveSpeed = locomotive.MaxSpeed + locomotive.MaxSpeedImprovmentForOneLevel * locomotiveCharacteristic.LevelMaxSpeed;
        moveSpeed = locomotive.MaxSpeed + locomotive.MaxSpeedImprovmentForOneLevel * locomotiveCharacteristic.improvmentLevels[1];
        smoothAcceleration = locomotive.AccelerationSpeed + locomotive.AccelerationSpeedImprovmentForOneLevel * locomotiveCharacteristic.improvmentLevels[2];
        smoothBrake = locomotive.BrakingSpeed + locomotive.BrakingSpeedImprovmentForOneLevel * locomotiveCharacteristic.improvmentLevels[3];

        TrainController.Instance.GetTrainSpeedCharacteristic(moveSpeed, smoothAcceleration, locomotiveCharacteristic.improvmentLevels[2], smoothBrake, locomotiveCharacteristic.improvmentLevels[3]);
        //Debug.Log("moveSpeed " + moveSpeed);
        //Debug.Log("smoothAcceleration " + smoothAcceleration);
        //Debug.Log("smoothBrake " + smoothBrake);
    }
   
    public int GetPrestige()
    {
        //Debug.Log("locomotiveCharacteristic.LevelPrestige " + locomotiveCharacteristic.LevelPrestige);
        return locomotiveCharacteristic.LevelPrestige;
    }
    
    public void PressGas()
    {
        TakeLocomotiveInfo();
        isPessGas = true;
        isStop = false;
        //Debug.Log("PressGas");
    }
    public void PressGasUp()
    {
        isPessGas = false;
    }
    public void PressBreake()
    {
        isPessGas = false;
        isPessBreak = true;
    }
    public void PressBreakeUp()
    {
        isPessBreak = false;
    }
    public void PressStop()
    {
        isStop = true;
    }
    private void Update()
    {
        MoveTrainControl();

        AdjustSpeed();
    }
    private void MoveTrainControl()
    {
        if (isPessGas)//движемся + ускоряемся
        {
            carSpeed = -moveSpeed * 6;
            carSmooth = smoothAcceleration;
            //Debug.Log("движемся + ускоряемся");
        }
        if(isPessBreak) //движемся + постепенно тормозим
        {
            carSpeed = 0; 
            carSmooth = smoothBrake;

        }
        if(!isPessGas && !isPessBreak)//движемся
        {
            carSpeed = jointMotor.motorSpeed;
            carSmooth = 0;
        }
        if (isStop)//резко тормозим
        {

            carSpeed = 0;
            carSmooth = stopStep;

        }
        
    }
        void AdjustSpeed()
    {
        
        jointMotor.motorSpeed = Mathf.Lerp(jointMotor.motorSpeed, carSpeed, carSmooth * Time.deltaTime);
        foreach (WheelJoint2D w in wheel)
        {
            w.motor = jointMotor;
        }
        if(jointMotor.motorSpeed >-5 && isPessBreak)
        {
            IsStopTrue();
        }
    }
    public void IsStopTrue()
    {
        isStop = true;

    }
    
}
