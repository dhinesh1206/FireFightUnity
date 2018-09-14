using UnityEngine;

public class AnimationSelection : MonoBehaviour {


    public string[] levelAnimationClipsName;
    
    Animator wheelAnimator;

    string currentAnimation = "";

    private void Start()
    {
        wheelAnimator = gameObject.GetComponent<Animator>();
        ChangeAnimation();
    }


    public void ChangeAnimation()
    {
        string animationToPlay = levelAnimationClipsName[Random.Range(0, levelAnimationClipsName.Length)];
        print(animationToPlay);
        if(currentAnimation != animationToPlay)
        {
            currentAnimation = animationToPlay;
            wheelAnimator.Play(animationToPlay);
        }
        else
        {
            ChangeAnimation();
        }

    }
}
