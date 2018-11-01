using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    Animator animator;

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
            }
            else if (transform.parent.gameObject.name == "Options")
            {
                Debug.Log("Options");
            }
            else if (transform.parent.gameObject.name == "Credits")
            {
                Debug.Log("Credits");
            }
            else if (transform.parent.gameObject.name == "Quitter")
            {
                Debug.Log("Quitter");
            }
        }
    }

    private void OnMouseExit()
    {
        animator.SetBool("LaunchAnim", false);
    }
}
