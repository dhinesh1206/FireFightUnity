using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {

    public enum RotationSide {
        Left,
        Right,
        Up,
        Down,
        ForWard,
        BackWard
    };
    public RotationSide rotationSide = RotationSide.Right;
    public float speedMultiplier;


	// Update is called once per frame
	void Update () {
        AutoRotation();
	}

    void AutoRotation()
    {
        if(rotationSide == RotationSide.ForWard)
        {
            transform.Rotate(Vector3.forward * speedMultiplier);
        } 
        else if (rotationSide == RotationSide.BackWard)
        {
            transform.Rotate(Vector3.back * speedMultiplier);
        }
        else if (rotationSide == RotationSide.Left)
        {
            transform.Rotate(Vector3.left * speedMultiplier);
        }
        else if (rotationSide == RotationSide.Right)
        {
            transform.Rotate(Vector3.right * speedMultiplier);
        }
        else if (rotationSide == RotationSide.Up)
        {
            transform.Rotate(Vector3.up * speedMultiplier);
        }
        else if (rotationSide == RotationSide.Down)
        {
            transform.Rotate(Vector3.down * speedMultiplier);
        }
    }
}
