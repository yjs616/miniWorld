using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishPlayerController : MonoBehaviour
{

    public float moveStep = 2.0f; // 한 칸의 크기
    private Vector3 startPosition;

    public AudioClip breadSE;
    public AudioClip forkSE;
    AudioSource aud;

    GameObject director;

    private void Start()
    {
        startPosition = transform.position;
        this.director = GameObject.Find("DishGameDirector");
        this.aud = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.right);
        }
    }

    private void Move(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction * moveStep;
        // 새로운 위치가 이동 가능한 영역 내에 있는지 확인
        if (newPosition.x >= 11.5f && newPosition.x <= 15.5f && newPosition.z >= -5.5f && newPosition.z <= -1.5f)
        {
            transform.position = newPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fork"))
        {
            this.director.GetComponent<DishGameDirector>().DecreaseHP();
            this.aud.PlayOneShot(this.forkSE);      //오디오 플레이
            
        }
        else if (other.gameObject.CompareTag("Bread"))
        {
            this.director.GetComponent<DishGameDirector>().IncreaseBread();
            this.aud.PlayOneShot(this.breadSE);     //오디오 플레이
            
        }
        Destroy(other.gameObject);
    }
}
