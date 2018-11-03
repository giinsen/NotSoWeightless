using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public GameObject[] parallax;
    private float timer;
    private Calibrate calibrate;

    private void Start()
    {
        //calibrate = GameObject.FindGameObjectWithTag("Calibrate").transform.GetComponent<Calibrate>();
    }

    void Update ()
    {
        //if (!calibrate.calibrateDone)
        //    return;

        timer += Time.deltaTime;
	    foreach (GameObject g in parallax)
        {
            if (timer % 8 >= 4) //8 secondes
            {
                g.transform.position = Vector3.Lerp(g.transform.position, g.transform.position + Vector3.left * 2, Time.deltaTime * 0.3f);
            }
            else
            {
                g.transform.position = Vector3.Lerp(g.transform.position, g.transform.position + Vector3.right * 2, Time.deltaTime * 0.3f);
            }
        }	
	}
}
