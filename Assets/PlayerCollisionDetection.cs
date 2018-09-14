using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PickUps")
        {
            Destroy(collision.gameObject);
            GameManager.instance.CoinPickedUp();
        }
    }
}
