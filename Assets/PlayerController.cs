using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteLib;
using System.Text.RegularExpressions;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour {

    public BalanceManager balance;
    private GameController gameController;

    private Rigidbody rb;

    private float speedHorizontal;
    private float speedVertical;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameController = GameObject.FindGameObjectWithTag("GameController").transform.GetComponent<GameController>();

        Invoke("Calibrate", 0.5f);
    }

    void Calibrate()
    {
        gameController.MAX_WEIGHT = balance.weight * 2;
        Debug.Log("balance.weight :  " + balance.weight);
        Debug.Log("gameController.MAX_WEIGHT :  " + gameController.MAX_WEIGHT);
    }

    private void Update()
    {
        SetSpeedHorizontal();
        SetSpeedVertical();

        rb.velocity = Vector3.right * speedHorizontal * gameController.SPEED_MULTIPLICATOR_HORIZONTAL
               + Vector3.up * speedVertical * gameController.SPEED_MULTIPLICATOR_VERTICAL;
               //+ Vector3.forward;
    }


    private void SetSpeedHorizontal()
    {
        float left = balance.bottomLeft + balance.topLeft;
        float right = balance.bottomRight + balance.topRight;
        speedHorizontal = right - left;
    }

    private void SetSpeedVertical()
    {
        float averageWeight = gameController.MAX_WEIGHT / 2;
        speedVertical = averageWeight - balance.weight;
    }
}
