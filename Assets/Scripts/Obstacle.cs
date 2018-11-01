using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    private GameController gameController;
    private float step1;
    private float step2;
    private float step3;
    private float timer;

	void Awake ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").transform.GetComponent<GameController>();
        step1 = gameController.SPRITE_STEP_1;
        step2 = step1 + gameController.SPRITE_STEP_2;
        step3 = step2 + gameController.SPRITE_STEP_3;
    }
	

	void Update ()
    {
        timer += Time.deltaTime;
        if (timer < step1)
        {
            Color tmp = this.GetComponent<SpriteRenderer>().color;
            tmp.r = 0f;
            tmp.g = 0f;
            tmp.b = 0f;
            tmp.a = 0.3f;
            this.GetComponent<SpriteRenderer>().color = tmp;
        }
        else if (timer > step1 && timer < step2)
        {
            Color tmp = this.GetComponent<SpriteRenderer>().color;
            tmp.r = 1f;
            tmp.g = 1f;
            tmp.b = 1f;
            tmp.a = 0.3f;
            this.GetComponent<SpriteRenderer>().color = tmp;
        }
        else if (timer > step2 && timer < step3)
        {
            this.GetComponent<PolygonCollider2D>().enabled = true;
            Color tmp = this.GetComponent<SpriteRenderer>().color;
            tmp.r = 1f;
            tmp.g = 1f;
            tmp.b = 1f;
            tmp.a = 1f;
            this.GetComponent<SpriteRenderer>().color = tmp;
        }
        else if (timer > step3)
        {
            Destroy(this.gameObject);
        }
	}
}
