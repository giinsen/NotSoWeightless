using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    private GameController gameController;
    private float step1;
    private float step2;
    private float step3;
    private float timer;

    private Vector3 offSetPostition;

    void Awake ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").transform.GetComponent<GameController>();
        step1 = gameController.SPRITE_STEP_1;
        step2 = step1 + gameController.SPRITE_STEP_2;
        step3 = step2 + gameController.SPRITE_STEP_3;
        offSetPostition = transform.position;
        SoundManager.instance.Obstacle(2f, 2f, 2f);

        //int r = Random.Range(0, anchorPoints.Length);

        //Vector3 position = new Vector3(anchorPoints[r].x, anchorPoints[r].y, 0);
        //transform.position = position;

        //if (!(anchorPointsRotation.Length == 0))
        //{
        //    Quaternion rotation = Quaternion.Euler(new Vector3(anchorPointsRotation[r].x, anchorPointsRotation[r].y, anchorPointsRotation[r].z));
        //    transform.rotation = rotation;
        //}       
    }
	

	void Update ()
    {
        ObstacleLifeCycle();
        if (gameObject.name.Contains("Helice"))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * 40);
        }
        else if (gameObject.name.Contains("Plane"))
        {
            if (offSetPostition.x > 0 && timer > step2 && timer < step3) //délacer vers la gauche
            {
                transform.parent.GetComponent<Animator>().SetTrigger("AvionGaucheTrigger");
            }
            if (offSetPostition.x < 0 && timer > step2 && timer < step3) //délacer vers la droite
            {
                transform.parent.GetComponent<Animator>().SetTrigger("AvionDroiteTrigger");
            }
        }
    }

    private void ObstacleLifeCycle()
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
            Destroy(transform.parent.gameObject);
        }
    }
}
