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
using System;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour {

    private BalanceManager balanceManager;
    private GameController gameController;
    private Calibrate calibrate;

    private Rigidbody2D rb;

    public float health;

    private float speedHorizontal;
    private float speedVertical;

    private float balloonHitTimer;

    private List<int> sandbagList = new List<int>();

    private int actualSandbag = 9;

    private void Start()
    {
        SoundManager.instance.GameStart();
        rb = GetComponent<Rigidbody2D>();
        balanceManager = GameObject.FindGameObjectWithTag("BalanceManager").transform.GetComponent<BalanceManager>();
        gameController = GameObject.FindGameObjectWithTag("GameController").transform.GetComponent<GameController>();
        calibrate = GameObject.FindGameObjectWithTag("Calibrate").transform.GetComponent<Calibrate>();
        health = gameController.PLAYER_HEALTH;
        ChargeSandbagList(6);        
    }


    private void Update()
    {
        if (!calibrate.calibrateDone)
            return;

        SetSpeedHorizontal();
        SetSpeedVertical();
        SetPitching();
        SetSandbag();

        balloonHitTimer += Time.deltaTime;

        SoundManager.instance.BalloonSpeed(Mathf.Abs(Mathf.Max(speedHorizontal, speedVertical*3) / 8));

        rb.velocity = Vector3.right * speedHorizontal * gameController.SPEED_MULTIPLICATOR_HORIZONTAL
               + Vector3.up * speedVertical * gameController.SPEED_MULTIPLICATOR_VERTICAL;

        //todo : si poids = 0 descendre
    }


    private void SetSpeedHorizontal()
    {
        float left = balanceManager.bottomLeft + balanceManager.topLeft;
        float right = balanceManager.bottomRight + balanceManager.topRight;
        speedHorizontal = right - left;
    }

    private void SetSpeedVertical()
    {
        speedVertical = (gameController.AVERAGE_WEIGHT - balanceManager.weight) *2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (balloonHitTimer >= 1.5f)
        {
            balloonHitTimer = 0.0f;
            SoundManager.instance.BalloonHit();
        }
        if (collision.gameObject.tag == "Wall")
        {
            ReduceHealth(0.5f);
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            ReduceHealth(1f);
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.constraints = RigidbodyConstraints2D.None;
    }

    private void SetPitching()
    {
        if (speedHorizontal < -1.5) //gauche
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 8), Time.deltaTime * 10);
        }
        else if (speedHorizontal > 1.5) //droite
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -8), Time.deltaTime * 10);
        }
        else //milieu
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5);
        }
    }

    private void SetSandbag()
    {
        if (speedHorizontal < -1.5) //gauche
        {
            if (balanceManager.weight < gameController.AVERAGE_WEIGHT * 0.8) //haut
            {
                sandbagList.RemoveAt(0); sandbagList.Add(1);
                ActiveSandbag(false, true, false, false);
            }
                
            else if (balanceManager.weight > gameController.AVERAGE_WEIGHT * 1.2) //bas
            {
                sandbagList.RemoveAt(0); sandbagList.Add(2);
                ActiveSandbag(true, true, false, true);
            }
                
            else //stable
            {
                sandbagList.RemoveAt(0); sandbagList.Add(3);
                ActiveSandbag(false, true, false, true);
            }
                
        }
        else if (speedHorizontal > 1.5) //droite
        {
            if (balanceManager.weight < gameController.AVERAGE_WEIGHT * 0.8) //haut
            {
                sandbagList.RemoveAt(0); sandbagList.Add(4);
                ActiveSandbag(true, false, false, false);
            }
                
            else if (balanceManager.weight > gameController.AVERAGE_WEIGHT * 1.2) //bas
            {
                sandbagList.RemoveAt(0); sandbagList.Add(5);
                ActiveSandbag(true, true, true, true);
            }
                
            else //stable
            {
                sandbagList.RemoveAt(0); sandbagList.Add(6);
                ActiveSandbag(true, false, true, false);
            }
                
        }
        else //ni droite ni gauche
        {
            if (speedVertical > 1.5) //haut
            {
                sandbagList.RemoveAt(0); sandbagList.Add(7);
                ActiveSandbag(false, false, false, false);
            }
            else if (speedVertical < -1.5) //bas
            {
                sandbagList.RemoveAt(0); sandbagList.Add(8);
                ActiveSandbag(true, true, true, true);
            }              
            else //stable
            {
                sandbagList.RemoveAt(0); sandbagList.Add(9);
                ActiveSandbag(true, true, false, false);
            }
        }

    }

    private void ActiveSandbag(bool premierDroite, bool premierGauche, bool deuxiemeDroite, bool deuxiemeGauche)
    {
        if (ChangeSandbag() && actualSandbag != sandbagList.ToArray()[0])
        {
            actualSandbag = sandbagList.ToArray()[0];
            SoundManager.instance.BagSpawn(1);
            transform.Find("Sandbags").GetChild(0).gameObject.SetActive(premierDroite);
            transform.Find("Sandbags").GetChild(1).gameObject.SetActive(premierGauche);
            transform.Find("Sandbags").GetChild(2).gameObject.SetActive(deuxiemeDroite);
            transform.Find("Sandbags").GetChild(3).gameObject.SetActive(deuxiemeGauche);
        }
    }

    private bool ChangeSandbag()
    {
        return sandbagList.ToArray()[0] == sandbagList.ToArray()[1]
            && sandbagList.ToArray()[0] == sandbagList.ToArray()[2]
            && sandbagList.ToArray()[0] == sandbagList.ToArray()[3]
            && sandbagList.ToArray()[0] == sandbagList.ToArray()[4]
            && sandbagList.ToArray()[0] == sandbagList.ToArray()[5];
    }

    private void ChargeSandbagList(int nbBag)
    {
        for (int i = 0; i < nbBag; i++)
        {
            sandbagList.Add(9);
        }
    }

    private void ReduceHealth(float damage)
    {
        this.health -= damage;
    }
}
