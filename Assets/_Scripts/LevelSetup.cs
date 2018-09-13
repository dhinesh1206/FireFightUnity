using System.Collections.Generic;
using UnityEngine;

public class LevelSetup:MonoBehaviour
{
    public Transform crateplayerPosition, netLandinPosition;
    public GameObject[] playerPrefabs;
    public AudioClip[] bgSounds;
    public AudioClip jumpSound, dropSound, failSound;
    public Transform hitCollider;
    public float minX, maxX, minY, maxY;
    public Animator[] wheelAnimator;
    public List<GameObject> playersActiveInLevel;
    public GameObject playerDetection;
    public Vector3 endPositionfromNet,newPlayerChangeValue, newplayerCreationPoint;
    public string AnimationName;
    public int PlayersToSave;
	public Transform[] coinPickUpPosition;
}
