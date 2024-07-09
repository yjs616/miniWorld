using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCameraController : MonoBehaviour
{
    public float moveSpeed = 15f; // 카메라 이동 속도
    public float moveRange = 16f;  // 좌우 이동 범위, 5

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position; // 시작 위치 저장
    }

    void Update()
    {
        // 이동 방향 결정
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= startPosition.x + moveRange)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= startPosition.x - moveRange)
            {
                movingRight = true;
            }
        }
    }
}
