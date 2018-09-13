using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerPrecence : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "JumpedPlayer")
        {
            if (!gameObject.GetComponent<CurrentPlayerIndicator>().jumpedToCrate)
            {
                GameManager.instance.JumptoCrate(gameObject, collision.gameObject);
                gameObject.GetComponent<CurrentPlayerIndicator>().objectInCrate = collision.gameObject;
                gameObject.GetComponent<CurrentPlayerIndicator>().jumpedToCrate = true;
            }
        }
    }
}
