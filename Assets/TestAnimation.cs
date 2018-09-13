using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestAnimation : MonoBehaviour {

	public Texture2D[] danceMoves;
	public SpriteRenderer playerSprite;
	public float fps;


	void Start()
	{
		StartCoroutine (CreateImageAnimation ());
	}

	IEnumerator CreateImageAnimation()
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
}
