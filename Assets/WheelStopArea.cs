using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelStopArea : MonoBehaviour {
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arm")
        {
            GameManager.instance.CheckRotationSpeed();
        }
    }

}
