using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public bool autoPlay = false;

    private Ball ball;
    private Vector3 paddlePOS;
    private float mousePosInBlocks;
    public float minX;
    public float maxX;

	// Use this for initialization
	void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!autoPlay)
        {
            MoveWithMouse();
        }
        else
        {
            AutoPlay();
        }
    }

    void AutoPlay()
    {
        paddlePOS = new Vector3(0.5f, this.transform.position.y, 0f);
        //mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
        Vector3 ballPos = ball.transform.position;
        paddlePOS.x = Mathf.Clamp(ballPos.x, minX, maxX);
        this.transform.position = paddlePOS;
    }

    void MoveWithMouse()
    {
        paddlePOS = new Vector3(0.5f, this.transform.position.y, 0f);
        mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
        paddlePOS.x = Mathf.Clamp(mousePosInBlocks, minX, maxX);
        this.transform.position = paddlePOS;
    }
}
