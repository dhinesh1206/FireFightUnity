using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager1 : MonoBehaviour
{
    #region variables
    public static GameManager1 instance;

    public GameObject[] playerPrefabs;
    public Transform netPosition;
    public GameObject playerinPosition;

    public bool isPlayerinCrate;

    public List<GameObject> playersActiveinScreen;
    public GameObject[] levelPrefabs;
    public int levelNumber;
    public Animator[] wheelAnimator;


    #endregion


    private void Awake()
    {

        instance = this;
    }

    private void Start()
    {
        GameObject currentLevel = Instantiate(levelPrefabs[levelNumber - 1]);
        currentLevel.transform.position = Vector3.zero;
        LevelSetup levelInfo = currentLevel.GetComponent<LevelSetup>();
        playerPrefabs = levelInfo.playerPrefabs;
        netPosition = levelInfo.netLandinPosition;
        trajectoryScript tranjector = transform.GetComponent<trajectoryScript>();
        tranjector.ballClick = levelInfo.hitCollider.gameObject;
        tranjector.minY = levelInfo.minY;
        tranjector.minX = levelInfo.minX;
        tranjector.maxX = levelInfo.maxX;
        tranjector.maxY = levelInfo.maxY;
        wheelAnimator = levelInfo.wheelAnimator;
        playersActiveinScreen = levelInfo.playersActiveInLevel;
        GameBegin();
    }

    public void GameBegin()
    {
        isPlayerinCrate = false;

    }

    public void DetectCurrentPlayer(GameObject player)
    {
        playerinPosition = player;
        transform.GetComponent<trajectoryScript>().player = playerinPosition;
        playerinPosition.GetComponent<Animator>().Play("Cry");
        playerinPosition.transform.GetChild(0).GetComponent<Animator>().Play("Cry");
    }

    public void JumptoCrate(GameObject cratePlayerPosition)
    {
        GameObject playerJumping = playerinPosition;
        playerJumping.tag = "Untagged";
        trajectoryScript.instance.StopJump(playerJumping);
        playerJumping.transform.SetParent(cratePlayerPosition.transform.GetChild(0).transform);
        Vector2 newPosition = Vector3.zero;
        playerJumping.transform.DOLocalMoveY(1, 0.2f, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            playerJumping.transform.DOLocalMoveY(0,0.2f, false).SetEase(Ease.Linear);
        });
        playerJumping.transform.DOLocalMoveX(newPosition.x,0.4f,false).SetEase(Ease.Linear);
        playerJumping.transform.localPosition = Vector3.zero;
        isPlayerinCrate = true;
    }

    public void DropPlayer(GameObject other)
    {
        if(isPlayerinCrate)
        {
            JumptoNet(other);
        }
    }

    public void JumptoNet(GameObject playertojump)
    {
        isPlayerinCrate = false;
        playertojump.transform.SetParent(netPosition);
        playertojump.transform.localRotation = Quaternion.EulerRotation(Vector3.zero);
        Vector2 newPosition = new Vector2(0, 1f);
        playertojump.transform.DOLocalMoveY(2f, 0.25f, false).SetId("JumpUp").SetEase(Ease.Linear).OnComplete(() =>
        {
            playertojump.transform.DOLocalMoveY(newPosition.y, 0.25f, false).SetId("JumpDown").SetEase(Ease.Linear);

        });
        playertojump.transform.DOLocalMoveX(newPosition.x, 0.5f, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            playertojump.GetComponent<Animator>().Play("Walk");
            playertojump.transform.GetChild(0).GetComponent<Animator>().Play("Walk");
            MovePlayer(playertojump);
        });
    }

    public void MovePlayer(GameObject other)
    {
        other.transform.DOLocalMoveX(1, 0.5f, false).OnComplete(() =>
        {
            Destroy(other);
        });
    }










    //#region GameLoadingManagement
    //public void GameOver()
    //{
    //    gameoverScreen.SetActive(true);
    //}

    //public void Restart()
    //{
    //    SceneManager.LoadScene(0);
    //}
    //#endregion

}
