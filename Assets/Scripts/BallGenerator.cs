using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform ballPosition;

    private GameObject currentBall;
    private bool ballInPlay = false;

    void Start()
    {
        // 기존 공이 없을 때만 생성
        if (!ballInPlay)
        {
            GenerateBall();
        }
    }

    void GenerateBall()
    {
        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        currentBall = Instantiate(ballPrefab, ballPosition.position, ballPosition.rotation);
        currentBall.GetComponent<Rigidbody>().useGravity = false;
        ballInPlay = true;
    }

    public void OnBallMissed()
    {
        ballInPlay = false;
        GenerateBall();
    }

    public void OnBallThrown()
    {
        ballInPlay = false;
    }
    
}
