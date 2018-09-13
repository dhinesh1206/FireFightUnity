using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceAtapoint : MonoBehaviour {

    public Rigidbody2D forcepoint;
    public Vector2 forceAngle,pointtoaddForce;
    public float forcevalue, timedelay;


	// Use this for initialization
	void Start () {
        forcepoint.velocity = Vector2.zero;
        forcepoint.AddForceAtPosition(forcevalue * forceAngle * Time.deltaTime, pointtoaddForce);
        StartCoroutine(AddForce());
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    IEnumerator AddForce()
    {
        yield return new WaitForSeconds(timedelay);
        forcepoint.velocity = Vector2.zero;
        forcepoint.AddForceAtPosition(forcevalue * forceAngle * Time.deltaTime, pointtoaddForce);
        StartCoroutine(AddForce());
    }
}
