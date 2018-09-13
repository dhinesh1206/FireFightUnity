using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCageCotation : MonoBehaviour {

    public Transform armposition;
    public Transform cageposition;

    Vector3 offset;

	// Use this for initialization
	void Start () {
        //offset = armposition.position - cageposition.position;
	}
	
	// Update is called once per frame
	void Update () {
        //cageposition.position = armposition.position ;

        // float rotationvalue = armposition.transform.rotation.z;
        armposition.rotation = Quaternion.Euler(Vector3.zero);
        
	}
}
