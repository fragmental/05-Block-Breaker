using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Paddle paddle;
    private Vector3 paddleToBallVector;
    private bool hasStarted = false;

	// Use this for initialization
	void Start () {
        paddle = GameObject.FindObjectOfType<Paddle>();
        paddleToBallVector = this.transform.position - paddle.transform.position;
        //print(paddleToBallVector.y);
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasStarted)
        {
            //Lock the ball relative to paddle.
            this.transform.position = paddle.transform.position + paddleToBallVector;
        
        //Wait for a mouse press to launch
            if (Input.GetMouseButtonDown(0))
            {
                print("mouse clicked");
                GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 10f);
                //this.rigidbody2D
                hasStarted = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 tweak = new Vector2(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));
        Debug.Log(tweak);

        if (hasStarted)
        {
            //audio.Play();
            GetComponent<AudioSource>().Play();
            //rigidbody.velocity += tweak;
            GetComponent<Rigidbody2D>().velocity += tweak;
        }
       
    }
}
