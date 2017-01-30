using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public AudioClip crack;
	public Sprite[] hitSprites;
	public static int breakableCount = 0;

	private bool isBreakable;
	private int timesHit;
	private LevelManager levelManager;

	// Use this for initialization
	void Start () 
	{
		isBreakable = (this.tag == "Breakable");
		if (isBreakable)
		{
			breakableCount++;
		}

		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		AudioSource.PlayClipAtPoint (crack, transform.position);
		if (isBreakable)
		{
			HanleHits();
		}
	}

	void HanleHits()
	{
		timesHit++;
		int maxHits = hitSprites.Length +1;
		if (timesHit >= maxHits)
		{
			breakableCount--;
			levelManager.BrickDestroyed();
			Destroy(gameObject);
		} else
		{
			LoadSprites();
		}
	}

	void LoadSprites ()
	{
		int spriteIndex = timesHit -1;

		if (hitSprites[spriteIndex] !=null)
		{
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}else
		{
			Debug.LogError("Brick Sprite missing");
		}
	}

	void SimulateWin()
	{
		levelManager.LoadNextLevel();

	}
}
