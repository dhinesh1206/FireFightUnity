using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIneWaveCreation : MonoBehaviour {

    public GameObject circle;

    public List<GameObject> circleCreatedList;

    public float multiplier;

    public Vector3 startPosition;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 7; i++)
        {
            GameObject circlecreated =  Instantiate(circle);
            circlecreated.transform.position = Vector2.zero;
            circleCreatedList.Add(circlecreated);

        }
        //CreateSinWave();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            startPosition = MouseHelper.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            if(MouseHelper.mousePosition.x < startPosition.x)
            {
                float Distance = Vector2.Distance(startPosition, MouseHelper.mousePosition);
                //print(Distance);
                float endpoint = -Distance/100 ;
                for (int i = 0; i < circleCreatedList.Count; i++)
                {
                   
                }

            } 
            else if (MouseHelper.mousePosition.x > startPosition.x)
            {
                float Distance = Vector2.Distance(startPosition, MouseHelper.mousePosition);
                float endpoint = Distance/100 ;
                for (int i = 0; i < circleCreatedList.Count; i++)
                {
                            CreateSinWaveWithDrag(circleCreatedList[i], ((i+1)));
                }
            }

        }
	}


    void CreateSinWaveWithDrag(GameObject cir, float endpoint)
    {
        cir.transform.position = new Vector2(endpoint, Mathf.Sin(endpoint)*100);
    }
}
