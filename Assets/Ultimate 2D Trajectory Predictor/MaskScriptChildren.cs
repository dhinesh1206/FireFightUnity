﻿using UnityEngine;
using System.Collections;

public class MaskScriptChildren : MonoBehaviour {
    

	void OnTriggerStay2D(Collider2D other){
		if (other.GetComponent<Collider2D> ().isTrigger == false && other.gameObject.tag != "Player") {
			
			//player.GetComponent<trajectoryScript> ().collided (gameObject);
         //   GameManager.instance.transform.gameObject.GetComponent<trajectoryScript>().collided(gameObject);

		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.GetComponent<Collider2D> ().isTrigger == false) {

			//player.GetComponent<trajectoryScript> ().uncollided (gameObject);
          //  GameManager.instance.transform.gameObject.GetComponent<trajectoryScript>().collided(gameObject);

		}
	}
}
