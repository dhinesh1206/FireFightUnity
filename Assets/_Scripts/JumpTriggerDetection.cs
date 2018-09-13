using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTriggerDetection : MonoBehaviour {

    public Animator armAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arm")
        {
            GameManager.instance.canJump = true; 
            // armAnimator.speed = 0.5f;
             foreach (Transform child in transform)
              {
                print(child.name);
                 child.gameObject.SetActive(true);
             }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arm")
        {
            GameManager.instance.canJump = false;
           // armAnimator.speed = 1f;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        } 
    }
}
