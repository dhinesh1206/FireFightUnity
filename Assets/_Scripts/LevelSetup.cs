using System.Collections.Generic;
using UnityEngine;

public class LevelSetup:MonoBehaviour
{
    public Transform netLandinPosition;
    public GameObject[] playerPrefabs;
    public Transform hitCollider;
    public float minX, maxX, minY, maxY;
    public Animator[] wheelAnimator;
    public List<GameObject> playersActiveInLevel;
    public GameObject playerDetection;
    public Vector3 endPositionfromNet,newPlayerChangeValue, newplayerCreationPoint;
    public int PlayersToSave;
	public Transform[] coinPickUpPosition;
}
