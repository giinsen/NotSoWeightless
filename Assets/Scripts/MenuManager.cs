using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    Animator animator;
    private bool creditDisplay = false;

    public GameObject mongolfiereJouer;
    public GameObject mongolfiereOptions;
    public GameObject mongolfiereQuitter;
    public GameObject panelCredit;

    void Start ()
    {
        animator = transform.parent.GetComponent<Animator>();
    }

    private void OnMouseOver()
    { 
        animator.SetBool("LaunchAnim", true);
        if (Input.GetMouseButtonDown(0))
        {
            if (transform.parent.gameObject.name == "Jouer")
            {
                SceneManager.LoadScene("GameScene");
                SoundManager.instance.MenuClick();
            }
            else if (transform.parent.gameObject.name == "Options")
            {
                SoundManager.instance.MenuClick();
            }
            else if (transform.parent.gameObject.name == "Credits")
            {
                if (creditDisplay)
                    creditDisplay = false;
                else if (!creditDisplay)
                    creditDisplay = true;
                OnCredit();
                SoundManager.instance.MenuClick();
            }
            else if (transform.parent.gameObject.name == "Quitter")
            {
                SoundManager.instance.MenuClick();
            }
        }
    }

    private void OnMouseExit()
    {
        animator.SetBool("LaunchAnim", false);
    }

    private void OnCredit()
    {
        if (creditDisplay)
        {
            mongolfiereJouer.SetActive(false);
            mongolfiereOptions.SetActive(false);
            mongolfiereQuitter.SetActive(false);
            panelCredit.SetActive(true);
            creditDisplay = true;
        }
        else
        {
            mongolfiereJouer.SetActive(true);
            mongolfiereOptions.SetActive(true);
            mongolfiereQuitter.SetActive(true);
            panelCredit.SetActive(false);
            creditDisplay = false;
        }
    }
}
