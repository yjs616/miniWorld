using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiningRoomChairController : MonoBehaviour
{
    public float moveStep = 1.0f; // 의자가 한 번에 이동할 거리
    public float targetZPosition = 14.0f; // 의자가 이동할 x축의 최종 위치

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MoveChair();
        }
    }

    private void MoveChair()
    {
        if (transform.position.z < targetZPosition)
        {
            transform.position += new Vector3(0, 0, moveStep); // z축으로 이동
        }
    }
}

