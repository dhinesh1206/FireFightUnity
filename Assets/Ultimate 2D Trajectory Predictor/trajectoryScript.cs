using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class trajectoryScript : MonoBehaviour {
    public static trajectoryScript instance;

    public GameObject player;      
	public Sprite dotSprite;					
	public bool changeSpriteAfterStart;			
	public float initialDotSize;				
	public int numberOfDots;					
	public float dotSeparation;					
	public float dotShift;						
	public float idleTime;						
    public GameObject trajectoryDots;				
	private Rigidbody2D ballRB;					
	private Vector3 ballPos;					
	private Vector3 fingerPos;					
	private Vector3 ballFingerDiff;				
	private Vector2 shotForce;					
	private float x1, y1;						
    public GameObject helpGesture;				
	private float idleTimer = 7f;				
    public bool ballIsClicked = false;			
	private bool ballIsClicked2 = false;		
	public GameObject ballClick;				
	public float shootingPowerX;			
	public float shootingPowerY;				
	public bool usingHelpGesture;				
	public bool explodeEnabled;					
	public bool grabWhileMoving;				
	public GameObject[] dots;					
	public bool mask;
	private BoxCollider2D[] dotColliders;
    public float speed;
    public float minX, maxX, minY, maxY;
    Sequence sequence;

   // public bool playerMover = true;

    public Vector3 touchStartPosition;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        trajectoryDots.transform.localScale = new Vector3(initialDotSize, initialDotSize, trajectoryDots.transform.localScale.z);

        for (int i = 0; i < 40; i++)
        {
            dots[i] = GameObject.Find("Dot (" + i + ")");
            if (dotSprite != null)
            {
                dots[i].GetComponent<SpriteRenderer>().sprite = dotSprite;
            }
        }
        for (int k = numberOfDots; k < 40; k++)
        {
            GameObject.Find("Dot (" + k + ")").SetActive(false);
        }
        trajectoryDots.SetActive(false);
    }




    void Update()
    {

        if (GameManager.instance.currentPlayer)
        {
            player = GameManager.instance.currentPlayer;
            ballRB = player.GetComponent<Rigidbody2D>();
        } else {
            player = null;
            ballRB = null;
        }

        if (numberOfDots > 40)
        {
            numberOfDots = 40;
        }

        if (usingHelpGesture == true)
        {                               
            helpGesture.transform.position = new Vector3(ballPos.x, ballPos.y, ballPos.z); 
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);  

        if (hit.collider != null && ballIsClicked2 == false)
        {                   
            if (hit.collider.gameObject.name == ballClick.gameObject.name)
            {   
                ballIsClicked = true;                                           
            }
            else
            {                                                          
                ballIsClicked = false;                                         
            }
        }
        else
        {                                                              
            ballIsClicked = false;                                             
        }

        if (ballIsClicked2 == true)
        {                                         
            ballIsClicked = true;                                         
        }

        if (ballRB)
        {
            if ((ballRB.velocity.x * ballRB.velocity.x) + (ballRB.velocity.y * ballRB.velocity.y) <= 0.0085f)
            { 
                ballRB.velocity = new Vector2(0f, 0f);
                idleTimer -= Time.deltaTime;

            }
            else
            {
                trajectoryDots.SetActive(false);
            }
        }

        if (usingHelpGesture == true && idleTimer <= 0f)
        {                      
            helpGesture.GetComponent<Animator>().SetBool("Inactive", true); 
        }


        if(player)
        ballPos = player.transform.position;    
       // ballPos = touchStartPosition;

        if (changeSpriteAfterStart == true)
        {                                  
            for (int k = 0; k < numberOfDots; k++)
            {
                if (dotSprite != null)
                {                                      
                    dots[k].GetComponent<SpriteRenderer>().sprite = dotSprite;
                }
            }
        }

        if(ballRB)
        {
            if ((Input.GetKey(KeyCode.Mouse0) && ballIsClicked == true) && ((ballRB.velocity.x == 0f && ballRB.velocity.y == 0f) || (grabWhileMoving == true)))
            {  								
                ballIsClicked2 = true;

                if (usingHelpGesture == true)
                {
                    idleTimer = idleTime;
                    helpGesture.GetComponent<Animator>().SetBool("Inactive", false);
                }

                fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                fingerPos.z = 0;

                if (grabWhileMoving == true)
                {
                    ballRB.velocity = new Vector2(0f, 0f);
                    ballRB.isKinematic = true;
                }


                ballFingerDiff = touchStartPosition - fingerPos;
              //  print(ballFingerDiff);

                if (ballFingerDiff.y >= maxY)
                {
                    ballFingerDiff = new Vector3(ballFingerDiff.x, maxY, ballFingerDiff.z);
                }
                if (ballFingerDiff.x >= maxX)
                {
                    ballFingerDiff = new Vector3(maxX, ballFingerDiff.y, ballFingerDiff.z);
                }
                if (ballFingerDiff.x <= minX)
                {
                    ballFingerDiff = new Vector3(minX, ballFingerDiff.y, ballFingerDiff.z);
                }
                if (ballFingerDiff.y <= minY)
                {
                    ballFingerDiff = new Vector3(ballFingerDiff.x, minY, ballFingerDiff.z);
                }

                shotForce = new Vector2(ballFingerDiff.x * shootingPowerX, ballFingerDiff.y * shootingPowerY);

                if ((Mathf.Sqrt((ballFingerDiff.x * ballFingerDiff.x) + (ballFingerDiff.y * ballFingerDiff.y)) > (0.37f)))
                {
                    trajectoryDots.SetActive(true);
                }
                else
                {
                    trajectoryDots.SetActive(false);
                    if (ballRB.isKinematic == true)
                    {
                        ballRB.isKinematic = false;
                    }
                }

                for (int k = 0; k < numberOfDots; k++)
                {
                    x1 = ballPos.x + shotForce.x * Time.fixedDeltaTime * (dotSeparation * k + dotShift);
                    y1 = ballPos.y + shotForce.y * Time.fixedDeltaTime * (dotSeparation * k + dotShift) - (-Physics2D.gravity.y / 2f * Time.fixedDeltaTime * Time.fixedDeltaTime * (dotSeparation * k + dotShift) * (dotSeparation * k + dotShift));    //Y position for each point is found
                    dots[k].transform.position = new Vector3(x1, y1, dots[k].transform.position.z);
                    if (k > 15)
                    {
                        dots[k].GetComponent<SpriteRenderer>().enabled = false;
                    }
                    else
                    {
                        dots[k].GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
            }
        }


        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            
            if ((ballFingerDiff.x > 0.05f || ballFingerDiff.x < -0.05f) && (ballFingerDiff.y > 0.4f || ballFingerDiff.y < -0.4f) && ballClick.activeSelf)
            {
               
                ballIsClicked2 = false;
                player = GameManager.instance.currentPlayer;
                StartCoroutine(MovePlayer(player));
                trajectoryDots.SetActive(false);
                ballClick.SetActive(false);
                GameManager.instance.CreatePlayer();
                ReadyForNextJump();
                //GameManager.instance.playerDetection.SetActive(false);
                GameManager.instance.currentPlayer = null;
            }
        }
    }


    IEnumerator MovePlayer(GameObject activePlayer)
    {
        if (activePlayer)
        {
           GameObject jumpedPlayer = activePlayer;
            GameManager.instance.playerActiveinScene.Remove(jumpedPlayer);
            jumpedPlayer.tag = "JumpedPlayer";
            jumpedPlayer.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            int activeDots = 0;

            for (int i = 0; i < dots.Length; i++)
            {
                if (dots[i].activeSelf)
                {
                    activeDots += 1;
                }
            }

            for (int i = 0; i < dots.Length; i++)
            {

                if (dots[i].activeSelf)
                {
                    sequence.Append(jumpedPlayer.transform.DOMove(dots[i].transform.position, speed / activeDots, false).SetEase(Ease.Linear));

                    //jumpedPlayer.transform.DOMove(dots[i].transform.position, speed / activeDots, false).SetEase(Ease.Linear).SetId("Jumping");
                    yield return new WaitForSeconds(speed / activeDots);
                }
            }

            jumpedPlayer.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

        }
        yield return null;
    }

	public IEnumerator explode(){											
		yield return new WaitForSeconds (Time.fixedDeltaTime * (dotSeparation * (numberOfDots - 1f)));	
		Debug.Log ("exploded");
	}

	public void collided(GameObject dot){

		for (int k = 0; k < numberOfDots; k++) {
			if (dot.name == "Dot (" + k + ")") 
            {
				for (int i = k + 1; i < numberOfDots; i++) {					
					dots [i].gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				}
			}
		}
	}

    public void StopJump(GameObject player)
    {
        //playerMover = false;

        // DOTween.Kill("Jumping", false);
        sequence.Kill();
        StopAllCoroutines();
        for (int i = 0; i < dots.Length;i++)
        {
            dots[i].GetComponent<SpriteRenderer>().enabled = false;
        }
        player.transform.localPosition = Vector3.zero;
       
    }

    public void ReadyForNextJump()
    {
        Invoke("EnableForNextJump", 1f);
    }

    public void ClearDots()
    {
       // trajectoryDots = GameObject.Find("Trajectory Dots");
        trajectoryDots.transform.localScale = new Vector3(initialDotSize, initialDotSize, trajectoryDots.transform.localScale.z);

        for (int k = 0; k < 40; k++)
        {
            if (dotSprite != null)
            {
                dots[k].GetComponent<SpriteRenderer>().sprite = dotSprite;
            }
        }
        for (int k = numberOfDots; k < 40; k++)
        {
            GameObject.Find("Dot (" + k + ")").SetActive(false);
        }
        trajectoryDots.SetActive(false);
    }

    public void EnableForNextJump()
    {
       // playerMover = true;
        ClearDots();
        ballClick.SetActive(true);
    }

	public void uncollided(GameObject dot){
		for (int k = 0; k < numberOfDots; k++) {
			if (dot.name == "Dot (" + k + ")") {

				for (int i = k-1; i > 0; i--) {
				
					if (dots [i].gameObject.GetComponent<SpriteRenderer> ().enabled == false) {
						Debug.Log ("nigggssss");
						return;
					}
				}
				if (dots [k].gameObject.GetComponent<SpriteRenderer> ().enabled == false) {
					for (int i = k; i > 0; i--) {
						dots [i].gameObject.GetComponent<SpriteRenderer> ().enabled = true;
					}
				}
			}
		}
	}
}

