using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrunDetection : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "JumpedPlayer")
        {
            // collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
           collision.gameObject.SetActive(false);
           GameManager.instance.GameOver();
        }
    }
}
