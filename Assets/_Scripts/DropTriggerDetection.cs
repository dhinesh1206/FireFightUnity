using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTriggerDetection : MonoBehaviour {
    
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Arm")
    //    {
           
    //       // armAnimator.speed = 0.3f;
    //        //foreach(Transform child in transform)
    //        //{
    //        //    child.gameObject.SetActive(true);
    //        //}

    //       // print(armAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.);
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arm")
        {
            if (collision.gameObject.transform.parent.GetChild(1).GetComponent<CurrentPlayerIndicator>().objectInCrate)
            {
                print(collision.gameObject.transform.parent.GetChild(1).GetComponent<CurrentPlayerIndicator>().objectInCrate.name);
                GameManager.instance.DropPlayer(collision.transform.parent.GetChild(1).GetComponent<CurrentPlayerIndicator>().objectInCrate, collision);
                collision.gameObject.transform.parent.GetChild(1).GetComponent<CurrentPlayerIndicator>().jumpedToCrate = false;
            }
            //GameManager.instance.canDrop = false;
            //foreach (Transform child in transform)
            //{
            //    child.gameObject.SetActive(false);
            //}
            //armAnimator.speed = 1f;
        }//
    }
}
