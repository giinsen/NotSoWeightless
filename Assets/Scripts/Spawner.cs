using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private GameController gameController;
    private float timer;
    public GameObject[] obstacles;
    private Calibrate calibrate;
    private Grid grid;
    private PlayerController player;

    private int range;
    private bool foundRange = false;

    void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").transform.GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerController>();
        calibrate = GameObject.FindGameObjectWithTag("Calibrate").transform.GetComponent<Calibrate>();
        grid = GameObject.FindGameObjectWithTag("Grid").transform.GetComponent<Grid>();
    }

    void Update ()
    {
        if (!calibrate.calibrateDone) return;
        if (player.health <= 0) return;

        timer += Time.deltaTime;
        if (timer >= gameController.SPAWNING_DELAY)
        {
            timer = 0f;
            Instantiate(obstacles[ChooseObstacle()]);
        }
	}

    private int ChooseObstacle()
    {
        range = Random.Range(0, obstacles.Length);

        if (grid.playerPositionGrid == 1 && !obstacles[range].GetComponent<ObstacleGroup>().basGauche) //si joueur en bas a gauche & l'obstacle n'a pas de collider en bas a gauche
        {
            while (!foundRange)
            {
                range = Random.Range(0, obstacles.Length);
                if (obstacles[range].GetComponent<ObstacleGroup>().basGauche)
                {
                    foundRange = true;
                }
            }
        }
        else if (grid.playerPositionGrid == 2 && !obstacles[range].GetComponent<ObstacleGroup>().basMilieu)
        {
            while (!foundRange)
            {
                range = Random.Range(0, obstacles.Length);
                if (obstacles[range].GetComponent<ObstacleGroup>().basMilieu)
                {
                    foundRange = true;
                }
            }
        }
        else if (grid.playerPositionGrid == 3 && !obstacles[range].GetComponent<ObstacleGroup>().basDroite)
        {
            while (!foundRange)
            {
                range = Random.Range(0, obstacles.Length);
                if (obstacles[range].GetComponent<ObstacleGroup>().basDroite)
                {
                    foundRange = true;
                }
            }
        }
        else if (grid.playerPositionGrid == 4 && !obstacles[range].GetComponent<ObstacleGroup>().hautGauche)
        {
            while (!foundRange)
            {
                range = Random.Range(0, obstacles.Length);
                if (obstacles[range].GetComponent<ObstacleGroup>().hautGauche)
                {
                    foundRange = true;
                }
            }
        }
        else if (grid.playerPositionGrid == 5 && !obstacles[range].GetComponent<ObstacleGroup>().hautMilieu)
        {
            while (!foundRange)
            {
                range = Random.Range(0, obstacles.Length);
                if (obstacles[range].GetComponent<ObstacleGroup>().hautMilieu)
                {
                    foundRange = true;
                }
            }
        }
        else if (grid.playerPositionGrid == 6 && !obstacles[range].GetComponent<ObstacleGroup>().hautDroite)
        {
            while (!foundRange)
            {
                range = Random.Range(0, obstacles.Length);
                if (obstacles[range].GetComponent<ObstacleGroup>().hautDroite)
                {
                    foundRange = true;
                }
            }
        }

        foundRange = false;
        return range;
    }
}
