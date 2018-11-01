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

    private BalanceManager balanceManager;
    private GameController gameController;

    private Rigidbody2D rb;

    private float speedHorizontal;
    private float speedVertical;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        balanceManager = GameObject.FindGameObjectWithTag("BalanceManager").transform.GetComponent<BalanceManager>();
        gameController = GameObject.FindGameObjectWithTag("GameController").transform.GetComponent<GameController>();
    }


    private void Update()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        SetSpeedHorizontal();
        SetSpeedVertical();
        SoundManager.instance.BalloonSpeed(1);

        rb.velocity = Vector3.right * speedHorizontal * gameController.SPEED_MULTIPLICATOR_HORIZONTAL
               + Vector3.up * speedVertical * gameController.SPEED_MULTIPLICATOR_VERTICAL;
    }


    private void SetSpeedHorizontal()
    {
        float left = balanceManager.bottomLeft + balanceManager.topLeft;
        float right = balanceManager.bottomRight + balanceManager.topRight;
        speedHorizontal = right - left;
    }

    private void SetSpeedVertical()
    {
        float averageWeight = gameController.MAX_WEIGHT / 2;
        speedVertical = averageWeight - balanceManager.weight;
    }
}
