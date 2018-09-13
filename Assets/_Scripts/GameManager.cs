using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region variables
    public static GameManager instance;
    [Header("UI part")]
    public Text scoreText;
    public GameObject gameoverScreen;

    [Space]
    [Header("Score Manager")]
    public int scoreMultiplier;


    [Space]
    [Header("Audio Manager")]
    public AudioSource audioSource;
    public AudioSource bgAudioSource;
    [HideInInspector]
    public AudioClip dropSound, jumpSound, failSound;
    [HideInInspector]
    public AudioClip[] bgSounds;

    [Space]
    [Header("GamePlay")]
    public GameObject[] playerPrefabs;
    [HideInInspector]
    public Transform crateCharecterPosition,netPosition;

    public GameObject currentPlayer;
    [HideInInspector]
    public bool canJump, canDrop, inCrate;
   
    public List<GameObject> playerActiveinScene;
    bool jumpedInsidejumpArea, jumpedInsideDropArea,jumped;
	public GameObject coinPrefab;


    [Space]
    [Header("Level Manager")]
    public GameObject[] levelPrefabs;
    int score,highScore ;
    public int levelNumber;
    [HideInInspector]
    public Animator[] wheelAnimator;
    public string animationName;

    public GameObject playerDetection;
    public Image bg;

    public float rotationSTopTime;
    public Vector3 endPosition,newPlayerPositionChangeValue, newPlayerCreationPoint;
    GameObject currentLevel;
    public int playersToSave;
	public Transform[] coinpickupPositions;
	Transform currentCoinPickUpPoint;

	public List<AnimationTypes> charecterList;
	public enum MyEnum
	{
		Walk,
		Cry,
		Happy,
		Fly
	}
    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateLevel(levelNumber);
    }

    public void CreateLevel(int levelNumber)
    {
        currentLevel = Instantiate(levelPrefabs[levelNumber]);
        currentLevel.transform.position = Vector3.zero;
        LevelSetup levelInfo = currentLevel.GetComponent<LevelSetup>();
        playerPrefabs = levelInfo.playerPrefabs;
        crateCharecterPosition = levelInfo.crateplayerPosition;
        netPosition = levelInfo.netLandinPosition;
        jumpSound = levelInfo.jumpSound;
        dropSound = levelInfo.dropSound;
        failSound = levelInfo.failSound;
        bgSounds = levelInfo.bgSounds;
        GameLevelBegins();
        trajectoryScript tranjector = transform.GetComponent<trajectoryScript>();
        tranjector.ballClick = levelInfo.hitCollider.gameObject;
        tranjector.minY = levelInfo.minY;
        tranjector.minX = levelInfo.minX;
        tranjector.maxX = levelInfo.maxX;
        tranjector.maxY = levelInfo.maxY;
        wheelAnimator = levelInfo.wheelAnimator;
        animationName = levelInfo.AnimationName;
        playerDetection = levelInfo.playerDetection;
        playerActiveinScene = levelInfo.playersActiveInLevel;
        endPosition = levelInfo.endPositionfromNet;
        newPlayerPositionChangeValue = levelInfo.newPlayerChangeValue;
        newPlayerCreationPoint = levelInfo.newplayerCreationPoint;
        playersToSave = levelInfo.PlayersToSave;
		coinpickupPositions = levelInfo.coinPickUpPosition;

        foreach(Animator anim in wheelAnimator)
        {
            anim.Play(animationName);
        }
    }

    public void GameLevelBegins()
    {
        //AllPlayers();
        score = 0;
        scoreText.text = score.ToString();
        bgAudioSource.clip = bgSounds[Random.Range(0, bgSounds.Length)];
        bgAudioSource.Play();  
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) )
        {
           // CreateTestDot(dotCreationPoint.transform.position);
            //if (canJump)
            //{
            //    JumptoCrate();
            //} 
            //else if (canDrop && inCrate)
            //{
            //    JumptoNet();
            //}
            //jump();
        } 
    }

    public void DropPlayer(GameObject other, Collider2D othercollidr)
    {
             GameObject dropplayer = other;
             othercollidr.transform.parent.GetChild(1).GetComponent<CurrentPlayerIndicator>().objectInCrate = null;
              JumptoNet(dropplayer);

    }

    public void JumptoNet(GameObject incratePlayer)
    {
		audioSource.clip = dropSound;
        audioSource.Play();
        incratePlayer.transform.SetParent(netPosition);
       
        incratePlayer.transform.localRotation = Quaternion.EulerRotation(Vector3.zero);
        Vector2 newPosition = new Vector2 (0,1f);
        incratePlayer.transform.DOLocalMoveY(2f, 0.25f, false).SetId("JumpUp").SetEase(Ease.Linear).OnComplete(() =>
        {
            incratePlayer.transform.DOLocalMoveY(newPosition.y, 0.25f, false).SetId("JumpDown").SetEase(Ease.Linear);

        });
        incratePlayer.transform.DOLocalMoveX(newPosition.x, 0.5f, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            incratePlayer.GetComponent<Animator>().Play("Walk");
            incratePlayer.transform.GetChild(0).GetComponent<Animator>().Play("Walk");
            MovePlayer(incratePlayer);
        });
    }

	public void CreateNewCoins()
	{
		GameObject coinCreated = Instantiate (coinPrefab,coinpickupPositions[Random.Range(0,coinpickupPositions.Length)],false);
	}

    public void GameOver()
    {
        gameoverScreen.SetActive(true); 
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void ResumeWheelSpeed()
    {
        foreach (Animator crateAnimation in wheelAnimator)
        {
            crateAnimation.speed = 1;
        }
    }

	public IEnumerator CreateImageAnimation(SpriteRenderer playerSprite, Texture2D[] danceMoves, float fps)
	{
		foreach (Texture2D item in danceMoves) {
			playerSprite.sprite = CreateSprite (item); 
			yield return new WaitForSeconds (1/fps);
		}
		StartCoroutine (CreateImageAnimation ());
	}

	Sprite CreateSprite(Texture2D spriteTexture)
	{
		Sprite newImageSprite;

		newImageSprite = Sprite.Create(spriteTexture,new Rect(0, 0,spriteTexture.width ,spriteTexture.height),new Vector2(0.5f,0.5f));
		return newImageSprite;
	}


    public void JumptoCrate(GameObject cratePlayerPosition, GameObject playertoJump)
    {
       // playerActiveinScene.Remove(playertoJump);
        GameObject plyer = playertoJump;
        //plyer.gameObject.tag = "Untagged";
        //currentPlayer = null;
        trajectoryScript.instance.StopJump(plyer);
       // audioSource.clip = jumpSound;
       // audioSource.Play();
        plyer.transform.SetParent(cratePlayerPosition.transform.GetChild(0).transform);
        // Vector2 newPosition = Vector3.zero;
       // WaitForSecondsRealtime(0.5f);
        Vector2 newPosition = Vector3.zero;
        plyer.transform.localPosition = new Vector3(0, plyer.transform.localPosition.y , 0);
        if(plyer.transform.localPosition.y > 0.2f)
        {
            plyer.transform.localPosition = new Vector3(0,0.2f, 0);
        }
        print(plyer.transform.localPosition);
        plyer.transform.DOLocalMoveY(plyer.transform.localPosition.y +0.1f, 0.15f, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            plyer.transform.DOLocalMoveY(0, 0.15f, false).SetEase(Ease.Linear).SetEase(Ease.Linear).OnComplete(() => {
                plyer.transform.DOLocalMove(Vector3.zero, 0.1f, false);
            });
        });
        // plyer.transform.DOLocalMoveX(newPosition.x, 0.3f, false).SetEase(Ease.Linear);

        print(plyer.transform.localPosition);
        plyer.GetComponent<Animator>().Play("Happy");
        plyer.transform.GetChild(0).GetComponent<Animator>().Play("Happy");
        inCrate = true;
    }




    public void CLearCurrentPlayer()
    {
       // currentPlayer = null;
    }

    public void CreatePlayer()
    {
        
        Invoke("CreateNewPlayer", 1f);
    }

    public void DetectPlayer(GameObject player)
    {
        currentPlayer = player;
        transform.GetComponent<trajectoryScript>().player = player;
        currentPlayer.GetComponent<Animator>().Play("Cry");
        currentPlayer.transform.GetChild(0).GetComponent<Animator>().Play("Cry");
    }

    public void MovePlayer(GameObject other)
    {
        other.transform.DOLocalMove(endPosition, 0.5f, false).OnComplete(() =>
        {
           // playerActiveinScene.Remove(currentPlayer);
            Destroy(other);
            IncrementScore();
            //Invoke("CreateNewPlayer", 0.1f);
        });
    }

    public void CheckRotationSpeed()
    {
        foreach (Animator crateAnimation in wheelAnimator)
        {
            crateAnimation.speed = 1;
        }
        Invoke("ResumeWheelSpeed", rotationSTopTime);
    }

    IEnumerator ChangePlayerPosition()
    {
        foreach (GameObject player in playerActiveinScene)
        {
            if (player != null)
                player.transform.localPosition = new Vector3(player.transform.position.x + newPlayerPositionChangeValue.x, player.transform.position.y + newPlayerPositionChangeValue.y, 0);

                 //  player.transform.DOLocalMove(new Vector3(player.transform.position.x + 2f, player.transform.position.y + 1.75f, 0), 0.5f, false);
                
        }
        yield return new WaitForSeconds(0.0f);
       // GameManager.instance.playerDetection.SetActive(true);
    }

    public void AllPlayers()
    {
        //playerActiveinScene.Clear();
        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        //foreach(GameObject player in players) 
        //{
        //    if (!playerActiveinScene.Contains(player) && player.tag == "Player")
        //        playerActiveinScene.Add(player);
        //}
    }

    IEnumerator DelayAnimation(string animationname, Animator animatortoAnimate, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        animatortoAnimate.Play(animationname);
        animatortoAnimate.gameObject.transform.GetChild(0).GetComponent<Animator>().Play(animationname);
    }

    public void CreateNewPlayer()
    {
        if (playersToSave > 3)
        {
           
            GameObject newPlayer = Instantiate(playerPrefabs[Random.Range(0, playerPrefabs.Length)]);
            newPlayer.transform.position = newPlayerCreationPoint;
            newPlayer.transform.SetParent(currentLevel.transform);
            playerActiveinScene.Add(newPlayer);

            foreach (GameObject player in playerActiveinScene)
            {
                if (player != null)
                {
                    StartCoroutine(DelayAnimation("Cry", player.GetComponent<Animator>(), 0.5f));
                }
            }
        }
        playersToSave -= 1;
        StartCoroutine(ChangePlayerPosition());
    }

    public void  IncrementScore()
    {
        score += scoreMultiplier;
        scoreText.text = score.ToString();
        if (score == 5)
        {
            Invoke("CreateNewLvelAfterScore", 3f);
        }
    }

    public void CreateNewLvelAfterScore()
    {
        Destroy(currentLevel);
        CreateLevel(Random.Range(0, levelPrefabs.Length)); 
    }
}

[System.Serializable]
public class AnimationTypes
{	
	public int charecterIndex;
	public List<AnimationTexture> charecterAnimations;
}

[System.Serializable]
public class AnimationTexture
{
	public GameManager.MyEnum AnimationEnum;
	public float fps = 24;
	public Texture2D[] spriteTextures;	
}
