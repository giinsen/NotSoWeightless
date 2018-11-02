using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private GameController gameController;
    private float timer;
    public GameObject[] obstacles;
    private Calibrate calibrate;

    void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").transform.GetComponent<GameController>();
        calibrate = GameObject.FindGameObjectWithTag("Calibrate").transform.GetComponent<Calibrate>();
    }

    void Update ()
    {
        if (!calibrate.calibrateDone) return;

        timer += Time.deltaTime;
        if (timer >= gameController.SPAWNING_DELAY)
        {
            timer = 0f;
            Instantiate(obstacles[0]);
        }
	}
}
