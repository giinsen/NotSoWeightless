using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calibrate : MonoBehaviour {

    private BalanceManager balanceManager;
    private GameController gameController;
    private GameObject player;

    public GameObject calibratePanel;
    public Text totalWeightText;
    public bool calibrateDone = false;

    void Start () {
        calibratePanel.SetActive(true);
        balanceManager = GameObject.FindGameObjectWithTag("BalanceManager").transform.GetComponent<BalanceManager>();
        gameController = GameObject.FindGameObjectWithTag("GameController").transform.GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update () {
        totalWeightText.text = Mathf.RoundToInt(balanceManager.weight).ToString();
    }

    public void Go()
    {
        calibratePanel.SetActive(false);
        calibrateDone = true;
        gameController.AVERAGE_WEIGHT = balanceManager.weight;
        gameController.MAX_WEIGHT = balanceManager.weight * 2;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Debug.Log("balance.weight :  " + balanceManager.weight);
        Debug.Log("gameController.MAX_WEIGHT :  " + gameController.MAX_WEIGHT);
    }
}
