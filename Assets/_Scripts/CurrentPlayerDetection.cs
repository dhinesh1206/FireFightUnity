using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayerDetection : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            GameManager.instance.DetectPlayer(collision.gameObject);
    }
}
