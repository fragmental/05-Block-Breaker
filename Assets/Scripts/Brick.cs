using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
    private LevelManager levelManager; 
    private int maxHits;
    private int timesHit;
    private bool isBreakable;

    public static int breakableCount = 0;

    public AudioClip crack;
    public Sprite[] hitSprites;
    public GameObject smoke;

	// Use this for initialization
	void Start () {
        timesHit = 0;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        maxHits = hitSprites.Length + 1;
        isBreakable = (this.tag == "Breakable");
            //keep track of breakable bricks
        if (isBreakable)
        {
            breakableCount++;
            print(breakableCount);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(crack, transform.position, 0.2f);
    }

    void OnCollisionExit2D (Collision2D col)
    {

        if (isBreakable)
        {
            HandleHits();
        }
    }

    void HandleHits()
    {
        timesHit++;
        if (timesHit >= maxHits)
        {
            breakableCount--;
            //Debug.Log(breakableCount);
            levelManager.BrickDestroyed();
            PuffSmoke();
           
            Destroy(gameObject);
        }
        else
        {
            LoadSprites();
        }
    }

    void PuffSmoke()
    {
        GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
        //^Don't need " as GameObject;" ¯\_(ツ)_/¯. Did it anyway while trying to diagnose a bug where puff color was delayed.  
        //Fixed by using "smokePuff instead of "smoke" btw

        //todo: update this function
        smokePuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }


    void LoadSprites()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Brick Sprite is missing");
        }
        
    }

    // TODO Remove this method once we can actually win!
    void SimulateWin()
    {
        levelManager.LoadNextLevel();
    }
}
