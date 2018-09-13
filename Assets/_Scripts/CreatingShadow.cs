using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatingShadow : MonoBehaviour {

    float distancefromLight;
    bool lightDetected;
    GameObject lightObject;

    private void Update()
    {
        if(lightDetected)
        {
            distancefromLight = Vector2.Distance(transform.position, lightObject.gameObject.transform.position);

            GameObject shadow = transform.GetChild(0).gameObject;

            Vector3 shadowPosition = shadow.transform.localPosition;

            if(transform.position.x > lightObject.transform.position.x)
            {
                shadowPosition.x = distancefromLight * 0.05f;
               // transform.GetChild(0).transform.localPosition = new Vector2(distancefromLight * 0.05f, transform.GetChild(0).transform.localPosition.y);
            }
            if (transform.position.x < lightObject.transform.position.x)
            {
                shadowPosition.x = distancefromLight * -0.05f;
                //transform.GetChild(0).transform.localPosition = new Vector2(distancefromLight * -0.05f, transform.GetChild(0).transform.localPosition.y);
            }

            if(transform.position.y > lightObject.transform.position.y)
            {
                shadowPosition.y = distancefromLight * 0.05f;
                //transform.GetChild(0).transform.localPosition = new Vector2(transform.GetChild(0).transform.localPosition.x,distancefromLight * 0.05f);
            }
            if (transform.position.y < lightObject.transform.position.y)
            {
                shadowPosition.y = distancefromLight * -0.05f;
                //transform.GetChild(0).transform.localPosition = new Vector2(transform.GetChild(0).transform.localPosition.x, distancefromLight * -0.05f);
            }

            shadow.transform.localPosition = shadowPosition;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag == "Light")
        {
            lightObject = collision.gameObject;
            lightDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag == "Light")
        {
            lightDetected = false;
            lightObject = null;
        }
    }
}
