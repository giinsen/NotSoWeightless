using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    private GameObject player;
    public int playerPositionGrid;


    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update ()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(player.transform.position);

        //résolution 16:9 -> 384x216
        if (screenPos.y <= 116) //partie basse
        {
            if (screenPos.x < 122) // gauche
                playerPositionGrid = 1;
            if (screenPos.x >= 122 && screenPos.x < 244) // milieu
                playerPositionGrid = 2;
            if (screenPos.x >= 244) // droite
                playerPositionGrid = 3;
        }
        else //partie haute
        {
            if (screenPos.x < 122) // gauche
                playerPositionGrid = 4;
            if (screenPos.x >= 122 && screenPos.x < 244) // milieu
                playerPositionGrid = 5;
            if (screenPos.x >= 244) // droite
                playerPositionGrid = 6;
        }
    }
}
